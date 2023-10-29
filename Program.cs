using System.Numerics;
using System.Security.Cryptography;

namespace PrimeGenerator
{
    class Generator
    {
        private static Boolean rLoop(int r, BigInteger n, BigInteger x, BigInteger bigInteger)
        {
            for (int j = 0; j <= r - 2; j++)
            {
                x = BigInteger.ModPow(x, (BigInteger)2, bigInteger);
                //Console.WriteLine($"x2: {x.ToString()}");
                if (x.Equals(n))
                {
                    //Console.WriteLine("Second continue");
                    return true;
                }
            }
            return false;
        }
        
        private static Boolean IsProbablyPrime(BigInteger bigInteger, int k = 10)
        {
            BigInteger n = bigInteger - 1;
            int r = 1;
            BigInteger d = 0;
            while (true)
            {
                int two_raised = (int)Math.Pow(2, r);
                if ((n % two_raised) != 0)
                {
                    d = bigInteger / (two_raised/2);
                    r--;
                    Console.WriteLine($"divided:  {(n/(two_raised / 2)).ToString()}");
                    break;
                }
                r++;
            }
            Console.WriteLine($"r:  {r.ToString()}");
            Console.WriteLine($"d:  {d.ToString()}");

            for (int i = 0; i <= k-1; i++)
            {
                byte[] array = new byte[bigInteger.GetByteCount()];
                RandomNumberGenerator.Create().GetBytes(array);
                BigInteger a = new BigInteger(array);
                a = BigInteger.Abs(a);
                a = a % (bigInteger - 2);
                Console.WriteLine($"a:  {a.ToString()}");
                if (a < 2)
                {
                    a += 2;
                }
                BigInteger x = BigInteger.ModPow(a, d, bigInteger);
                //Console.WriteLine($"x1: {x.ToString()}");
                if (x.IsOne || x.Equals(n))
                {
                    //Console.WriteLine("First continue");
                    continue;
                }
                if (rLoop(r, n, x, bigInteger))
                {
                    continue;
                }
                return false;
            }
            return true;
        }
        
        private static void generateNumbers(int num_bytes)
        {
            /*CancellationTokenSource cts = new CancellationTokenSource();
            ParallelOptions parallelOptions = new ParallelOptions()
            {
                CancellationToken = cts.Token
            };
            Parallel.For(0, 50, parallelOptions, i =>
            {*/
            byte[] array = new byte[num_bytes];
            RandomNumberGenerator.Create().GetBytes(array);
            BigInteger bigInteger = new BigInteger(array);
            bigInteger = BigInteger.Abs(bigInteger);
            //BigInteger bigInteger = 236360317;
            Console.WriteLine($"b:  {bigInteger.ToString()}");
            if (bigInteger % 2 == 0 || bigInteger <= 3) {
                generateNumbers(num_bytes);
                return;
            }
            Console.WriteLine(IsProbablyPrime(bigInteger));
            //})
            
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