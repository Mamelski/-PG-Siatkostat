using System;

namespace Siatkostat
{
    class Match
    {
        public string ID { get; private set; }

        public string TeamAID { get; private set; }

        public string TeamBID { get; private set; }

        public DateTime MatchDate { get; private set; }

        public int MatchDuration { get; private set; }

        public int TeamAScoreInSet1 { get; private set; }

        public int TeamAScoreInSet2 { get; private set; }
        
        public int TeamAScoreInSet3 { get; private set; }
        
        public int TeamAScoreInSet4 { get; private set; }
        
        public int TeamAScoreInSet5 { get; private set; }
        
        public int TeamBScoreInSet1 { get; private set; }
        
        public int TeamBScoreInSet2 { get; private set; }
        
        public int TeamBScoreInSet3 { get; private set; }
        
        public int TeamBScoreInSet4 { get; private set; }
        
        public int TeamBScoreInSet5 { get; private set; }


        #region Constructor
        public Match()
        {
            ID = "1";
            TeamAID = "1";
            TeamBID = "2";
            TeamAScoreInSet1 = TeamAScoreInSet2 = TeamAScoreInSet3 = TeamAScoreInSet4 = TeamAScoreInSet5 = 0;
            TeamBScoreInSet1 = TeamBScoreInSet2 = TeamBScoreInSet3 = TeamBScoreInSet4 = TeamBScoreInSet5 = 0;
            MatchDate = new DateTime(2014, 5, 5);
        }
        #endregion
    }
}
