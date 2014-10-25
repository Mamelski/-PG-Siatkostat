using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Siatkostat.Authentication
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignInPage
    {
        public SignInPage()
        {
            //var x = new WebServiceOperations();
            //try
            //{
            //    x.CheckPlayerTable();
            //}
            //catch(Exception ex)
            //{
            //    int s = 0;
            //}
            InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }


        private void OnClickRegitsration(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditTeam));
        }
    }
}
