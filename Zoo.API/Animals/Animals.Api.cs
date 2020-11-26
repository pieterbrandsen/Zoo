using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Zoo.Models.Animals;
using Zoo.Constants.Animals;

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
                AnimalDb AnimalDb = new AnimalDb();
                AnimalDb.AnimalList = new List<BaseAnimal>();
                string jsonArray = JsonConvert.SerializeObject(AnimalDb);
                File.WriteAllText(AnimalDbConst.JsonFilePath, jsonArray);
            }
        }
            //AnimalMockDB.InitAnimalsList();
        public static List<BaseAnimal> GetAnimals() 
        {
            // Get the file from local machine
            string JsonFile = File.ReadAllText(AnimalDbConst.JsonFilePath);
            
            return JsonConvert.DeserializeObject<AnimalDb>(JsonFile).AnimalList;
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

            // Get the file from local machine

            AnimalDb animalDb = new AnimalDb();
            animalDb.AnimalList = AllAnimals;
            string jsonArray = JsonConvert.SerializeObject(animalDb);

            File.WriteAllText(AnimalDbConst.JsonFilePath, jsonArray);
        }
    }
}
