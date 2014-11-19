using System;
using System.Linq;
using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices;
using Siatkostat.Models;

namespace Siatkostat.ViewModels
{
    public class PlayersViewModel
    {
        #region Lazy singleton elements
        private static readonly Lazy<PlayersViewModel> LazyInstance = new Lazy<PlayersViewModel>(() => new PlayersViewModel());

        public static PlayersViewModel Instance
        {
            get
            {
                return LazyInstance.Value;
            }
        }

        #endregion

        #region Events
        public delegate void PlayersCollectionLoaded(object sender);

        public event PlayersCollectionLoaded CollectionLoaded;

        protected virtual void OnCollectionLoaded()
        {
            if (CollectionLoaded != null)
                CollectionLoaded(this);
        }

        #endregion

        #region Data Objects
        private readonly IMobileServiceTable<Player> playersTable = App.MobileService.GetTable<Player>();

        public MobileServiceCollection<Player, Player> PlayersCollection { get; set; }
        #endregion

        #region Constructor

        private PlayersViewModel()
        {
            RefreshPlayers();
        }

        #endregion

        public async void RefreshPlayers()
        {
            if (App.SelectedTeam == null)
            {
                return;
            }

            MobileServiceInvalidOperationException exception = null;
            PlayersCollection = null;
            try
            {
                PlayersCollection = await playersTable
                    .Where(player => player.TeamId.ToString() == App.SelectedTeam.Id.ToString())
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                OnCollectionLoaded();
            }
        }


        #region DataOperations
        public async void InsertPlayer(Player player)
        {
            await playersTable.InsertAsync(player);
            PlayersCollection.Add(player);
        }

        public async void DeletePlayer(Player player)
        {
            await playersTable.DeleteAsync(player);
            PlayersCollection.Remove(player);
        }

        public async void ModifyPlayer(Player player)
        {
            
            foreach (var player1 in PlayersCollection.Where(player1 => player1.Id.ToString() == player.Id.ToString()))
            {
                player1.IsLibero = player.IsLibero;
                player1.FirstName = player.FirstName;
                player1.LastName = player.LastName;
                player1.Number = player.Number;
                await playersTable.UpdateAsync(player1);
                return;
            }
        }

        #endregion
    }
}
