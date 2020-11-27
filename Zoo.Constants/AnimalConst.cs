using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Zoo.Constants
{
    /// <summary>
    /// All Animal types
    /// </summary>
    public class AnimalsTypesNamesConst
    {
        public const string MONKEY = "Monkey";
        public const string LION = "Lion";
        public const string ELEPHANT = "Elephant";
    }

    public class AnimalDbConst
    {
        public static string JsonFilePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/animalDb2.json";
        public static string JsonEncryptedFilePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/animalDbEncrypted2.json";
    }
}
