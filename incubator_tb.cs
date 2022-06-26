using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArduinoCore5.Models
{
    public class incubator_tb
    {
        public int incubator_id { get; set; }
                
        public string incubator_state { get; set; }
        
        public int number_of_sensors { get; set; }
        
        public string temperature_sensor { get; set; }
        
        public string humidity_sensor { get; set; }
        
        public string oxygen_sensor { get; set; }
        
        public string co2_sensor { get; set; }
        
        public string heartbeat_sensor { get; set; }
        
        public string light_sensor { get; set; }
        
    }
}
