using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;

namespace FredagsCafeUWP.Models
{
    public class EventPage : INotifyPropertyChanged
    {
        private Message _message;

        private ObservableCollection<Event> _events;
        private Event _selectedEvent;
        private EventUser _selectedEventUser;

        private string _userNameTb;
        private string _userEmailTb;

        private string _eventNameTb;
        private string _eventLocationTb;
        private string _eventDescriptionTb;
        private string _eventMaxUsersTb;

        public ObservableCollection<Event> Events
        {
            get { return _events; }
            set
            {
                _events = value; 
                OnPropertyChanged();
            }
        }

        public Event SelectedEvent
        {
            get { return _selectedEvent; }
            set
            {
                _selectedEvent = value;
                OnPropertyChanged();
            }
        }

        public EventUser SelectedEventUser
        {
            get { return _selectedEventUser; }
            set
            {
                _selectedEventUser = value; 
                OnPropertyChanged();
            }
        }

        public string UserNameTb
        {
            get { return _userNameTb; }
            set { _userNameTb = value; }
        }

        public string UserEmailTb
        {
            get { return _userEmailTb; }
            set { _userEmailTb = value; }
        }

        public string EventNameTb
        {
            get { return _eventNameTb; }
            set { _eventNameTb = value; }
        }

        public string EventDescriptionTb
        {
            get { return _eventDescriptionTb; }
            set { _eventDescriptionTb = value; }
        }

        public string EventLocationTb
        {
            get { return _eventLocationTb; }
            set { _eventLocationTb = value; }
        }

        public string EventMaxUsersTb
        {
            get { return _eventMaxUsersTb; }
            set { _eventMaxUsersTb = value; }
        }

        public EventPage()
        {
            _message = new Message(this);
            Events = new ObservableCollection<Event>()
            {
                new Event("Øl-Bowling", "Haven", "Husk bajer!", 12, new ObservableCollection<EventUser>()
                {
                    new EventUser("Morten", "@edu.easj.dk"),
                    new EventUser("Lucas", "@edu.easj.dk")
                })
            };
        }

        public void AddUser()
        {
            Debug.WriteLine("addEventUser");
        }

        public async void RemoveUser()
        {
            Debug.WriteLine("removeEventUser");
            if (SelectedEvent != null)
            {
                if (SelectedEventUser != null)
                {
                    await _message.YesNo("Slet bruger af eventet", "Er du sikker på at du vil slette " + SelectedEventUser.Name + " fra " + SelectedEvent.Name + "?");
                }
                else await _message.Error("Ingen bruger valgt", "Vælg venligst en bruger.");
            }
            else await _message.Error("Intet event valgt", "Vælg venligst et event.");
        }

        public void AddEvent()
        {
            Debug.WriteLine("addEvent");
        }

        public async void RemoveEvent()
        {
            Debug.WriteLine("removeEvent");
            if (SelectedEvent != null)
            {
                await _message.YesNo("Slet event", "Er du sikker på at du vil slette " + SelectedEvent + "?");
            }
            else await _message.Error("Intet event valgt", "Vælg venligst et event.");
        }

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
