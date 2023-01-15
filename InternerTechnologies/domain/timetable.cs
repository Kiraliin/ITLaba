using System;
using System.Collections.Generic;
using System.Text;

namespace domain
{
    class timetable
    {
        private int doctor_id;
        private int start_day;
        private int finish_day;

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
        public int Start_day
        {
            get
            {
                return start_day;
            }
            set
            {
                start_day = value;
            }
        }
        public int Finish_day
        {
            get
            {
                return finish_day;
            }
            set
            {
                finish_day = value;
            }
        }
    }
}
