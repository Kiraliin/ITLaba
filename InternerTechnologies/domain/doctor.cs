using System;
using System.Collections.Generic;
using System.Text;

namespace domain
{
    class doctor
    {
        private int id;
        private string name;
        private specialization spec;

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
        public specialization Specialization
        {
            get
            {
                return spec;
            }
            set
            {
                spec = value;
            }
        }
    }
}
