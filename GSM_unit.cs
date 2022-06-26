using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArduinoCore5.Models
{
    public class GSM_unit
    {
        public int doctor_id { get; set; }

        public string doctor_mobile { get; set; }

        public int baby_id { get; set; }
        
        public string msg_txt { get; set; }

        public DateTime msg_date_time { get; set; }

    }
}
