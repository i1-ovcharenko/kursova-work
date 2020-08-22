using System;

namespace HAS
{
    [Serializable]
    public class InfraRedHeater : Heater
    {
        public InfraRedHeater() { }
        public InfraRedHeater(string heatertype, string manufacturer, string model, string service_area, int power, string power_suply, string placing,
            string purpose, string control, string heating_element, string dimensions, double cost) :
            base(heatertype, manufacturer, model, service_area, power, power_suply, placing, purpose, control, heating_element, dimensions, cost)
        {

        }
        public override string OutputInfo()
        {
            return base.OutputInfo();
        }
    }
}
