using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Siatkostat.Models;

namespace Siatkostat.EditTeam
{
    public sealed partial class NewPlayerContentDialog
    {
        #region Fields
        public Player NewPlayer;
        #endregion

        #region Constructor
        public NewPlayerContentDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region Buttons
        private async void GotoweButton_Click(object sender, RoutedEventArgs e)
        {
            if (ImięTextBox.Text == string.Empty
                || NazwiskoTextBox.Text == string.Empty
                || NumerTextBox.Text == string.Empty)
            {
                NewPlayer = null;
                var addPlayerErrorDialog = new MessageDialog("Wypełnij wszystkie pola") { Title = "Błąd tworzenia zawodnika" };
                await addPlayerErrorDialog.ShowAsync();
                return;
            }

            int tmp;
            if (!Int32.TryParse(NumerTextBox.Text, out tmp))
            {
                NewPlayer = null;
                var addPlayerErrorDialog = new MessageDialog("Pole \"Numer\" musi być liczbą") { Title = "Błąd tworzenia zawodnika" };
                await addPlayerErrorDialog.ShowAsync();
                return;
            }
            NewPlayer = new Player
            {
                FirstName = ImięTextBox.Text,
                LastName = NazwiskoTextBox.Text,
                Number = tmp
            };
            if (App.SelectedTeam != null)
                NewPlayer.TeamId = App.SelectedTeam.Id;

            if (IsLIberoToggleButton.IsChecked == true)
            {
                NewPlayer.IsLibero = true;
            }


            Hide();   
        }

        private void AnulujButton_Click(object sender, RoutedEventArgs e)
        {
            NewPlayer = null;
            Hide();
        }
        #endregion
    }
}
