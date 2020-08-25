using System;

namespace HAS
{
    [Serializable]
    public class OilRadiator : Heater
    {
        public OilRadiator() { }
        public OilRadiator(string manufacturer, string model, string service_area, int power, string power_supply, string placing,
            string purpose, string control, string heating_element, string dimensions, double cost, int sections): 
            base(manufacturer, model, service_area, power, power_supply, placing, purpose, control, heating_element, dimensions, cost)
        {
            Heater_type = "Масляний радіатор";
            Section_count = sections;
        }
        public override string OutputInfo()
        {
            return $"{Id}*{Heater_type}*{Manufacturer}*{Model}*{Service_area}*{Power}*{Power_supply}*{Placing}*" +
                $"{Purpose}*{Control}*{Heating_element}*{Dimensions}*{Cost}*{Section_count}*";
        }
    }
}
