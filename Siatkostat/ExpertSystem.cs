using Siatkostat.Models;

namespace Siatkostat
{
    public static class ExpertSystem
    {

        public static bool NeedSubstitution(Set statistics)
        {
            int score = 0;
            double serveRate = ServeRate(statistics);
            double receiveRate = ReceiveRate(statistics);
            double spikeRate = SpikeRate(statistics);
            double blockRate = BlockRate(statistics);
            double faultRate = FaultRate(statistics);

            if (statistics.TotalServes() >= 3)
            {
                if (serveRate < 0.16)
                    score -= 2;
                else if (serveRate >= 0.35)
                    score += 2;
            }
            if (statistics.TotalReceives() >= 3)
            {
                if (receiveRate < 0.2)
                    score -= 3;
                else if (receiveRate >= 0.6)
                    score += 2;
            }
            if (statistics.TotalSpikes() >= 3)
            {
                if (spikeRate < 0.2)
                    score -= 2;
                else if (spikeRate >= 0.45)
                    score += 3;
            }
            if (statistics.TotalBlocks() >= 3)
            {
                if (blockRate < 0.15) 
                    score -= 1;
                else if (blockRate >= 0.37)
                    score += 2;
            }
            if (statistics.TotalBlocks() + statistics.TotalFaults() + statistics.TotalReceives() +
                statistics.TotalServes() + statistics.TotalSpikes() >= 12)
            {
                if (faultRate < 0.2)
                    score += 1;
                else if (faultRate >= 0.45)
                    score -= 3;
            }

            return score < 0;
        }

        public static double BlockRate(Set statistics)
        {
            double divider = (4*statistics.BlockFault + statistics.BlockKill +
                              statistics.BlockRebound);

            if (divider.Equals(0))
                return 0.0;

            return (statistics.BlockKill + 0.3 * statistics.BlockRebound) / 
                divider;
        }

        public static double SpikeRate(Set statistics)
        {
            double divider = (4*statistics.SpikeFault + statistics.SpikeKill + statistics.SpikeOther +
                              statistics.SpikeBlocked);

            if (divider.Equals(0))
                return 0.0;

            return (statistics.SpikeKill + 0.4 * statistics.SpikeOther + 0.2 * statistics.SpikeBlocked) / 
                divider;
        }

        public static double ReceiveRate(Set statistics)
        {
            double divider = (5 * statistics.ReceiveFault + statistics.ReceivePerfect + statistics.ReceiveGood + 
                statistics.ReceiveBad);

            if (divider.Equals(0))
                return 0.0;

            return (statistics.ReceivePerfect + 0.7*statistics.ReceiveGood + 0.3*statistics.ReceiveBad)/
                   divider;

        }

        public static double ServeRate(Set statistics)
        {
            double divider = (2*statistics.ServeFault + statistics.ServeAce + statistics.ServeHit +
                              statistics.ServeOther);

             if (divider.Equals(0))
                return 0.0;

            return (statistics.ServeAce + 0.5 * statistics.ServeHit + 0.1 * statistics.ServeOther) /
                           divider;
        }

        public static double FaultRate(Set statistics)
        {
            double divider = (statistics.OwnFault + statistics.ServeFault + statistics.ServeHit +
                              statistics.ServeOther + statistics.ReceiveFault +
                              statistics.ReceivePerfect +
                              statistics.ReceiveGood + statistics.ReceiveBad + statistics.SpikeFault +
                              statistics.SpikeKill + statistics.SpikeOther + statistics.SpikeBlocked +
                              statistics.BlockFault + statistics.BlockKill + statistics.BlockRebound);
            
            if (divider.Equals(0))
                return 0.0;

            return (statistics.OwnFault + statistics.ServeFault + statistics.ReceiveFault +
                statistics.SpikeFault + statistics.BlockFault) / divider;
        }
    }
}
