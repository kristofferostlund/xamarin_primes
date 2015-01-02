using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using xamarin_primes.Views;

namespace xamarin_primes.Pages
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            Content = new MainView();
        }
    }
}
