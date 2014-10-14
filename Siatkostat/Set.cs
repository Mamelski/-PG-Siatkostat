using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siatkostat
{
    class Set
    {
        public int ID { get; private set; }

        public int MatchID { get; private set; }

        public int SetNumber { get; private set; }

        public int BlockFault { get; private set; }

        public int BlockKill { get; private set; }

        public int BlockRebound { get; private set; }

        public int OwnFault { get; private set; }

        public int PlayerID { get; private set; }

        public int ReceiveBad { get; private set; }

        public int ReceiveFault { get; private set; }

        public int ReceiveGood { get; private set; }

        public int ReceivePerfect { get; private set; }

        public int ServeAce { get; private set; }

        public int ServeFault { get; private set; }

        public int ServeHit { get; private set; }

        public int ServeOther { get; private set; }

        public int SpikeBlocked { get; private set; }

        public int SpikeFault { get; private set; }

        public int SpikeKill { get; private set; }

        public int SpikeOther { get; private set; }

    }
}
