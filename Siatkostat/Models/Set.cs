using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Siatkostat.Annotations;

namespace Siatkostat.Models
{
    public class Set : INotifyPropertyChanged
    {
        #region Fields

        private int setNumber;

        private int blockFault;

        private int blockKill;

        private int blockRebound;

        private int ownFault;

        private int receiveBad;

        private int receiveFault;

        private int receiveGood;

        private int receivePerfect;

        private int serveAce;

        private int serveFault;

        private int serveHit;

        private int serveOther;

        private int spikeBlocked;

        private int spikeFault;

        private int spikeKill;

        private int spikeOther;

  
        #endregion

        #region Properties

        public Guid Id { get; set; }

        public Guid PlayerId { get; set; }

        public Guid MatchId { get; set; }

        public int SetNumber
        {
            get { return setNumber; }
            set
            {
                if (value == setNumber) return;
                setNumber = value;
                NotifyPropertyChanged();
            }
        }

        public int BlockFault
        {
            get { return blockFault; }
            set
            {
                if (value == blockFault) return;
                blockFault = value;
                NotifyPropertyChanged();
            }
        }

        public int BlockKill
        {
            get { return blockKill; }
            set
            {
                if (value == blockKill) return;
                blockKill = value;
                NotifyPropertyChanged();
            }
        }

        public int BlockRebound
        {
            get { return blockRebound; }
            set
            {
                if (value == blockRebound) return;
                blockRebound = value;
                NotifyPropertyChanged();
            }
        }

        public int OwnFault
        {
            get { return ownFault; }
            set
            {
                if (value == ownFault) return;
                ownFault = value;
                NotifyPropertyChanged();
            }
        }

        public int ReceiveBad
        {
            get { return receiveBad; }
            set
            {
                if (value == receiveBad) return;
                receiveBad = value;
                NotifyPropertyChanged();
            }
        }

        public int ReceiveFault
        {
            get { return receiveFault; }
            set
            {
                if (value == receiveFault) return;
                receiveFault = value;
                NotifyPropertyChanged();
            }
        }

        public int ReceiveGood
        {
            get { return receiveGood; }
            set
            {
                if (value == receiveGood) return;
                receiveGood = value;
                NotifyPropertyChanged();
            }
        }

        public int ReceivePerfect
        {
            get { return receivePerfect; }
            set
            {
                if (value == receivePerfect) return;
                receivePerfect = value;
                NotifyPropertyChanged();
            }
        }

        public int ServeAce
        {
            get { return serveAce; }
            set
            {
                if (value == serveAce) return;
                serveAce = value;
                NotifyPropertyChanged();
            }
        }

        public int ServeFault
        {
            get { return serveFault; }
            set
            {
                if (value == serveFault) return;
                serveFault = value;
                NotifyPropertyChanged();
            }
        }

        public int ServeHit
        {
            get { return serveHit; }
            set
            {
                if (value == serveHit) return;
                serveHit = value;
                NotifyPropertyChanged();
            }
        }

        public int ServeOther
        {
            get { return serveOther; }
            set
            {
                if (value == serveOther) return;
                serveOther = value;
                NotifyPropertyChanged();
            }
        }

        public int SpikeBlocked
        {
            get { return spikeBlocked; }
            set
            {
                if (value == spikeBlocked) return;
                spikeBlocked = value;
                NotifyPropertyChanged();
            }
        }

        public int SpikeFault
        {
            get { return spikeFault; }
            set
            {
                if (value == spikeFault) return;
                spikeFault = value;
                NotifyPropertyChanged();
            }
        }

        public int SpikeKill
        {
            get { return spikeKill; }
            set
            {
                if (value == spikeKill) return;
                spikeKill = value;
                NotifyPropertyChanged();
            }
        }

        public int SpikeOther
        {
            get { return spikeOther; }
            set
            {
                if (value == spikeOther) return;
                spikeOther = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region INotifyProperyChange members

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Methods

        public int TotalServes()
        {
            return serveAce + serveFault + serveHit + serveOther;
        }

        public int TotalReceives()
        {
            return receiveBad + receiveFault + receiveGood + receivePerfect;
        }

        public int TotalSpikes()
        {
            return spikeBlocked + spikeKill + spikeFault + spikeOther;
        }

        public int TotalFaults()
        {
            return ownFault + serveFault + receiveFault + spikeFault + blockFault;
        }

        public int TotalBlocks()
        {
            return blockFault + blockKill + blockRebound;
        }
        #endregion
    }
}
