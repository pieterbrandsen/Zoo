using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Zoo.Constants;
using Zoo.Models.Animals;

namespace Zoo.API.Animals
{
    /// <summary>
    /// Animal Api
    /// This is used to get all animal related logic
    /// </summary>
    public static class AnimalsApi
    {
        public static void InitAnimals() {
            if (!File.Exists(AnimalDbConst.JsonFilePath))
            {
                List<BaseAnimal> animals = new List<BaseAnimal>();
                string jsonArray = JsonConvert.SerializeObject(animals, Formatting.Indented);
                File.WriteAllText(AnimalDbConst.JsonFilePath, jsonArray);
            }
        }
        public static List<BaseAnimal> GetAnimals() 
        {
            // Get the file from local machine
            string JsonFile = File.ReadAllText(AnimalDbConst.JsonFilePath);
            List<BaseAnimal> animalList = JsonConvert.DeserializeObject<List<BaseAnimal>>(JsonFile, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            });

            return animalList;
        }

        public static void UpdateAnimals(List<BaseAnimal> animals)
        {
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
        }
        public static List<BaseAnimal> GetAnimalsOfType<T>(T type)
        {
            List<BaseAnimal> AllAnimals = GetAnimals();

            List<BaseAnimal> DesiredAnimalTypeList = AllAnimals.FindAll(s => s.GetType() == type.GetType());

            return DesiredAnimalTypeList;
        }
        public static void AddAnimal(BaseAnimal animal)
        {
            List<BaseAnimal> AllAnimals = GetAnimals();
            AllAnimals.Add(animal);

            UpdateAnimals(AllAnimals);
        }
    }
}
