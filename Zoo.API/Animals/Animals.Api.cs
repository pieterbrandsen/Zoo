using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Zoo.Constants;
using Zoo.Helper.Animals;
using Zoo.Models.Animals;

namespace Zoo.API.Animals
{
    /// <summary>
    ///     Animal Api
    ///     This is used to get all animal related logic
    /// </summary>
    public static class AnimalsApi
    {
        public static async void InitAnimals()
        {
            if (!File.Exists(AnimalDbConst.JsonFilePath) && !File.Exists(AnimalDbConst.JsonEncryptedFilePath))
            {
                var animals = new List<BaseAnimal>();
                var jsonArray = JsonConvert.SerializeObject(animals, Formatting.Indented);
                await File.WriteAllTextAsync(AnimalDbConst.JsonFilePath, jsonArray);
                await AnimalsHelper.EncryptFile(AnimalDbConst.JsonFilePath, AnimalDbConst.JsonEncryptedFilePath);
            }
        }

        public static async Task<List<BaseAnimal>> GetAnimals()
        {
            try
            {
                await AnimalsHelper.DecryptFile(AnimalDbConst.JsonEncryptedFilePath, AnimalDbConst.JsonFilePath);
                // Get the file from local machine
                var jsonFile = File.ReadAllText(AnimalDbConst.JsonFilePath);
                var animalList = JsonConvert.DeserializeObject<List<BaseAnimal>>(jsonFile, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore
                });
                await AnimalsHelper.EncryptFile(AnimalDbConst.JsonFilePath, AnimalDbConst.JsonEncryptedFilePath);
                return animalList;
            }
            catch (Exception e)
            {
                Console.WriteLine("There was an error getting animals");
                Console.WriteLine(e);
                throw;
            }
        }

        public static async void UpdateAnimals(List<BaseAnimal> animals)
        {
            try
            {
                await AnimalsHelper.DecryptFile(AnimalDbConst.JsonEncryptedFilePath, AnimalDbConst.JsonFilePath);

                JsonSerializer serializer = new JsonSerializer();
                serializer.Converters.Add(new JavaScriptDateTimeConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;
                serializer.TypeNameHandling = TypeNameHandling.Auto;
                serializer.Formatting = Formatting.Indented;

                using (StreamWriter sw = new StreamWriter(AnimalDbConst.JsonFilePath))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, animals, typeof(BaseAnimal));
                }


                await AnimalsHelper.EncryptFile(AnimalDbConst.JsonFilePath, AnimalDbConst.JsonEncryptedFilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("There was an error updating animal list");
                Console.WriteLine(e);
                throw;
            }
        }

        public static List<BaseAnimal> GetAnimalsOfType<T>(T type)
        {
            var allAnimals = GetAnimals().Result;

            var desiredAnimalTypeList = allAnimals.FindAll(s => s.GetType().Name == type.GetType().Name);

            return desiredAnimalTypeList;
        }

        public static void AddAnimal(BaseAnimal animal)
        {
            var allAnimals = GetAnimals().Result;
            allAnimals.Add(animal);
            UpdateAnimals(allAnimals);
        }

        public static List<BaseAnimal> GetAllAliveAnimals(BaseAnimal filterType)
        {
            List<BaseAnimal> animals;

            if (filterType != null)
            {
                animals = GetAnimalsOfType(filterType);
            }
            else
            {
                animals = GetAnimals().Result; 
            }

            return animals;
        }
    }
}