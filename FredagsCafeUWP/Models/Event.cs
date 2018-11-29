using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;

namespace FredagsCafeUWP.Models
{
    public class Event : INotifyPropertyChanged
    {
        private ObservableCollection<EventUser> _eventsUsers;
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int MaxUsers { get; set; }

        public ObservableCollection<EventUser> EventsUsers
        {
            get => _eventsUsers;
            set
            {
                _eventsUsers = value;
                OnPropertyChanged();
            } 
        }

        #region Constructors
        public Event(string name, string location, string description, int maxUsers, ObservableCollection<EventUser> eventUsers)
        {
            Name = name;
            Location = location;
            Description = description;
            MaxUsers = maxUsers;

            EventsUsers = eventUsers;
        }
        public Event()
        {

        }
        #endregion

        #region INotify

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
