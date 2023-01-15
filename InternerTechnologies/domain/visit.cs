using System;
using System.Collections.Generic;
using System.Text;

namespace domain
{
    class visit
    {
        private int start;
        private int finish;
        private int user_id;
        private int doctor_id;

        public int Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value;
            }
        }
        public int Finish
        {
            get
            {
                return finish;
            }
            set
            {
                finish = value;
            }
        }
        public int User_id
        {
            get
            {
                return user_id;
            }
            set
            {
                user_id = value;
            }
        }
        public int Doctor_id
        {
            get
            {
                return doctor_id;
            }
            set
            {
                doctor_id = value;
            }
        }
    }
}
