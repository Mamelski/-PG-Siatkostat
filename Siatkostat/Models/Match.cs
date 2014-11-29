using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Siatkostat.Annotations;
using Siatkostat.ViewModels;

namespace Siatkostat.Models
{
    public class Match : INotifyPropertyChanged 
    {
        #region Fields

        private string oponentName;

        private DateTime matchDate;

        private int teamScoreSet1;

        private int teamScoreSet2;

        private int teamScoreSet3;

        private int teamScoreSet4;

        private int teamScoreSet5;

        private int oponentScoreSet1;

        private int oponentScoreSet2;

        private int oponentScoreSet3;

        private int oponentScoreSet4;

        private int oponentScoreSet5;
        #endregion

        #region Properties
        public int CurrentSet { get; set; }

        public int OpponentSetScore { get; set; }

        public int TeamSetScore { get; set; }

        public Guid Id { get; set; }

        public Guid TeamId { get; set; }
       
        public string OponentName
        {
            get { return oponentName; }
            set
            {
                if (value == oponentName) return;
                oponentName = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime MatchDate
        {
            get { return matchDate; }
            set
            {
                if (value == matchDate) return;
                matchDate = value;
                NotifyPropertyChanged();
            }
        }

        public int TeamScoreSet1
        {
            get { return teamScoreSet1; }
            set
            {
                if (value == teamScoreSet1) return;
                teamScoreSet1 = value;
                NotifyPropertyChanged();
            }
        }

        public int TeamScoreSet2
        {
            get { return teamScoreSet2; }
            set
            {
                if (value == teamScoreSet2) return;
                teamScoreSet2 = value;
                NotifyPropertyChanged();
            }
        }

        public int TeamScoreSet3
        {
            get { return teamScoreSet3; }
            set
            {
                if (value == teamScoreSet3) return;
                teamScoreSet3 = value;
                NotifyPropertyChanged();
            }
        }

        public int TeamScoreSet4
        {
            get { return teamScoreSet4; }
            set
            {
                if (value == teamScoreSet4) return;
                teamScoreSet4 = value;
                NotifyPropertyChanged();
            }
        }

        public int TeamScoreSet5
        {
            get { return teamScoreSet5; }
            set
            {
                if (value == teamScoreSet5) return;
                teamScoreSet5 = value;
                NotifyPropertyChanged();
            }
        }

        public int OponentScoreSet1
        {
            get { return oponentScoreSet1; }
            set
            {
                if (value == oponentScoreSet1) return;
                oponentScoreSet1 = value;
                NotifyPropertyChanged();
            }
        }

        public int OponentScoreSet2
        {
            get { return oponentScoreSet2; }
            set
            {
                if (value == oponentScoreSet2) return;
                oponentScoreSet2 = value;
                NotifyPropertyChanged();
            }
        }

        public int OponentScoreSet3
        {
            get { return oponentScoreSet3; }
            set
            {
                if (value == oponentScoreSet3) return;
                oponentScoreSet3 = value;
                NotifyPropertyChanged();
            }
        }

        public int OponentScoreSet4
        {
            get { return oponentScoreSet4; }
            set
            {
                if (value == oponentScoreSet4) return;
                oponentScoreSet4 = value;
                NotifyPropertyChanged();
            }
        }

        public int OponentScoreSet5
        {
            get { return oponentScoreSet5; }
            set
            {
                if (value == oponentScoreSet5) return;
                oponentScoreSet5 = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Events

        public delegate void SetFinish(object sender);
        public event SetFinish OnSetFinish;
        #endregion

        #region Constructor

        public Match()
        {
            teamScoreSet1 = teamScoreSet2 = teamScoreSet3 = teamScoreSet4 = teamScoreSet5 = 0;
            oponentScoreSet1 = oponentScoreSet2 = oponentScoreSet3 = oponentScoreSet4 = oponentScoreSet5 = 0;
            CurrentSet = 1;
            OpponentSetScore = TeamSetScore = 0;
        }
        #endregion

        #region Methods
        public void AddOpponentPoint()
        {
            switch (CurrentSet)
            {
                case 1:
                    oponentScoreSet1++;
                    break;
                case 2:
                    oponentScoreSet2++;
                    break;
                case 3:
                    oponentScoreSet3++;
                    break;
                case 4:
                    oponentScoreSet4++;
                    break;
                case 5:
                    oponentScoreSet5++;
                    break;
            }

            if (CurrentOpponentScore() >= 25)
            {
                if (CurrentOpponentScore() - CurrentTeamScore() >= 2)
                {
                    SetViewModel.Instance.InsertSets(CurrentSet);
                    OpponentSetScore++;
                    CurrentSet++;
                    if(OnSetFinish != null)
                        OnSetFinish.Invoke(this);
                }
            }
        }

        public void AddTeamPoint()
        {
            switch (CurrentSet)
            {
                case 1:
                    teamScoreSet1++;
                    break;
                case 2:
                    teamScoreSet2++;
                    break;
                case 3:
                    teamScoreSet3++;
                    break;
                case 4:
                    teamScoreSet4++;
                    break;
                case 5:
                    teamScoreSet5++;
                    break;
            }

            if (CurrentTeamScore() >= 25)
            {
                if (CurrentTeamScore() - CurrentOpponentScore() >= 2)
                {
                    SetViewModel.Instance.InsertSets(CurrentSet);
                    TeamSetScore++;
                    CurrentSet++;
                    if (OnSetFinish != null)
                        OnSetFinish.Invoke(this);
                }
            }
        }

        public int CurrentTeamScore()
        {
            switch (CurrentSet)
            {
                case 1:
                    return teamScoreSet1;
                case 2:
                    return teamScoreSet2;
                case 3:
                    return teamScoreSet3;
                case 4:
                    return teamScoreSet4;
                case 5:
                    return teamScoreSet5;
                default:
                    return 0;
            }
        }

        public int CurrentOpponentScore()
        {
            switch (CurrentSet)
            {
                case 1:
                    return oponentScoreSet1;
                case 2:
                    return oponentScoreSet2;
                case 3:
                    return oponentScoreSet3;
                case 4:
                    return oponentScoreSet4;
                case 5:
                    return oponentScoreSet5;
                default:
                    return 0;
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
    }
}
