using System;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556
using Siatkostat.Models;
using Siatkostat.ViewModels;

namespace Siatkostat.Authentication
{
    public sealed partial class ChoseTeamDialog
    {
        #region Fields
        public StatusBarProgressIndicator ProgresIndicator = StatusBar.GetForCurrentView().ProgressIndicator;
        #endregion

        #region Constructor
        public ChoseTeamDialog()
        {
            InitializeComponent();

            TeamsViewModel.Instance.CollectionLoaded += TeamsViewModel_CollectionLoaded;
            Loaded += ChoseTeamDialog_Loaded;
            Closing += ChoseTeamDialog_Closing;
        }

       

        #endregion

        #region Properties
        public Team SelectedTeam { get; set; }
        #endregion

        #region Events
        async void ChoseTeamDialog_Loaded(object sender, RoutedEventArgs e)
        {
            TeamsViewModel.Instance.RefreshTeams();
            if (ChoseTeamListView.ItemsSource != null)
                return;

            ProgresIndicator.Text = "Trwa łączenie z bazą danych";
            await ProgresIndicator.ShowAsync();
        }

        void ChoseTeamDialog_Closing(Windows.UI.Xaml.Controls.ContentDialog sender, Windows.UI.Xaml.Controls.ContentDialogClosingEventArgs args)
        {
            Loaded -= ChoseTeamDialog_Loaded;
        }

        async void TeamsViewModel_CollectionLoaded(object sender)
        {
            ChoseTeamListView.ItemsSource = TeamsViewModel.Instance.TeamsCollection;
            await ProgresIndicator.HideAsync();
        }

        #endregion

        #region Buttons
        private async void GotoweButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChoseTeamListView.SelectedItem == null)
            {
                var selectTeamErrorDialog = new MessageDialog("Wybierz drużynę") { Title = "Błąd wyboru" };
                await selectTeamErrorDialog.ShowAsync();
                return;
            }

            SelectedTeam = (Team) ChoseTeamListView.SelectedItem;
            Hide();
        }

        private void AnulujButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedTeam = null;
            Hide();
        }
        #endregion
    }
}
