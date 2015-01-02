using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using xamarin_primes.DataModel;
using xamarin_primes.Pages;

namespace xamarin_primes
{
    public class App
    {
        public static Page GetMainPage()
        {
            return new MainPage();
        }

    }
}
