using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siatkostat
{
    public class Set
    {
        public int ID { get; set; }

        public int MatchID { get; set; }

        public int SetNumber { get; set; }

        public int BlockFault { get; set; }

        public int BlockKill { get; set; }

        public int BlockRebound { get; set; }

        public int OwnFault { get; set; }

        public int PlayerID { get; set; }

        public int ReceiveBad { get; set; }

        public int ReceiveFault { get; set; }

        public int ReceiveGood { get; set; }

        public int ReceivePerfect { get; set; }

        public int ServeAce { get; set; }

        public int ServeFault { get; set; }

        public int ServeHit { get; set; }

        public int ServeOther { get; set; }

        public int SpikeBlocked { get; set; }

        public int SpikeFault { get; set; }

        public int SpikeKill { get; set; }

        public int SpikeOther { get; set; }

        #region Constructor
        public Set()
        {
            BlockFault = BlockKill = BlockRebound = OwnFault = ReceiveBad = ReceiveFault = ReceiveGood = ReceivePerfect = ServeAce = ServeFault
                = ServeHit = ServeOther = SpikeBlocked = SpikeFault = SpikeKill = SpikeOther = 0;
        }
        #endregion

    }
}
