using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;

namespace FredagsCafeUWP.Models
{
    public class EventPage : INotifyPropertyChanged
    {
        #region Field

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

        #endregion

        #region Props

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
            set
            {
                _userNameTb = value;
                OnPropertyChanged();
            }
        }

        public string UserEmailTb
        {
            get { return _userEmailTb; }
            set
            {
                _userEmailTb = value;
                OnPropertyChanged();
            }
        }

        public string EventNameTb
        {
            get { return _eventNameTb; }
            set
            {
                _eventNameTb = value;
                OnPropertyChanged();
            }
        }

        public string EventDescriptionTb
        {
            get { return _eventDescriptionTb; }
            set
            {
                _eventDescriptionTb = value;
                OnPropertyChanged();
            }
        }

        public string EventLocationTb
        {
            get { return _eventLocationTb; }
            set
            {
                _eventLocationTb = value;
                OnPropertyChanged();
            }
        }

        public string EventMaxUsersTb
        {
            get { return _eventMaxUsersTb; }
            set
            {
                _eventMaxUsersTb = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public EventPage() //TODO Tilføj img og tidspunkt til events
        {
            _message = new Message(this);
            LoadAsync();
        }

        public async void AddUser()
        {
            if (UserNameTb != null && UserEmailTb != null)
            {
                if (SelectedEvent != null)
                {
                    if (UserEmailTb.Contains("@edu.easj.dk") || UserEmailTb.Contains("@easj.dk"))
                    {
                        foreach (var eu in SelectedEvent.EventsUsers)
                        {
                            if (eu.Email.Equals(UserEmailTb))
                            {
                                await _message.Error("Email findes allerede", eu.Email + " findes allerede til en anden bruger");
                                return;
                            }
                        }

                        SelectedEvent.EventsUsers.Add(new EventUser(UserNameTb, UserEmailTb));

                        UserNameTb = null;
                        UserEmailTb = null;

                        SaveAsync();
                    }
                    else await _message.Error("Forkert email", "Du skal bruge en \"@edu.easj.dk\" eller en \"@easj.dk\" mail.");
                }
                else await _message.Error("Intet event valgt", "Vælg venligst et event");
            }
            else await _message.Error("Manglende input", "Tekstfelter mangler at blive udfyldt");
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

        public async void AddEvent()
        {
            if (EventNameTb != null && EventDescriptionTb != null && EventLocationTb != null && EventMaxUsersTb != null)
            {
                Events.Add(new Event(EventNameTb, EventLocationTb, EventDescriptionTb, EventMaxUsersTb, new ObservableCollection<EventUser>()));

                EventNameTb = null;
                EventLocationTb = null;
                EventDescriptionTb = null;
                EventMaxUsersTb = null;

                SaveAsync();
            }
            else await _message.Error("Manglende input", "Tekstfelter mangler at blive udfyldt");
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

        #region save/load
        public async void SaveAsync()
        {
            Debug.WriteLine("Saving list async...");
            await XmlReadWriteClass.SaveObjectToXml(Events, "events.xml");
            Debug.WriteLine("events.count: " + Events.Count);
        }
        private async void LoadAsync()
        {
            try
            {
                Debug.WriteLine("loading list async...");
                Events = await XmlReadWriteClass.ReadObjectFromXmlFileAsync<ObservableCollection<Event>>("events.xml");
                Debug.WriteLine("events.count:" + Events.Count);
            }
            catch (Exception)
            {
                Events = new ObservableCollection<Event>()
                {
                    new Event("Øl-Bowling", "Haven", "Husk bajer!", "12", new ObservableCollection<EventUser>()
                    {
                        new EventUser("Morten", "@edu.easj.dk"),
                        new EventUser("Lucas", "@edu.easj.dk")
                    })
                }; SaveAsync();
            }

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
