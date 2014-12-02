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
        private Player playerToEdit;

        private Player playerAfterEdition;
        
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
            playerToEdit = (Player)e.Parameter;
            if (playerToEdit == null)
            {
                return;
            }

            ImięTextBox.Text = playerToEdit.FirstName;
            NazwiskoTextBox.Text = playerToEdit.LastName;
            NumerTextBox.Text = playerToEdit.Number.ToString();

            if (playerToEdit.IsLibero)
            {
                IsLIberoToggleButton.IsChecked = true;
            }
        }

        #endregion

        #region Buttons
        private async void GotoweButton_Click(object sender, RoutedEventArgs e)
        {
            if (ImięTextBox.Text == string.Empty
                || NazwiskoTextBox.Text == string.Empty
                || NumerTextBox.Text == string.Empty)
            {
                playerAfterEdition = null;
                var addPlayerErrorDialog = new MessageDialog("Wypełnij wszystkie pola") { Title = "Błąd tworzenia zawodnika" };
                await addPlayerErrorDialog.ShowAsync();
                return;
            }

            int tmp;
            if (!Int32.TryParse(NumerTextBox.Text, out tmp))
            {
                playerAfterEdition = null;
                var addPlayerErrorDialog = new MessageDialog("Pole \"Numer\" musi być liczbą") { Title = "Błąd tworzenia zawodnika" };
                await addPlayerErrorDialog.ShowAsync();
                return;
            }

            playerAfterEdition = new Player
            {
                Id = playerToEdit.Id,
                FirstName = ImięTextBox.Text,
                LastName = NazwiskoTextBox.Text,
                Number = tmp,
            };

            if (App.SelectedTeam != null)
                playerAfterEdition.TeamId = App.SelectedTeam.Id;

            if (IsLIberoToggleButton.IsChecked == true)
            {
                playerAfterEdition.IsLibero = true;
            }


            Frame.Navigate(typeof(EditTeamPage), playerAfterEdition);
        }

        private void AnulujButton_Click(object sender, RoutedEventArgs e)
        {
            playerToEdit = null;
            Frame.Navigate(typeof(EditTeamPage), playerToEdit);
        }
        #endregion
    }
}
