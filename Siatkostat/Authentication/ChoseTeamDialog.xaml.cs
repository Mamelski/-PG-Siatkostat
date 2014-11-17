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
            ChoseTeamListView.ItemsSource = TeamsViewModel.Instance.TeamsCollection;

            if (ChoseTeamListView.ItemsSource != null) 
                return;

            TeamsViewModel.Instance.CollectionLoaded += TeamsViewModel_CollectionLoaded;
            Loaded += ChoseTeamDialog_Loaded;
            Closing += ChoseTeamDialog_Closing;
        }

       

        #endregion

        #region Properties
        public Team SelectedTeam { get; set; }
        #endregion

        #region ContentDialog Events
        void ChoseTeamDialog_Loaded(object sender, RoutedEventArgs e)
        {
            if (ChoseTeamListView.ItemsSource != null)
                return;

            ProgresIndicator.Text = "Trwa łączenie z bazą danych";
            ProgresIndicator.ShowAsync();
        }

        void ChoseTeamDialog_Closing(Windows.UI.Xaml.Controls.ContentDialog sender, Windows.UI.Xaml.Controls.ContentDialogClosingEventArgs args)
        {
            Loaded -= ChoseTeamDialog_Loaded;
        }

        void TeamsViewModel_CollectionLoaded(object sender)
        {
            ChoseTeamListView.ItemsSource = TeamsViewModel.Instance.TeamsCollection;
            ProgresIndicator.HideAsync();
        }
        #endregion

        #region Buttons Events
        private void GotoweButton_Click(object sender, RoutedEventArgs e)
        {
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
