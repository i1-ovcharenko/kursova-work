﻿using System;

namespace HAS
{
    [Serializable]
    public class InfraRedHeater : Heater
    {
        public InfraRedHeater() { }
        public InfraRedHeater(string manufacturer, string model, string service_area, int power, string power_supply, string placing,
            string purpose, string control, string heating_element, string dimensions, double cost) :
            base(manufacturer, model, service_area, power, power_supply, placing, purpose, control, heating_element, dimensions, cost)
        {
            Heater_type = "Інфрачервоний обігрівач";
        }
        public override string OutputInfo()
        {
            return base.OutputInfo();
        }
    }
}
