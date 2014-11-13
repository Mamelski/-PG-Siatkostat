using System;
using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices;
using Siatkostat.Data.DataModels;
using Siatkostat.Models;

namespace Siatkostat.Data.DataProviders
{
    class TeamsProvider
    {
        public MobileServiceCollection<Team, Team> TeamCollection { get; set; }

        #region Constructor
        public TeamsProvider()
        {
            TeamCollection.Add(new Team { Id = 1, TeamName = "Pierwsza drużyna" });
            TeamCollection.Add(new Team { Id = 2, TeamName = "Druga drużyna" });
        }
        #endregion


    }
}
