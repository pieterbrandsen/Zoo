using System;
using System.Collections.Generic;
using System.Text;
using Zoo.Classes.Animals;

namespace Zoo.API.Animals
{
    public static class AnimalsApi
    {
        public static void InitAnimals() => AllAnimals.InitAnimalsList();
        public static List<BaseAnimal> GetAnimals() => AllAnimals.Animals;

        public static void AddAnimal(BaseAnimal animal)
        {
            AllAnimals.Animals.Add(animal);
        } 
    }
}
