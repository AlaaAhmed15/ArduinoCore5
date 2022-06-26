using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArduinoCore5.Models
{
    public class baby_tb
    {
        public int baby_id { get; set; }

        public int doctor_id { get; set; }

        public int nurse_id { get; set; }

        public string baby_firstname { get; set; }

        public string baby_lastname { get; set; }

        public DateTime baby_birthDate { get; set; }

        public DateTime baby_entry_date { get; set; }

        public string parents_phone_number { get; set; }

        public string baby_reason_of_entry { get; set; }

        public string baby_medical_history { get; set; }
    }
}
