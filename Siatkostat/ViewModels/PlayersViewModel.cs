using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<Player> PlayersOnCourt = new ObservableCollection<Player>();
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

        public void SetPlayersOnCourt(IEnumerable<Player> players)
        {
            PlayersOnCourt.Clear();
            foreach (var p in players)
            {
                PlayersOnCourt.Add(p);
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
            
            foreach (var playerToModify in PlayersCollection.Where(player1 => player1.Id.ToString() == player.Id.ToString()))
            {
                playerToModify.IsLibero = player.IsLibero;
                playerToModify.FirstName = player.FirstName;
                playerToModify.LastName = player.LastName;
                playerToModify.Number = player.Number;
                await playersTable.UpdateAsync(playerToModify);
                return;
            }
        }

        #endregion
    }
}
