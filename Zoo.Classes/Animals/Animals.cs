using System;
using System.Collections.Generic;
using System.Text;

namespace Zoo.Classes.Animals
{
    public class AllAnimals
    {
        public static void InitAnimalsList() => Animals = new List<BaseAnimal>();
        public static List<BaseAnimal> Animals { get; set; }
    }

    // Monkey Animal
    public sealed class Monkey : BaseAnimal
    {
         
    }

    // Lion Animal
    public sealed class Lion : BaseAnimal
    {

    }

    // Elephant Animal
    public sealed class Elephant : BaseAnimal
    {

    }
}
