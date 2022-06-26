using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArduinoCore5.Models
{
    public class readings_tb
    {                                          
        public int baby_id { get; set; }

        public float temperature_reading { get; set; }
        public DateTime temperature_time { get; set; }

        public float humidity_reading { get; set; }

        public DateTime humidity_time { get; set; }

        public float oxygen_reading { get; set; }
        public DateTime oxygen_time { get; set; }

        public float co2_reading { get; set; }
        public DateTime co2_time { get; set; }

        public int heartbeat_reading { get; set; }
        public DateTime heartbeat_time { get; set; }
    }
}
