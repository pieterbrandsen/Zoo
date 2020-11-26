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

    // Monkey
    public class Monkey : BaseAnimal
    {
         
    }

    // Lion
    public class Lion : BaseAnimal
    {

    }

    // Elephant
    public class Elephant : BaseAnimal
    {

    }
}
