using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556
using Microsoft.WindowsAzure.MobileServices;
using Siatkostat.Data.DataModels;
using Siatkostat.Data.DataProviders;

namespace Siatkostat
{
    public sealed partial class EditTeam
    {
        private PlayersProvider playersProvider;

        public EditTeam()
        {
            InitializeComponent();

            playersProvider = new PlayersProvider();
            playersProvider.CollectionLoaded += playersProvider_CollectionLoaded;
        }

        void playersProvider_CollectionLoaded(object sender)
        {
            ListItems.ItemsSource = playersProvider.PlayerCollection;
        }


        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

    }
}