using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArduinoCore5.Models
{
    public class nicu
    {
        public DateTime curr_date { get; set; }

        public int doctor_id { get; set; }

        public int nurse_id { get; set; }
        
        public int baby_id { get; set; }
        
        public int incubator_id { get; set; }

        public float temperature_reading { get; set; }

        public float humidity_reading { get; set; }

        public float oxygen_reading { get; set; }

        public float co2_reading { get; set; }

        public string light_state { get; set; }

        public int heartbeat_reading { get; set; }
    }
}
