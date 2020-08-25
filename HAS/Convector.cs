using System;

namespace HAS
{
    [Serializable]
    public class Convector : Heater
    {
        public Convector() { }
        public Convector(string manufacturer, string model, string service_area, int power, string power_supply, string placing,
            string purpose, string control, string heating_element, string dimensions, double cost) :
            base(manufacturer, model, service_area, power, power_supply, placing, purpose, control, heating_element, dimensions, cost)
        {
            Heater_type = "Конвектор";
        }
        public override string OutputInfo()
        {
            return base.OutputInfo();
        }
    }
}
