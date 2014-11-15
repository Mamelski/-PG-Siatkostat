using System;
using System.Collections.Generic;
using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices;
using Siatkostat.Data.DataModels;
using Siatkostat.Models;

namespace Siatkostat.Data.DataProviders
{
    class TeamsProvider
    {
        //public MobileServiceCollection<Team, Team> TeamCollection { get; set; }
        public List<Team> TeamCollection { get; set; }

        #region Constructor
        public TeamsProvider()
        {
            TeamCollection.Add(new Team { TeamName = "Pierwsza drużyna" });
            TeamCollection.Add(new Team { TeamName = "Druga drużyna" });
        }
        #endregion


    }
}
