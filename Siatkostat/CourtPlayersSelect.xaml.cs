using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Siatkostat.Models;
using Siatkostat.ViewModels;

namespace Siatkostat
{
    public sealed partial class CourtPlayersSelect : Page
    {
        private ObservableCollection<Player> Players = new ObservableCollection<Player>(); 

        private PlayerControl SelectedPlayer = null;
        public CourtPlayersSelect()
        {
            this.InitializeComponent();

            if (PlayersViewModel.Instance.PlayersOnCourt.Count == 0)
            {
                PlayersViewModel.Instance.CollectionLoaded += PlayersViewModel_CollectionLoaded;
            }
            else
            {
                Court.SetPlayersOnCourt(PlayersViewModel.Instance.PlayersOnCourt);
                foreach (var p in PlayersViewModel.Instance.PlayersCollection.Except(PlayersViewModel.Instance.PlayersOnCourt))
                {
                    Players.Add(p);
                }
                PlayersListBox.ItemsSource = Players;
            }
        }

        private void PlayersViewModel_CollectionLoaded(object sender)
        {
            foreach (var p in PlayersViewModel.Instance.PlayersCollection)
            {
                Players.Add(p);
            }
            PlayersListBox.ItemsSource = Players;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void CourtControl_OnOnPlayerSelect(object sender)
        {
            PlayerControl selected = sender as PlayerControl;
            if (selected == null) return;
            SelectedPlayer = selected;

            if (PlayersListBox.SelectedItem == null) return;

            // Set player on court
            if (selected.player != null)
            {
                // restore player to listbox
                Players.Add(selected.player);
            }
            selected.player = PlayersListBox.SelectedItem as Player;

            // Remove selected player from ListBox
            Players.Remove(PlayersListBox.SelectedItem as Player);

            // Unselect after changes
            PlayersListBox.SelectedItem = null;
            SelectedPlayer = null;
        }

        private void PlayersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.SelectedPlayer == null
                || PlayersListBox.SelectedItem == null) return;

            // Set player on court
            if (this.SelectedPlayer.player != null)
            {
                // restore player to listbox
                Players.Add(this.SelectedPlayer.player);
            }
            this.SelectedPlayer.player = PlayersListBox.SelectedItem as Player;

            // Remove selected player from ListBox
            Players.Remove(PlayersListBox.SelectedItem as Player);

            // Unselect after changes
            PlayersListBox.SelectedItem = null;
            this.SelectedPlayer = null;
        }

        private async void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            if (Court.Players.Any(p => p.player == null))
            {
                await new MessageDialog("Nie wszystkie pozycje mają przypisanego zawodnika!").ShowAsync();
                return;
            }
            if (!Court.ValidatePositions())
            {
                await new MessageDialog("Libero nie może znajdować się w pierwszej linii!").ShowAsync();
                return;
            }

            PlayersViewModel.Instance.SetPlayersOnCourt(Court.Players.Select(p => p.player));
            Frame.Navigate(typeof (MainMatch));
        }
    }
}
