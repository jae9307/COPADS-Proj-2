using System.Numerics;
using System.Security.Cryptography;

namespace PrimeGenerator
{
    class Generator
    {
        private static Boolean IsProbablyPrime(BigInteger bigInteger, int k = 10)
        {
            BigInteger n = bigInteger - 1;
            int r = 1;
            BigInteger d = 0;
            Console.WriteLine($"big int: {bigInteger}");
            while (true)
            {
                int two_raised = (int)Math.Pow(2, r);
                if ((n % two_raised) == 0)
                {
                    d = bigInteger / two_raised;
                    break;
                }
                r++;
            }
            Console.WriteLine($"d: {d.ToString()}");
            return true;
        }
        
        private static void generateNumbers(int num_bytes)
        {
            byte[] array = new byte[num_bytes];
            RandomNumberGenerator.Create().GetBytes(array);
            BigInteger bigInteger = new BigInteger(array);
            if (bigInteger % 2 == 0 || bigInteger <= 3) {
                generateNumbers(num_bytes);
                return;
            }
            IsProbablyPrime(bigInteger);
        }
        
        public static void Main(string[] args)
        {
            int bits = int.Parse(args[0]);
            int amount_to_generate = int.Parse(args[1]);

            int num_bytes = bits / 8;
            Parallel.For(0, amount_to_generate, count =>
            {
                Generator.generateNumbers(num_bytes);
            });
        }
    }
}