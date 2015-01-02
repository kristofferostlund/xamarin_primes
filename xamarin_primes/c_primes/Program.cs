using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xamarin_primes.DataModel;

namespace c_primes
{ 
    class Program
    {
        static void Main(string[] args)
        {
            Primes.SetupComplete += Primes_SetupComplete;

            while (true)
            {
                Console.WriteLine("Input a positive integer. Or write 'exit' or 'close' to close.");

                Int64 num = 0;

                var inpt = Console.ReadLine();
                if (Int64.TryParse(inpt, out num))
                {
                    if (Primes.IsPrime(num))
                    {
                        Console.WriteLine("{0} is a prime number!", num);
                    }
                    else
                    {
                        Console.WriteLine("{0} is a composite number", num);

                        var divisors = Primes.GetDivisors(num);
                        Console.WriteLine(string.Join(", ", divisors));
                    }
                }
                else if (inpt.ToLower() == "close" || inpt.ToLower() == "exit")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please, only input integers.\r\n");
                }
            }
        }

        static void Primes_SetupComplete(object sender, EventArgs e)
        {
            Console.WriteLine(Primes.PrimeNumbers.Last());
        }
    }
}
