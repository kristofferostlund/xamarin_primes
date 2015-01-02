using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using xamarin_primes.DataModel;

namespace xamarin_primes.Views
{
    public class PrimeFinderView : ContentView
    {
        private Entry numberEntry;
        private Button checkButton;
        private BoxView separatorOne;
        private Label numberLabel;
        private Label resultLabel;
        private BoxView separatorTwo;
        private Label factorTitleLabel;
        private Label factorsLabel;
        private Grid grid;

        private ActivityIndicator activityIndicator;

        public PrimeFinderView()
        {
            InitializeContent();
            
            Content = grid;
        }

        private void InitializeContent()
        {
            numberEntry = new Entry()
            {
                Placeholder = "Input an integer",
                WidthRequest = 400,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Keyboard = Keyboard.Numeric
            };
            numberEntry.TextChanged += numberEntry_TextChanged;

            checkButton = new Button()
            {
                Text = "Check",
                WidthRequest = 250,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            checkButton.Clicked += checkButton_Clicked;

            separatorOne = new BoxView()
            {
                HeightRequest = 2,
                WidthRequest = 400,
                Color = Color.Accent,
                HorizontalOptions = LayoutOptions.Center
            };

            // Get a tapped event for this one
            numberLabel = new Label()
            {
                Text = "Waiting for input",
                Font = Font.BoldSystemFontOfSize(36),
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Center
            };
            resultLabel = new Label()
            {
                Text = "Also waiting for input",
                Font = Font.SystemFontOfSize(36),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center
            };

            separatorTwo = new BoxView()
            {
                HeightRequest = 2,
                WidthRequest = 400,
                Color = Color.Accent,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            factorTitleLabel = new Label()
            {
                Text = "Prime factors:",
                Font = Font.BoldSystemFontOfSize(32),
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Center
            };

            factorsLabel = new Label()
            {
                Text = "Prime factors would be present if there'd be a number input",
                Font = Font.SystemFontOfSize(32),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };

            activityIndicator = new ActivityIndicator()
            {
                IsRunning = false
                //IsVisible = false
            };

            grid = new Grid()
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
                    new RowDefinition() {Height = new GridLength(2, GridUnitType.Star)},
                    new RowDefinition() {Height = new GridLength(2, GridUnitType.Star)},
                    new RowDefinition() {Height = GridLength.Auto},
                    new RowDefinition() {Height = new GridLength(2, GridUnitType.Star)},
                    new RowDefinition() {Height = GridLength.Auto},
                    new RowDefinition() {Height = new GridLength(2, GridUnitType.Star)},
                    new RowDefinition() {Height = GridLength.Auto},
                    new RowDefinition() {Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition() {Height = new GridLength(3, GridUnitType.Star)}
                }
            };

            grid.Children.Add(numberEntry, 0, 0);
            grid.Children.Add(checkButton, 0, 1);
            
            grid.Children.Add(separatorOne, 0, 2);

            grid.Children.Add(numberLabel, 0, 3);
            grid.Children.Add(activityIndicator, 0, 4);
            grid.Children.Add(resultLabel, 0, 5);

            grid.Children.Add(separatorTwo, 0, 6);

            grid.Children.Add(factorTitleLabel, 0, 7);
            grid.Children.Add(factorsLabel, 0, 8);

        }

        void numberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;

            if (entry != null && entry.Text.Length > 18)
            {
                entry.Text = entry.Text.Remove(entry.Text.Length - 1);
            }
        }

        async void checkButton_Clicked(object sender, EventArgs e)
        {
            activityIndicator.IsRunning = true;
            //activityIndicator.IsVisible = true;

            var num = 0L;
            if (Int64.TryParse(numberEntry.Text, out num))
            {
                    numberLabel.Text = num.ToString();
                if (Primes.IsPrime(num))
                {
                    resultLabel.Text = "Is a prime number.";
                    factorsLabel.Text = string.Join(" x ", 1, num);
                }
                else
                {
                    resultLabel.Text = "Is a composite number.";

                    var output = await Task.Run(() => string.Join(" x ", Primes.GetDivisors(num)));

                    factorsLabel.Text = output;
                }
            }
            activityIndicator.IsRunning = false;
            //activityIndicator.IsVisible = false;
        }
    }
}
