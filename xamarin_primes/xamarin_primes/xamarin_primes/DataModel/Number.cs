using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace xamarin_primes.DataModel
{
    public class Number : INotifyPropertyChanged
    {
        public Number()
        {
            _value = 0;
            _isPrime = false;
        }
        public Number(Int64 value, bool isPrime)
        {
            this._value = value;
            this._isPrime = isPrime;
        }
        public Number(string valueString, bool isPrime = false)
        {
            Int64 num = 0;

            Int64.TryParse(valueString, out num);
            _value = num;
            _isPrime = isPrime;
        }

        private Int64 _value;
        public Int64 Value
        {
            get { return _value; }
            set
            {
                if (_value == value) return;
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        private bool _isPrime;
        public bool IsPrime
        {
            get { return _isPrime; }
            set
            {
                if (_isPrime == value) return;
                _isPrime = value;
                OnPropertyChanged("IsPrime");
            }
        }

        public ObservableCollection<Int64> PrimeFactors { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
