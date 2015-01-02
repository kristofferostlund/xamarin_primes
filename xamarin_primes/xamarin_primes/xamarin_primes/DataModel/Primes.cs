using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace xamarin_primes.DataModel
{
    public static class Primes
    {
        public static bool IsSetup { get; private set; }

        static Primes()
        {
            IsSetup = false;
            setup();
        }

        private static async void setup()
        {
            IsSetup = false;
            if (await doSetup())
            {
                IsSetup = true;
                OnSetupComplete();
            }
        }

        private static async Task<bool> doSetup()
        {
            PrimeNumbers = await SetupPrimes();
            return true;
        }

        private static ObservableCollection<Int64> _primeNumbers = new ObservableCollection<Int64>();
        public static ObservableCollection<Int64> PrimeNumbers
        {
            get { return _primeNumbers; }
            set { _primeNumbers = value; }
        }

        public static event EventHandler SetupComplete;
        private static void OnSetupComplete()
        {
            EventHandler handler = SetupComplete;
            if (handler != null) handler(null, EventArgs.Empty);
        }

        private static async Task<ObservableCollection<Int64>> SetupPrimes()
        {
            var collection = new ObservableCollection<Int64>();
            

            if (PrimeNumbers.Count < 1)
            {
                _primeNumbers = new ObservableCollection<Int64>();
            }

            collection.Add(2);
            collection.Add(3);

            await Task.Run(() =>
            {
                Int64 number = 3;
                while (collection.Count < 10000)
                {
                    number += 2;
                    if (PrivateIsPrime(number, collection)) collection.Add(number);
                }
                return collection;
            });
            return collection;
        }

        private static bool PrivateIsPrime(Int64 number, Collection<Int64> collection)
        {
            if (number == 2) return true;
            if (number%2 == 0) return false;

            return IsPrimeCheck(number, collection);
        }

        private static bool IsPrimeCheck(Int64 number, Collection<Int64> collection)
        {
            if (collection.Contains(number)) return true;
            if (collection.Count > 1)
            {
                if (number < collection[collection.Count - 1])
                    return false;
            }

            for (int i = 0; i < collection.Count; i++)
            {
                if ((number%collection[i] == 0))
                    return false;
            }

            if (collection.Count <= 0) return true;
            for (var i = collection[collection.Count - 1] + 2; i < (int) Math.Sqrt(number) + 1; i += 2)
            {
                if (number%i == 0)
                    return false;
            }
            return true;
        } 

        public static bool IsPrime(Int64 number)
        {
            return PrivateIsPrime(number, PrimeNumbers);
        }

        public static List<Int64> GetDivisors(Int64 num)
        {
            if (num == 0) return new List<Int64>{0};
            if (num == 1) return new List<Int64> { 1 };

            var list = new List<Int64>();
            

            var offset = 1L;
            if (num < 0) offset = -1L;

            var tempNum = num * offset;

            while (tempNum > 0)
            {
                if (IsPrime(tempNum))
                {
                    list.Add(tempNum);
                    tempNum = 0;
                }
                else
                {
                    var divisorFound = false;

                    TryFindDivisorInList(list, ref tempNum, ref divisorFound, offset);
                    if (divisorFound) 
                        continue;

                    tempNum = TryFindDivisorAboveList(list, tempNum * offset);
                }
            }
            return list;
        }

        private static long TryFindDivisorAboveList(List<long> list, long tempNum)
        {
            for (var i = PrimeNumbers[PrimeNumbers.Count - 1]; i < (int)Math.Sqrt(tempNum) + 1; i += 2)
            {
                if (tempNum % i == 0)
                {
                    list.Add(i);
                    tempNum = tempNum / i;
                    if (tempNum == 1) tempNum = 0;
                    break;
                }
            }
            return tempNum;
        }

        private static void TryFindDivisorInList(List<Int64> list, ref long tempNum, ref bool divisorFound, Int64 offset)
        {
            for (int i = 0; i < PrimeNumbers.Count; i++)
            {
                if (tempNum % PrimeNumbers[i] == 0)
                {
                    list.Add(PrimeNumbers[i] * offset);
                    tempNum = tempNum / PrimeNumbers[i];
                    if (tempNum == 1) tempNum = 0;
                    divisorFound = true;
                    break;
                }
            }
        }
    }
}
