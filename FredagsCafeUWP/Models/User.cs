using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using FredagsCafeUWP.Annotations;

namespace FredagsCafeUWP.Models
{
    class User : INotifyPropertyChanged
    {
        private string _name;
        private string _grade;
        private string _education;
        private string _email;
        private string _telephoneNumber;
        private string _userName;
        private string _password;
        private string _imageSource;

        public User(string name, string grade, string education, string email, string telephoneNumber, string userName, string password, string imageSource)
        {
            Name = name;
            Grade = grade;
            Education = education;
            Email = email;
            TelephoneNumber = telephoneNumber;
            UserName = userName;
            Password = password;
            ImageSource = imageSource;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Navn: " + Name);
            //sb.Append(" ");
            //sb.Append("Købspris: " + BuyingPrice);
            //sb.Append(" ");
            //sb.Append("Salgspris: " + SellingPrice);
            //sb.Append(" ");
            //sb.Append("Antal: " + Amount);
            //sb.Append(" ");
            //sb.Append("Antal solgt: " + AmountSold);
            //sb.Append(" ");
            //sb.Append("Image Source: " + ImageSource);
            //sb.Append(" ");
            //sb.Append("ForegroundColor: " + ForegroundColor);
            sb.Append("\n");

            return sb.ToString();
        }

        #region Properties

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }
        public string Grade
        {
            get { return _grade; }
            set { _grade = value; }
        }
        public string Education
        {
            get { return _education; }
            set { _education = value; }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
            }
        }
        public string TelephoneNumber
        {
            get { return _telephoneNumber; }
            set { _telephoneNumber = value; }
        }
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string ImageSource
        {
            get { return _imageSource; }
            set { _imageSource = value; }
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
