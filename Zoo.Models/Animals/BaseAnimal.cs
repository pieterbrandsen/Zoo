namespace Zoo.Models.Animals
{
    public abstract class BaseAnimal
    {
        private string Name { get; set; }

        private int Energy { get; set; }

        // Name of animal
        public string GetName()
        {
            return Name;
        }

        public string SetName(string newName)
        {
            return Name = newName;
        }

        // Energy amount of animal
        public int GetEnergy()
        {
            return Energy;
        }

        public int SetEnergy(int newEnergy)
        {
            return Energy = newEnergy;
        }

        public int AddEnergy(int newEnergy)
        {
            return Energy += newEnergy;
        }

        public int RemoveEnergy(int newEnergy)
        {
            return Energy -= newEnergy;
        }

        // Eat function
        // This will add 25 energy to the animal every time its called
        public void Eat()
        {
            AddEnergy(25);
        }
    }
}