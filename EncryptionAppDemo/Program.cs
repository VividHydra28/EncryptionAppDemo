using System.Security.Cryptography;
using System.Threading.Tasks;

namespace EncryptionAppDemo
{
    internal class Program
    {
        
        static async Task Main(string[] args)
        {

            var plaintext = await File.ReadAllTextAsync("plaintext.txt");

            var masterKey = RandomNumberGenerator.GetBytes(32);

            var encrypted = Encrypt(plaintext, masterKey);

            /*
           var encryption = new Encryption();
            var decrypted = encryption.Decrypt("EncryptedString");
            Console.WriteLine(decrypted);
            Console.ReadKey();
            */
        }

        const int iVSize = 16; // AES block size in bytes

        static string Encrypt(string plaintext, byte[] masterKey)
        {
            using var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = masterKey;
            aes.IV = RandomNumberGenerator.GetBytes(iVSize);


            using var memoryStream = new MemoryStream();
            memoryStream.Write(aes.IV, 0, iVSize);
            /*
            using (var aes = Aes.Create())
            {
                aes.Key = masterKey;
                aes.GenerateIV();
                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    using (var ms = new MemoryStream())
                    {
                        ms.Write(aes.IV, 0, aes.IV.Length);
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (var sw = new StreamWriter(cs))
                            {
                                sw.Write(plaintext);
                            }
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            } 
            */

            return Convert.ToBase64String(memoryStream.ToArray());
        }
    }
}
