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
        public static void InitAnimals() => AllAnimals.InitAnimalsList();
        public static List<BaseAnimal> GetAnimals() => AllAnimals.Animals;
        public static List<BaseAnimal> GetAnimalsOfType<T>(T type)
        {
            List<BaseAnimal> AllAnimals = GetAnimals();

            List<BaseAnimal> DesiredAnimalTypeList = AllAnimals.FindAll(s => s.GetType() == type.GetType());

            return DesiredAnimalTypeList;
        }
        public static void AddAnimal(BaseAnimal animal)
        {
            AllAnimals.Animals.Add(animal);
        }
    }
}
