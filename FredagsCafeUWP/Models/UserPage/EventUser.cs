namespace FredagsCafeUWP.Models
{
    public class EventUser
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public EventUser(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public EventUser()
        {
            
        }
    }
}
