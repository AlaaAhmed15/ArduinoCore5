using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArduinoCore5.Models
{
    public class doctor_tb
    {
        public int doctor_id { get; set; }
        
        public string doctor_firstname { get; set; }
        
        public string doctor_lastname { get; set; }

        public string doctor_username { get; set; }

        public string doctor_password { get; set; }
        
        public string doctor_email { get; set; }
        
        public string doctor_mobile { get; set; }

        public DateTime doctor_insertion_date { get; set; }

        public DateTime doctor_last_modification { get; set; }
    }
}
