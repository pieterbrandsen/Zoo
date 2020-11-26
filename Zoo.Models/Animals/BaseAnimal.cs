using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Zoo.Models.Animals
{
    public abstract class BaseAnimal
    {
        // Name of animal
        public string GetName() => Name;
        public string SetName(string newName) => Name = newName;
        private string Name { get; set; }

        // Energy amount of animal
        public int GetEnergy() => Energy;
        public int SetEnergy(int newEnergy) => Energy = newEnergy;
        public int AddEnergy(int newEnergy) => Energy += newEnergy;
        public int RemoveEnergy(int newEnergy) => Energy -= newEnergy;
        private int Energy { get; set; }

        // Eat function
        // This will add 25 energy to the animal every time its called
        public void Eat() => AddEnergy(25);
    }
}
