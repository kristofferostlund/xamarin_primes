using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace xamarin_primes.Views
{
    public class SetupView : ContentView
    {
        private Label startingLabel;
        private ActivityIndicator startingActivityIndicator;
        private Label startingTitleLabel;
        private Grid startingGrid;

        public SetupView()
        {
            InitializeContent();
            IntializeGrid();

            PopulateGrid();

            Content = startingGrid;
        }

        private void PopulateGrid()
        {
            startingGrid.Children.Add(startingLabel, 0, 0);
            startingGrid.Children.Add(startingActivityIndicator, 0, 1);
            startingGrid.Children.Add(startingTitleLabel, 0, 2);
        }

        private void IntializeGrid()
        {
            startingGrid = new Grid()
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
                    new RowDefinition() {Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition() {Height = new GridLength(2, GridUnitType.Star)}
                }
            };
        }

        private void InitializeContent()
        {
            startingLabel = new Label()
            {
                Text = "Please wait while the app is setting up.",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Font = Font.SystemFontOfSize(36)
            };

            startingActivityIndicator = new ActivityIndicator()
            {
                IsRunning = true,
                IsVisible = true
            };

            startingTitleLabel = new Label()
            {
                Text = "IS THIS PRIME?",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Font = Font.BoldSystemFontOfSize(36)
            };
        }

        public async Task AnimateOut()
        {
            await Task.Delay(1000);

            Rectangle oldBounds = startingLabel.Bounds;
            Rectangle newBounds = new Rectangle(new Point(new Size(oldBounds.X, -oldBounds.Height)), oldBounds.Size);

            Rectangle oldTitleBounds = startingTitleLabel.Bounds;
            Rectangle newTitleBounds = new Rectangle(new Point(new Size(oldTitleBounds.X, oldTitleBounds.Height * 5)), oldTitleBounds.Size);

            startingLabel.LayoutTo(newBounds, 1000, Easing.CubicOut);
            startingActivityIndicator.IsVisible = false;
            startingActivityIndicator.IsRunning = false;
            await startingTitleLabel.LayoutTo(newTitleBounds, 800, Easing.CubicOut);

            return;
        }
    }
}
