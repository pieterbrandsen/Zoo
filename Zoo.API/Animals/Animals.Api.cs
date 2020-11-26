using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Zoo.Constants;
using Zoo.Models.Animals;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Windows;

namespace Zoo.API.Animals
{
    /// <summary>
    /// Animal Api
    /// This is used to get all animal related logic
    /// </summary>
    public static class AnimalsApi
    {
        public static void InitAnimals()
        {
            if (!File.Exists(AnimalDbConst.JsonFilePath))
            {
                List<BaseAnimal> animals = new List<BaseAnimal>();
                string jsonArray = JsonConvert.SerializeObject(animals, Formatting.Indented);
                File.WriteAllText(AnimalDbConst.JsonFilePath, jsonArray);
            }
            EncryptFile(AnimalDbConst.JsonFilePath, AnimalDbConst.JsonEncryptedFilePath);
        }
        public static List<BaseAnimal> GetAnimals()
        {
            DecryptFile(AnimalDbConst.JsonEncryptedFilePath, AnimalDbConst.JsonFilePath);

            // Get the file from local machine
            string JsonFile = File.ReadAllText(AnimalDbConst.JsonFilePath);
            List<BaseAnimal> animalList = JsonConvert.DeserializeObject<List<BaseAnimal>>(JsonFile, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            });
            EncryptFile(AnimalDbConst.JsonFilePath, AnimalDbConst.JsonEncryptedFilePath);
            return animalList;
        }
        public static void UpdateAnimals(List<BaseAnimal> animals)
        {
            DecryptFile(AnimalDbConst.JsonEncryptedFilePath, AnimalDbConst.JsonFilePath);
            List<BaseAnimal> animalList = animals;

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.TypeNameHandling = TypeNameHandling.Auto;
            serializer.Formatting = Formatting.Indented;

            using (StreamWriter sw = new StreamWriter(AnimalDbConst.JsonFilePath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, animalList, typeof(BaseAnimal));
            }

            EncryptFile(AnimalDbConst.JsonFilePath, AnimalDbConst.JsonEncryptedFilePath);
        }
        public static List<BaseAnimal> GetAnimalsOfType<T>(T type)
        {
            List<BaseAnimal> AllAnimals = GetAnimals();

            List<BaseAnimal> DesiredAnimalTypeList = AllAnimals.FindAll(s => s.GetType() == type.GetType());

            return DesiredAnimalTypeList;
        }
        public static void AddAnimal(BaseAnimal animal)
        {
            List<BaseAnimal> AllAnimals = GetAnimals();
            AllAnimals.Add(animal);

            UpdateAnimals(AllAnimals);
        }

        ///<summary>
        /// Steve Lydford - 12/05/2008.
        ///
        /// Encrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        private static void EncryptFile(string inputFile, string outputFile)
        {

            try
            {
                string password = @"akey123"; // Your Key Here
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);


                fsIn.Close();
                cs.Close();
                fsCrypt.Close();

                File.Delete(inputFile);
            }
            catch
            {
                MessageBox.Show("Encryption failed!", "Error");
            }
        }
        ///<summary>
        /// Steve Lydford - 12/05/2008.
        ///
        /// Decrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        private static void DecryptFile(string inputFile, string outputFile)
        {

            {
                string password = @"akey123"; // Your Key Here

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);

                fsOut.Close();
                cs.Close();
                fsCrypt.Close();

                File.Delete(inputFile);
            }
        }
    }
}
