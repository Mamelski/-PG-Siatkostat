using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Documents;
using Siatkostat.Models;

namespace Siatkostat
{
    public static class ExpertSystem
    {

        public static bool NeedSubstitution(Set playerStatistics)
        {
            int score = 0;
            double serveRate = ServeRate(playerStatistics);
            double receiveRate = ReceiveRate(playerStatistics);
            double spikeRate = SpikeRate(playerStatistics);
            double blockRate = BlockRate(playerStatistics);
            double faultRate = FaultRate(playerStatistics);

            if (playerStatistics.TotalServes() >= 3)
            {
                if (serveRate < 0.16)
                    score -= 2;
                else if (serveRate >= 0.35)
                    score += 2;
            }
            if (playerStatistics.TotalReceives() >= 3)
            {
                if (receiveRate < 0.2)
                    score -= 3;
                else if (receiveRate >= 0.6)
                    score += 2;
            }
            if (playerStatistics.TotalSpikes() >= 3)
            {
                if (spikeRate < 0.2)
                    score -= 2;
                else if (spikeRate >= 0.45)
                    score += 3;
            }
            if (playerStatistics.TotalBlocks() >= 3)
            {
                if (blockRate < 0.15) 
                    score -= 1;
                else if (blockRate >= 0.37)
                    score += 2;
            }
            if (playerStatistics.TotalBlocks() + playerStatistics.TotalFaults() + playerStatistics.TotalReceives() +
                playerStatistics.TotalServes() + playerStatistics.TotalSpikes() >= 3)
            {
                if (faultRate < 0.2)
                    score += 1;
                else if (faultRate >= 0.45)
                    score -= 3;
            }

            return score < 0;
        }

        private static double BlockRate(Set playerStatistics)
        {
            double divider = (4*playerStatistics.BlockFault + playerStatistics.BlockKill +
                              playerStatistics.BlockRebound);

            if (divider.Equals(0))
                return 0.0;

            return (playerStatistics.BlockKill + 0.3 * playerStatistics.BlockRebound) / 
                divider;
        }

        private static double SpikeRate(Set playerStatistics)
        {
            double divider = (4*playerStatistics.SpikeFault + playerStatistics.SpikeKill + playerStatistics.SpikeOther +
                              playerStatistics.SpikeBlocked);

            if (divider.Equals(0))
                return 0.0;

            return (playerStatistics.SpikeKill + 0.4 * playerStatistics.SpikeOther + 0.2 * playerStatistics.SpikeBlocked) / 
                divider;
        }

        private static double ReceiveRate(Set playerStatistics)
        {
            double divider = (5 * playerStatistics.ReceiveFault + playerStatistics.ReceivePerfect + playerStatistics.ReceiveGood + 
                playerStatistics.ReceiveBad);

            if (divider.Equals(0))
                return 0.0;

            return (playerStatistics.ReceivePerfect + 0.7*playerStatistics.ReceiveGood + 0.3*playerStatistics.ReceiveBad)/
                   divider;

        }

        private static double ServeRate(Set playerStatistics)
        {
            double divider = (2*playerStatistics.ServeFault + playerStatistics.ServeAce + playerStatistics.ServeHit +
                              playerStatistics.ServeOther);

             if (divider.Equals(0))
                return 0.0;

            return (playerStatistics.ServeAce + 0.5 * playerStatistics.ServeHit + 0.1 * playerStatistics.ServeOther) /
                           divider;
        }

        private static double FaultRate(Set playerStatistics)
        {
            double divider = (playerStatistics.OwnFault + playerStatistics.ServeFault + playerStatistics.ServeHit +
                              playerStatistics.ServeOther + playerStatistics.ReceiveFault +
                              playerStatistics.ReceivePerfect +
                              playerStatistics.ReceiveGood + playerStatistics.ReceiveBad + playerStatistics.SpikeFault +
                              playerStatistics.SpikeKill + playerStatistics.SpikeOther + playerStatistics.SpikeBlocked +
                              playerStatistics.BlockFault + playerStatistics.BlockKill + playerStatistics.BlockRebound);
            
            if (divider.Equals(0))
                return 0.0;

            return (playerStatistics.OwnFault + playerStatistics.ServeFault + playerStatistics.ReceiveFault +
                playerStatistics.SpikeFault + playerStatistics.BlockFault) / divider;
        }
    }
}
