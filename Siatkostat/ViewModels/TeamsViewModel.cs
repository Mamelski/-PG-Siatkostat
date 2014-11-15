using System;
using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices;
using Siatkostat.Models;

namespace Siatkostat.ViewModels
{
    public class TeamsViewModel
    {
        private readonly IMobileServiceTable<Team> teamTable  = App.MobileService.GetTable<Team>();

        public MobileServiceCollection<Team, Team> PlayerCollection { get; set; }

        public TeamsViewModel()
        {
            RefreshPlayers();
        }

        public async void RefreshPlayers()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                PlayerCollection = await teamTable
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
        }
    }
}
