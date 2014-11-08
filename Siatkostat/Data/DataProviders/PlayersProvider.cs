﻿using System;
using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices;
using Siatkostat.Data.DataModels;

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
            RefreshPlayers();
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