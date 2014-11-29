using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Siatkostat.Models;

namespace Siatkostat.Authentication
{
    public sealed partial class AddTeamDialog
    {

        #region Fields
        public Team NewTeam;
        #endregion

        #region Constructor
        public AddTeamDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region Buttons
        private async void GotoweButton_Click(object sender, RoutedEventArgs e)
        {
            if (NazwaTextBox.Text == string.Empty
                || HasłoTextBox.Password == string.Empty
                || PowtórzHasłoTextBox.Password == string.Empty)
            {
                NewTeam = null;
                var addTeamErrorDialog = new MessageDialog("Wypełnij wszystkie pola") { Title = "Błąd tworzenia drużyny" };
                await addTeamErrorDialog.ShowAsync();
                return;
            }

            if (!HasłoTextBox.Password.Equals(PowtórzHasłoTextBox.Password))
            {
                NewTeam = null;
                var addTeamErrorDialog = new MessageDialog("Hasła do siebie nie pasują") { Title = "Błąd tworzenia drużyny" };
                await addTeamErrorDialog.ShowAsync();
                return;  
            }

            NewTeam = new Team
            {
                TeamName = NazwaTextBox.Text,
                TeamPassword = HasłoTextBox.Password
            };
            Hide();   
        }

        private void AnulujButton_Click(object sender, RoutedEventArgs e)
        {
            NewTeam = null;
            Hide();
        }
        #endregion
    }
}
