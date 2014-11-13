using System;
using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices;
using Siatkostat.Data.DataModels;

using Siatkostat.Models;

namespace Siatkostat.Data.DataProviders
{
    public class PlayersProvider
    {
        #region Events

        public delegate void PlayerCollectionLoaded(object sender);

        public event PlayerCollectionLoaded CollectionLoaded;

        protected virtual void OnCollectionLoaded()
        {
            if (CollectionLoaded != null)
                CollectionLoaded(this);
        }

        #endregion

        #region DataObjects

        private readonly IMobileServiceTable<Player> playerTable = App.MobileService.GetTable<Player>();

        public MobileServiceCollection<Player, Player> PlayerCollection { get; set; }

        #endregion

        #region Constructor

        public PlayersProvider()
        {
            //RefreshPlayers();
            PlayerCollection.Add(new Player { Id = "1", FirstName = "Krzychu", LastName = "Dlsvbx", IsLibero = false, Number = 1, TeamId = "1" });
            PlayerCollection.Add(new Player { Id = "2", FirstName = "Rychu", LastName = "Bsdfxz", IsLibero = false, Number = 2, TeamId = "1" });
            PlayerCollection.Add(new Player { Id = "3", FirstName = "Zbychu", LastName = "Tsdgsdgxcvx", IsLibero = false, Number = 3, TeamId = "1" });
            PlayerCollection.Add(new Player { Id = "4", FirstName = "Zdzichu", LastName = "Zgsdfbs", IsLibero = false, Number = 4, TeamId = "1" });
            PlayerCollection.Add(new Player { Id = "5", FirstName = "Drugirychu", LastName = "Gsdgasdf", IsLibero = false, Number = 5, TeamId = "1" });
            PlayerCollection.Add(new Player { Id = "6", FirstName = "Tumek", LastName = "WEfdgxcb", IsLibero = false, Number = 6, TeamId = "1" });
            PlayerCollection.Add(new Player { Id = "7", FirstName = "Mikus", LastName = "bBssdf", IsLibero = true, Number = 7, TeamId = "1" });
            PlayerCollection.Add(new Player { Id = "8", FirstName = "Leszek", LastName = "Wsdfgfxcsdf", IsLibero = false, Number = 8, TeamId = "1" });
        }

        #endregion

        #region DataOperations
        public async void InsertPlayer(Player player)
        {
            await playerTable.InsertAsync(player);
            PlayerCollection.Add(player);
        }

        public async void DeletePlayer(Player player)
        {
            await playerTable.DeleteAsync(player);
            PlayerCollection.Remove(player);
        }

        public async void ModifyPlayer(Player player, int index)
        {
            await playerTable.UpdateAsync(player);
            PlayerCollection[index] = player;
        }

        #endregion

        public async void RefreshPlayers()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                PlayerCollection = await playerTable
                    //.Where(player => player.teamID == team.ID)
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
    }
}