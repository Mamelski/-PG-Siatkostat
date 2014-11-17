﻿using System;
using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices;
using Siatkostat.Models;

namespace Siatkostat.ViewModels
{
    public class TeamsViewModel
    {
        #region Lazy singleton elements
        private static readonly Lazy<TeamsViewModel> LazyInstance =  new Lazy<TeamsViewModel>(() => new TeamsViewModel());

        public static TeamsViewModel Instance
        {
            get
            {
                return LazyInstance.Value;
            }
        }

        #endregion

        #region Events
        public delegate void TeamsCollectionLoaded (object sender);

        public event TeamsCollectionLoaded CollectionLoaded;

        protected virtual void OnCollectionLoaded()
        {
            if (CollectionLoaded != null)
                CollectionLoaded(this);
        }

        #endregion

        #region Data Objects
        private readonly IMobileServiceTable<Team> teamsTable  = App.MobileService.GetTable<Team>();

        public MobileServiceCollection<Team, Team> TeamsCollection { get; set; }

        #endregion

        #region Constructor
        private TeamsViewModel()
        {
            RefreshPlayers();
        }

        #endregion

        public async void RefreshPlayers()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                TeamsCollection = await teamsTable
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