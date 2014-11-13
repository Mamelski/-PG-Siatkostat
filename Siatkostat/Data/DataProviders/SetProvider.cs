using System;
using System.Collections.Generic;
using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices;
using Siatkostat.Data.DataModels;

namespace Siatkostat.Data.DataProviders
{
    class SetProvider
    {
        //public MobileServiceCollection<Set, Set> SetCollection { get; set; }
        public List<Set> SetCollection { get; set; }

        #region Constructor
        public SetProvider()
        {
            SetCollection = new List<Set>();
            const int players = 8;
            const int sets = 5;

            for(int p = 1; p <= players; p++)
            {
                for(int s = 1; s <= sets; s++)
                {
                    SetCollection.Add(new Set { PlayerID = p, ID = p * 10 + s, MatchID = 1, SetNumber = s });
                }
            }
        }
        #endregion
    }
}
