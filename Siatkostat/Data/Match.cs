using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siatkostat
{
    class Match
    {
        public int ID { get; private set; }

        public int TeamAID { get; private set; }

        public int TeamBID { get; private set; }

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

    }
}
