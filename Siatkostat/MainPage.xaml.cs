using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Siatkostat.Statistics;

namespace Siatkostat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {

            InitializeComponent();
            
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            if (App.SelectedTeam == null)
            {
                EditTeamButton.Visibility = Visibility.Collapsed;
            }
        }
      

        private void MatchStartButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewMatch));
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }

        private void DisplayStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SelectCriterion));
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            Application.Current.Exit();
        }

        private void EditTeamButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditTeam.EditTeamPage));
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        
        }
    }
}
