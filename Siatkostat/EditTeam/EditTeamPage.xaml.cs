using System;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Navigation;
using Siatkostat.Models;
using Siatkostat.ViewModels;

namespace Siatkostat.EditTeam
{
    public sealed partial class EditTeamPage
    {
        #region Fields
        public StatusBarProgressIndicator ProgresIndicator = StatusBar.GetForCurrentView().ProgressIndicator;

        private readonly NewPlayerContentDialog newPlayerContentDialog = new NewPlayerContentDialog();

        #endregion

        #region Constructor
        public EditTeamPage()
        {
            InitializeComponent();

            PlayersViewModel.Instance.CollectionLoaded += PlayersViewModel_CollectionLoaded;
            Loaded += EditTeam_Loaded;
        }
        #endregion

        #region Events
        async void EditTeam_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (PlayersListBox.ItemsSource != null)
                return;

            ProgresIndicator.Text = "Trwa łączenie z bazą danych";
            await ProgresIndicator.ShowAsync();
        }
       
        async void PlayersViewModel_CollectionLoaded(object sender)
        {
            PlayersListBox.ItemsSource = PlayersViewModel.Instance.PlayersCollection;
            await ProgresIndicator.HideAsync();
        }

        async void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            await ProgresIndicator.HideAsync();
            Frame.Navigate(typeof(MainPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter as Player != null)
            {
                PlayersViewModel.Instance.ModifyPlayer((Player)e.Parameter);
            }
            PlayersListBox.ItemsSource = PlayersViewModel.Instance.PlayersCollection;
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }
        #endregion
       

        private void EdytujButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (PlayersListBox.SelectedItem == null)
            {
                return;
            }

            Frame.Navigate(typeof (EditPlayerPage),PlayersListBox.SelectedItem);

        }


        private async void UsuńButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (PlayersListBox.SelectedItem == null)
            {
                var editTeamErrorDialog = new MessageDialog("Wybierz zawodnika, którego chcesz usunąć") { Title = "Błąd edycji" };
                await editTeamErrorDialog.ShowAsync();
                return;
            }

            PlayersViewModel.Instance.DeletePlayer((Player)PlayersListBox.SelectedItem);
        }

        private async void DodajButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            newPlayerContentDialog.Closing += newPlayerContentDialog_Closing;
            await newPlayerContentDialog.ShowAsync();
        }

        void newPlayerContentDialog_Closing(Windows.UI.Xaml.Controls.ContentDialog sender, Windows.UI.Xaml.Controls.ContentDialogClosingEventArgs args)
        {
            if (newPlayerContentDialog.NewPlayer == null)
            {
                return;
            }
            newPlayerContentDialog.Closing -= newPlayerContentDialog_Closing;
            PlayersViewModel.Instance.InsertPlayer(newPlayerContentDialog.NewPlayer);
        }

    }
}