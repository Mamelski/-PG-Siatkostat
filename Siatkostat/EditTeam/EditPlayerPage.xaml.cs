using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Siatkostat.Models;

namespace Siatkostat.EditTeam
{

    public sealed partial class EditPlayerPage
    {
        #region Fields
        private Player editedPlayer;

        private Player newPlayer;
        
        #endregion

        #region Constructor
        public EditPlayerPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Navigation
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            editedPlayer = (Player)e.Parameter;
            if (editedPlayer == null)
            {
                return;
            }

            ImięTextBox.Text = editedPlayer.FirstName;
            NazwiskoTextBox.Text = editedPlayer.LastName;
            NumerTextBox.Text = editedPlayer.Number.ToString();

            if (editedPlayer.IsLibero)
            {
                IsLIberoToggleButton.IsChecked = true;
            }
        }

        #endregion


        private async void GotoweButton_Click(object sender, RoutedEventArgs e)
        {
            if (ImięTextBox.Text == string.Empty
                || NazwiskoTextBox.Text == string.Empty
                || NumerTextBox.Text == string.Empty)
            {
                newPlayer = null;
                var addPlayerErrorDialog = new MessageDialog("Wypełnij wszystkie pola") { Title = "Błąd tworzenia zawodnika" };
                await addPlayerErrorDialog.ShowAsync();
                return;
            }

            int tmp;
            if (!Int32.TryParse(NumerTextBox.Text, out tmp))
            {
                newPlayer = null;
                var addPlayerErrorDialog = new MessageDialog("Pole \"Numer\" musi być liczbą") { Title = "Błąd tworzenia zawodnika" };
                await addPlayerErrorDialog.ShowAsync();
                return;
            }

            newPlayer = new Player
            {
                Id = editedPlayer.Id,
                FirstName = ImięTextBox.Text,
                LastName = NazwiskoTextBox.Text,
                Number = tmp,
                TeamId = App.SelectedTeam.Id
            };

            if (IsLIberoToggleButton.IsChecked == true)
            {
                editedPlayer.IsLibero = true;
            }


            Frame.Navigate(typeof(EditTeamPage), newPlayer);
        }

        private void AnulujButton_Click(object sender, RoutedEventArgs e)
        {
            editedPlayer = null;
            Frame.Navigate(typeof(EditTeamPage), editedPlayer);
        }
    }
}
