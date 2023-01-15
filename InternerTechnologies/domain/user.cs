using System;
using System.Collections.Generic;
using System.Text;

namespace domain
{
    class user
    {
        private int id;
        private string phoneNumber;
        private string name;
        private role roleuser;

        public int Id 
        { 
            get 
            {
                return id; 
            } 
            set 
            { 
                id = value; 
            } 
        }
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public role RoleUser
        {
            get 
            { 
                return roleuser; 
            }
            set 
            {
                roleuser = value; 
            }
        }
    }
}
