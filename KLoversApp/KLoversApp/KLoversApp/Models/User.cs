using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLoversApp.Models
{
    public class User
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User() { }
        public User(string userName, string password)
        {
            this.Username = userName;
            this.Password = password; 
        }

        public bool CheckInformation()
        {
            if (!this.Username.Equals("") && !this.Password.Equals(""))
                return true;
            else
                return false;
        }

    }
}
