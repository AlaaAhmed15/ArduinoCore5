using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArduinoCore5.Models
{
    public class doctor_notes
    {
        public int doctor_id { get; set; }

        public int baby_id { get; set; } 
        
        public string note_text { get; set; }
        
        public DateTime note_date { get; set; }
        
        public string note_state { get; set; }
        
        public string note_progress { get; set; }
    }
}
