using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Zoo.Helper.Animals
{
    public class AnimalsHelper
    {
        public const int iterations = 1042; // Recommendation is >= 1000.

        // Rfc2898DeriveBytes constants:
        public static readonly byte[]
            salt =
            {
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            }; // Must be at least eight bytes.  MAKE THIS SALTIER!

        /// <summary>Decrypt a file.</summary>
        /// <remarks>
        ///     NB: "Padding is invalid and cannot be removed." is the Universal CryptoServices error.  Make sure the
        ///     password, salt and iterations are correct before getting nervous.
        /// </remarks>
        /// <param name="sourceFilename">The full path and name of the file to be decrypted.</param>
        /// <param name="destinationFilename">The full path and name of the file to be output.</param>
        public static Task DecryptFile(string sourceFilename, string destinationFilename)
        {
            var password = "141342421";

            var aes = new AesManaged();
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            // NB: Rfc2898DeriveBytes initialization and subsequent calls to   GetBytes   must be eactly the same, including order, on both the encryption and decryption sides.
            var key = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);
            aes.Mode = CipherMode.CBC;
            var transform = aes.CreateDecryptor(aes.Key, aes.IV);

            using (var destination =
                new FileStream(destinationFilename, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                using (var cryptoStream = new CryptoStream(destination, transform, CryptoStreamMode.Write))
                {
                    try
                    {
                        using (var source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read,
                            FileShare.Read))
                        {
                            source.CopyTo(cryptoStream);
                        }
                    }
                    catch (CryptographicException exception)
                    {
                        if (exception.Message == "Padding is invalid and cannot be removed.")
                            throw new ApplicationException(
                                "Universal Microsoft Cryptographic Exception (Not to be believed!)", exception);
                        throw;
                    }
                }
            }

            File.Delete(sourceFilename);

            return Task.CompletedTask;
        }

        /// <summary>Encrypt a file.</summary>
        /// <param name="sourceFilename">The full path and name of the file to be encrypted.</param>
        /// <param name="destinationFilename">The full path and name of the file to be output.</param>
        public static Task EncryptFile(string sourceFilename, string destinationFilename)
        {
            var password = "141342421";

            var aes = new AesManaged();
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            // NB: Rfc2898DeriveBytes initialization and subsequent calls to   GetBytes   must be eactly the same, including order, on both the encryption and decryption sides.
            var key = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);
            aes.Mode = CipherMode.CBC;
            var transform = aes.CreateEncryptor(aes.Key, aes.IV);

            using (var destination =
                new FileStream(destinationFilename, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                using (var cryptoStream = new CryptoStream(destination, transform, CryptoStreamMode.Write))
                {
                    using (var source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        source.CopyTo(cryptoStream);
                    }
                }
            }

            File.Delete(sourceFilename);

            return Task.CompletedTask;
        }
    }
}