using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Zoo.Constants.Animals
{
    public class AnimalDbConst
    {
        public static string JsonFilePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/path.txt";
    }
}
