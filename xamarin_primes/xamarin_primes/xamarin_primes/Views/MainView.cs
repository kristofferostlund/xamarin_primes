using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using xamarin_primes.DataModel;

namespace xamarin_primes.Views
{
    public class MainView : ContentView
    {
        private SetupView setupView;
        private PrimeFinderView primeFinderView;

        private Animation animation;

        public MainView()
        {
            Primes.SetupComplete += Primes_SetupComplete;

            setupView = new SetupView();

            Content = setupView;

        }

        private Entry oldEntry;
        private Label oldLabel;
        private Label oldSLabel;
        private Grid oldGrid;
        private void OldSetup()
        {
            oldEntry = new Entry()
            {
                Keyboard = Keyboard.Numeric,
                Placeholder = "Please input an integer",
                IsEnabled = Primes.IsSetup
            };
            oldEntry.TextChanged += entry_TextChanged;

            oldLabel = new Label()
            {
                Text = "Please wait until the first 10,000 primes are being added",
                TextColor = Color.Accent
            };

            oldSLabel = new Label();

            oldGrid = new Grid()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                ColumnDefinitions =
                {
                    new ColumnDefinition(){ Width = new GridLength(1, GridUnitType.Star)}
                },
                RowSpacing = 2,
                RowDefinitions =
                {
                    new RowDefinition() {Height = GridLength.Auto},
                    new RowDefinition() {Height = GridLength.Auto},
                    new RowDefinition() {Height = GridLength.Auto}
                }
            };
            oldGrid.Children.Add(oldEntry, 0, 0);
            oldGrid.Children.Add(oldLabel, 0, 1);
            oldGrid.Children.Add(oldSLabel, 0, 2);
            Content = oldGrid;
        }

        async void Primes_SetupComplete(object sender, EventArgs e)
        {
            await setupView.AnimateOut();

            primeFinderView = new PrimeFinderView();
            Content = primeFinderView;
        }

        void entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Int64 num = 0;
            if (!Int64.TryParse((sender as Entry).Text, out num))
            {
                oldLabel.Text = string.Format("Please input an integer");
                return;
            }


            //label.Text = num.ToString();

            oldLabel.Text = string.Format(Primes.IsPrime(num) ? "{0} is a prime." : "{0} is actually not a prime.", num);
            
            oldSLabel.Text = Primes.PrimeNumbers.LongCount().ToString() + "\r\n" + Primes.PrimeNumbers.Last().ToString();
        }
    }
}
