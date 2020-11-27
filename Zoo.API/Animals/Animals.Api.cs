using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Zoo.Constants;
using Zoo.Models.Animals;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using Zoo.Helper.Animals;

namespace Zoo.API.Animals
{
    /// <summary>
    /// Animal Api
    /// This is used to get all animal related logic
    /// </summary>
    public static class AnimalsApi
    {
        public static async void InitAnimals()
        {
            if (!File.Exists(AnimalDbConst.JsonFilePath) && !File.Exists(AnimalDbConst.JsonEncryptedFilePath))
            {
                List<BaseAnimal> animals = new List<BaseAnimal>();
                string jsonArray = JsonConvert.SerializeObject(animals, Formatting.Indented);
                File.WriteAllText(AnimalDbConst.JsonFilePath, jsonArray);
            await AnimalsHelper.EncryptFile(AnimalDbConst.JsonFilePath, AnimalDbConst.JsonEncryptedFilePath);
            }
        }
        public static async Task<List<BaseAnimal>> GetAnimals()
        {
            await AnimalsHelper.DecryptFile(AnimalDbConst.JsonEncryptedFilePath, AnimalDbConst.JsonFilePath);

            // Get the file from local machine
            string jsonFile = File.ReadAllText(AnimalDbConst.JsonFilePath);
            List<BaseAnimal> animalList = JsonConvert.DeserializeObject<List<BaseAnimal>>(jsonFile, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            });
            await AnimalsHelper.EncryptFile(AnimalDbConst.JsonFilePath, AnimalDbConst.JsonEncryptedFilePath);
            return animalList;
        }
        public static async void UpdateAnimals(List<BaseAnimal> animals)
        {
            await AnimalsHelper.DecryptFile(AnimalDbConst.JsonEncryptedFilePath, AnimalDbConst.JsonFilePath);
            List<BaseAnimal> animalList = animals;

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.TypeNameHandling = TypeNameHandling.Auto;
            serializer.Formatting = Formatting.Indented;

            using (StreamWriter sw = new StreamWriter(AnimalDbConst.JsonFilePath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, animalList, typeof(BaseAnimal));
            }

            await AnimalsHelper.EncryptFile(AnimalDbConst.JsonFilePath, AnimalDbConst.JsonEncryptedFilePath);
        }
        public static List<BaseAnimal> GetAnimalsOfType<T>(T type)
        {
            List<BaseAnimal> allAnimals = GetAnimals().Result;

            List<BaseAnimal> desiredAnimalTypeList = allAnimals.FindAll(s => s.GetType() == type.GetType());

            return desiredAnimalTypeList;
        }
        public static void AddAnimal(BaseAnimal animal)
        {
            List<BaseAnimal> allAnimals = GetAnimals().Result;
            allAnimals.Add(animal);
            UpdateAnimals(allAnimals);
        }
    }
}
