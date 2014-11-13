using System;
using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices;
using Siatkostat.Data.DataModels;

namespace Siatkostat.Data.DataProviders
{
    class MatchProvider
    {
        public MobileServiceCollection<Match, Match> MatchCollection { get; set; }

        #region Constructor
        public MatchProvider()
        {
            MatchCollection.Add(new Match());
        }
        #endregion
    }
}
