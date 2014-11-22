using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Siatkostat.ViewModels;

namespace Siatkostat
{
    public sealed partial class MainMatch : Page
    {
        public MainMatch()
        {
            this.InitializeComponent();

            Court.SetPlayersOnCourt(PlayersViewModel.Instance.PlayersOnCourt);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ActionStat));
        }

        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StatisticsWindow));
        }

        private void SubstitutionButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (CourtPlayersSelect));
        }

    }
}
