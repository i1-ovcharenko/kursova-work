using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAS
{
    [Serializable]
    public class FanHeater : Heater
    {
        public FanHeater() { }
        public FanHeater(string heatertype, string manufacturer, string model, string service_area, int power, string power_suply, string placing,
            string purpose, string control, string heating_element, string dimensions, double cost, int count) :
            base(heatertype, manufacturer, model, service_area, power, power_suply, placing, purpose, control, heating_element, dimensions, cost, count)
        {

        }
        public override string OutputInfo()
        {
            return base.OutputInfo();
        }
    }
}
