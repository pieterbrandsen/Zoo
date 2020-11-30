namespace Zoo.Models.Animals
{
    // Monkey Animal
    public sealed class Monkey : BaseAnimal
    {
        public Monkey()
        {

        }

        public Monkey(int monkeyAmount)
        {
            SetName($"Monkey-{monkeyAmount}");
            SetEnergy(60);
        }
        public override void Eat(int energyAmount) => AddEnergy(10);
        public override void UseEnergy(int energyAmount) => RemoveEnergy(2);
    }

    // Lion Animal
    public sealed class Lion : BaseAnimal
    {
        public Lion()
        {
        }

        public Lion(int lionAmount)
        {
            SetName($"Lion-{lionAmount}");
            SetEnergy(100);
        }
        public override void Eat(int energyAmount) => AddEnergy(25);
        public override void UseEnergy(int energyAmount) => RemoveEnergy(10);
    }

    // Elephant Animal
    public sealed class Elephant : BaseAnimal
    {
        public Elephant()
        {
        }

        public Elephant(int elephantAmount)
        {
            SetName($"Elephant-{elephantAmount}");
            SetEnergy(100);
        }
        public override void Eat(int energyAmount) => AddEnergy(50);
        public override void UseEnergy(int energyAmount) => RemoveEnergy(5);
    }
}