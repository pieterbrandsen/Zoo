namespace Zoo.Models.Animals
{
    // Monkey Animal
    public sealed class Monkey : BaseAnimal
    {
        public Monkey()
        {

        }

        public Monkey(int number)
        {
            SetName($"Monkey-{number}");
            SetEnergy(60);
        }
        public override void Eat() => AddEnergy(10);
        public override void UseEnergy() => RemoveEnergy(2);
    }

    // Lion Animal
    public sealed class Lion : BaseAnimal
    {
        public Lion()
        {
        }

        public Lion(int number)
        {
            SetName($"Lion-{number}");
            SetEnergy(100);
        }
        public override void Eat() => AddEnergy(25);
        public override void UseEnergy() => RemoveEnergy(10);
    }

    // Elephant Animal
    public sealed class Elephant : BaseAnimal
    {
        public Elephant()
        {
        }

        public Elephant(int number)
        {
            SetName($"Elephant-{number}");
            SetEnergy(100);
        }
        public override void Eat() => AddEnergy(50);
        public override void UseEnergy() => RemoveEnergy(5);
    }
}