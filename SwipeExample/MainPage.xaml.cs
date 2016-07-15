using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace SwipeExample
{
    /// <author>Ludovic Roland</author>
    /// <date>2016.07.15</date>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var list = new List<string>();
            for(var i = 0; i <= 100; i++)
            {
                list.Add(i.ToString());
            }

            DataContext = list;
        }

        private void Rectangle_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Row2.ActualHeight : " + Row2.ActualHeight);

            if (Row2.ActualHeight > 70)
            {
                var newHeight = Row1.ActualHeight + e.Delta.Translation.Y;
                Row1.Height = new GridLength(newHeight > 0 ? newHeight : 0);
            }
            else if(e.Delta.Translation.Y < 0) // we try to go to the top
            {
                Row1.Height = new GridLength(Row1.ActualHeight + e.Delta.Translation.Y);
            }
        }

        private void Rectangle_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            var screenHeight = Window.Current.Bounds.Height;
            var percent = Row2.ActualHeight * 100 / screenHeight;

            //if Row2 > 75% of the screen --> 100%
            if(percent >= 75)
            {
                Row1.Height = new GridLength(0);
            }
            //if Row2 < 75% && > 25% --> initial state
            else if(percent < 75 && percent > 25)
            {
                Row1.Height = new GridLength(70);
            }
            //if Row2 < 25% of the screen --> minimize
            else
            {
                Row1.Height = new GridLength(screenHeight - 70);
            }
        }
    }
}
