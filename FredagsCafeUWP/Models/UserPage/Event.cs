using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;

namespace FredagsCafeUWP.Models.UserPage
{
    public class Event : INotifyPropertyChanged
    {
        private ObservableCollection<EventUser> _eventsUsers;
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string MaxUsers { get; set; }
        public string Image { get; set; }
        public string DateTime { get; set; }

        public ObservableCollection<EventUser> EventsUsers
        {
            get => _eventsUsers;
            set{_eventsUsers = value;} 
        }

        #region Constructors
        public Event(string name, string location, string description, string maxUsers, string image, string dateTime, ObservableCollection<EventUser> eventUsers)
        {
            Name = name;
            Location = location;
            Description = description;
            MaxUsers = maxUsers;
            Image = image;
            DateTime = dateTime;

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
