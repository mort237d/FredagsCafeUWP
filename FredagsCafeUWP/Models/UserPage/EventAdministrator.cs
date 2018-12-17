using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Models.UserPage;

namespace FredagsCafeUWP.Models
{
    public class EventAdministrator : INotifyPropertyChanged
    {
        #region Field

        private Message _message;

        private ObservableCollection<Event> _events;
        private Event _selectedEvent;
        private EventUser _selectedEventUser;
        BrowseImages _browseImages = new BrowseImages();

        private string _userNameTb, _userEmailTb;

        private string _eventNameTb,_eventLocationTb,_eventDescriptionTb,_eventMaxUsersTb,_eventImageTb,_eventDateTimeTb;

        private bool _showAddEventPopUp, _showAddEventUserPopUp;

        #endregion

        #region Props

        public bool ShowAddEventPopUp
        {
            get { return _showAddEventPopUp; }
            set
            {
                _showAddEventPopUp = value;
                OnPropertyChanged();
            }
        }

        public bool ShowAddEventUserPopUp
        {
            get { return _showAddEventUserPopUp; }
            set
            {
                _showAddEventUserPopUp = value;
                OnPropertyChanged();
            }
        }

        public string EventImageTb
        {
            get { return _eventImageTb; }
            set
            {
                _eventImageTb = value;
                OnPropertyChanged();
            }
        }

        public string EventDateTimeTb
        {
            get { return _eventDateTimeTb; }
            set
            {
                _eventDateTimeTb = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Event> Events
        {
            get { return _events; }
            set
            {
                _events = value;
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

        private EventAdministrator()
        {
            _message = new Message(this);
        }

        #region Singleton

        private static EventAdministrator instance;
        public static EventAdministrator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventAdministrator();
                }
                return instance;
            }
        }

        #endregion
        
        #region ButtonMethods

        public void ShowAddEventPopUpMethod()
        {
            ShowAddEventPopUp = true;
        }

        public void ShowAddEventUserPopUpMethod()
        {
            ShowAddEventUserPopUp = true;
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

                        UserNameTb = UserEmailTb = null;

                        ShowAddEventUserPopUp = false;
                    }
                    else await _message.Error("Forkert email", "Du skal bruge en \"@edu.easj.dk\" eller en \"@easj.dk\" mail.");
                }
                else await _message.Error("Intet event valgt", "Vælg venligst et event");
            }
            else await _message.Error("Manglende input", "Tekstfelter mangler at blive udfyldt");
        }

        public async void RemoveUser()
        {
            if (SelectedEvent != null)
            {
                if (SelectedEventUser != null)
                {
                    await _message.YesNo("Slet bruger af eventet", "Er du sikker på at du vil slette " + SelectedEventUser.Name + " fra " + SelectedEvent.Name + "?");
                }
                else await _message.Error("Ingen deltager valgt", "Vælg venligst en deæltager for at slette.");
            }
            else await _message.Error("Intet event valgt", "Vælg venligst et event for at fjerne en deltager.");
        }

        public async void AddEvent()
        {
            if (EventNameTb != null && EventDescriptionTb != null && EventLocationTb != null && EventMaxUsersTb != null)
            {
                if (int.TryParse(EventMaxUsersTb, out _))
                {
                    if (EventImageTb != null || EventImageTb == "")
                    {
                        Events.Add(new Event(EventNameTb, EventLocationTb, EventDescriptionTb, EventMaxUsersTb, EventImageTb, EventDateTimeTb, new ObservableCollection<EventUser>()));
                    }
                    else Events.Add(new Event(EventNameTb, EventLocationTb, EventDescriptionTb, EventMaxUsersTb, "EventImages/Event.jpg", EventDateTimeTb, new ObservableCollection<EventUser>()));

                    EventNameTb = EventLocationTb = EventDescriptionTb = EventMaxUsersTb = EventDateTimeTb = EventImageTb = null;

                    ShowAddEventPopUp = false;
                }
                else await _message.Error("Forkert input", "Max deltagere skal være et tal.");
            }
            else await _message.Error("Manglende input", "Tekstfelter mangler at blive udfyldt");
        }

        public async void RemoveEvent()
        {
            if (SelectedEvent != null)
            {
                await _message.YesNo("Slet event", "Er du sikker på at du vil slette " + SelectedEvent.Name + "?");
            }
            else await _message.Error("Intet event valgt", "Vælg venligst et event for at fjerne det.");
        }

        public async void BrowseAddImageButton()
        {
            EventImageTb = await _browseImages.BrowseImageWindow("EventImages/");
            ShowAddEventPopUpMethod();
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
