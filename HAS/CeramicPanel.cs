using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAS
{
    class CeramicPanel : Heater
    {
        public CeramicPanel() { }
        public CeramicPanel(string manufacturer, string model, int service_area, int power, string power_suply, string placing,
            string purpose, string control, string heating_element, string dimensions, double cost, int count) :
            base(manufacturer, model, service_area, power, power_suply, placing, purpose, control, heating_element, dimensions, cost, count)
        {

        }
        public CeramicPanel(string filestring) : base(filestring)
        {

        }
    }
}
