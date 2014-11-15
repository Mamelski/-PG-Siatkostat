using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Siatkostat.Annotations;

namespace Siatkostat.Models
{
    public class Team : INotifyPropertyChanged
    {
        #region Fields

        private string teamName = String.Empty;

        private string teamPassword = String.Empty;

        #endregion

        #region Properties

        public Guid Id { get; set; }

        public string TeamName
        {
            get { return teamName; }
            set
            {
                if (value == teamName) return;
                teamName = value;
                NotifyPropertyChanged();
            }
        }

        public string TeamPassword
        {
            get { return teamPassword; }
            set
            {
                if (value == teamPassword) return;
                teamPassword = value;
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
    }
}