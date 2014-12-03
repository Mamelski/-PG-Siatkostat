using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Siatkostat.Annotations;

namespace Siatkostat.Models
{
    public class Player : INotifyPropertyChanged
    {
        #region Fields

        private string firstName;

        private string lastName;

        private int number;

        private bool isLibero;

        public bool needSubstitution = false;

        #endregion

        #region Properties

        public Guid Id { get; set; }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (value == firstName) return;
                firstName = value;
                NotifyPropertyChanged();
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                if (value == lastName) return;
                lastName = value;
                NotifyPropertyChanged();
            }
        }

        public int Number
        {
            get { return number; }
            set
            {
                if (value == number) return;
                number = value;
                NotifyPropertyChanged();
            }
        }

        public Guid TeamId { get; set; }

        public bool IsLibero
        {
            get { return isLibero; }
            set
            {
                if (value == isLibero) return;
                isLibero = value;
                NotifyPropertyChanged();
            }
        }

        public string IsLiberoString
        {
            get
            {
                return IsLibero ? "tak" : "nie";
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