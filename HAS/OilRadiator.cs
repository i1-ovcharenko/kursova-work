using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAS
{
    class OilRadiator : Heater
    {
        private int section_count;

        public int Section_count
        {
            get => section_count;
            set => section_count = value;
        }
        public OilRadiator() { }
        public OilRadiator(string manufacturer, string model, int service_area, int power, string power_suply, string placing,
            string purpose, string control, string heating_element, string dimensions, double cost, int count, int sections): 
            base(manufacturer, model, service_area, power, power_suply, placing, purpose, control, heating_element, dimensions, cost, count)
        {
            Section_count = sections;
        }
        public OilRadiator(string filestring):base(filestring)
        {

        }

    }
}
