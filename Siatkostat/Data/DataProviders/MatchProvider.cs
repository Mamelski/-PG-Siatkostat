using System;
using System.Collections.Generic;
using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices;
using Siatkostat.Data.DataModels;

namespace Siatkostat.Data.DataProviders
{
    class MatchProvider
    {
        //public MobileServiceCollection<Match, Match> MatchCollection { get; set; } 
        public List<Match> MatchCollection { get; set; } 

        #region Constructor
        public MatchProvider()
        {
            MatchCollection = new List<Match>();
            MatchCollection.Add(new Match());
        }
        #endregion
    }
}
