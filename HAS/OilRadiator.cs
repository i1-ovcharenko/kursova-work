using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAS
{
    [Serializable]
    public class OilRadiator : Heater
    {
        public OilRadiator() { }
        public OilRadiator(string heatertype, string manufacturer, string model, string service_area, int power, string power_suply, string placing,
            string purpose, string control, string heating_element, string dimensions, double cost, int count, int sections): 
            base(heatertype, manufacturer, model, service_area, power, power_suply, placing, purpose, control, heating_element, dimensions, cost, count)
        {
            Section_count = sections;
        }
        public override string OutputInfo()
        {
            return $"{Id}*{HeaterType}*{Manufacturer}*{Model}*{Service_area}*{Power}*{Power_suply}*{Placing}*" +
                $"{Purpose}*{Control}*{Heating_element}*{Dimensions}*{Cost}*{Count}*{Section_count}*";
        }
    }
}
