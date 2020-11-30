using System;
using System.Collections.Generic;

namespace Zoo.Models.Animals
{
    public abstract class BaseAnimal
    {
        public string Name { get; set; }

        public int Energy { get; set; }

        // Name of animal
        public string GetName()
        {
            return Name;
        }

        protected void SetName(string newName)
        {
            Name = newName;
        }

        // Energy amount of animal
        public int GetEnergy()
        {
            return Energy;
        }

        protected void SetEnergy(int newEnergy)
        {
            Energy = newEnergy;
        }

        protected int AddEnergy(int newEnergy)
        {
            return Energy += newEnergy;
        }

        protected int RemoveEnergy(int newEnergy)
        {
            return Energy -= newEnergy;
        }

        // Eat function
        // This will add <EnergyAmount> energy to the animal every time its called
        public abstract void Eat(int energyAmount);

        // Use Energy function
        // This will use <EnergyAmount> energy and remove it from the animal
        public abstract void UseEnergy(int energyAmount);
    }

    public class AnimalDb
    {
        public List<AnimalDbFields> List {get;set;}

    }
    public class AnimalDbFields
    {
        public string Name { get; set; }
        public int Energy { get; set; }
    }
}