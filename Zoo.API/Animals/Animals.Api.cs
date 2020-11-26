using System;
using System.Collections.Generic;
using System.Text;
using Zoo.Classes.Animals;

namespace Zoo.API.Animals
{
    /// <summary>
    /// Animal Api
    /// This is used to get all animal related logic
    /// </summary>
    public static class AnimalsApi
    {
        public static void InitAnimals() => AnimalMockDB.InitAnimalsList();
        public static List<BaseAnimal> GetAnimals() => AnimalMockDB.Animals;
        public static List<BaseAnimal> GetAnimalsOfType<T>(T type)
        {
            List<BaseAnimal> AllAnimals = GetAnimals();

            List<BaseAnimal> DesiredAnimalTypeList = AllAnimals.FindAll(s => s.GetType() == type.GetType());

            return DesiredAnimalTypeList;
        }
        public static void AddAnimal(BaseAnimal animal)
        {
            AnimalMockDB.Animals.Add(animal);
        }
    }
}
