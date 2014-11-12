using System;
using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices;
using Siatkostat.Data.DataModels;

namespace Siatkostat.Data.DataProviders
{
    class TeamsProvider
    {
        public MobileServiceCollection<Team, Team> TeamCollection { get; set; }

        #region Constructor
        public TeamsProvider()
        {
            TeamCollection.Add(new Team { ID = 1, Name = "Pierwsza drużyna" });
            TeamCollection.Add(new Team { ID = 2, Name = "Druga drużyna" });
        }
        #endregion


    }
}
