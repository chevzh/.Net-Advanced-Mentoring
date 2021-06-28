using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] salt = new byte[16];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(salt);
            }

            string password = "testPassword123testPassword123testPassword123testPassword123testPassword123";

            //GetTime(GeneratePasswordHashUsingSalt, "GeneratePasswordHashUsingSalt", password, salt);

            var iterate = 10000;
            var pbkdf2 = new CustomRfc2898DeriveBytes(password, salt, iterate);

            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            var passwordHash = Convert.ToBase64String(hashBytes);

        }

        public static string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
        {

            var iterate = 10000; 
            var pbkdf2 = new CustomRfc2898DeriveBytes(passwordText, salt, iterate); 

            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36]; 
            
            Array.Copy(salt, 0, hashBytes, 0, 16); 
            Array.Copy(hash, 0, hashBytes, 16, 20);

            var passwordHash = Convert.ToBase64String(hashBytes);

            return passwordHash;
        }

        private static void GetTime(Func<string, byte[], string> methodToCall, string methodName, string password, byte[] salt)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string hashedPass = methodToCall(password, salt);
            sw.Stop();

            System.Console.WriteLine($"{methodName}: {sw.ElapsedMilliseconds} ms");
        }
    }
}
