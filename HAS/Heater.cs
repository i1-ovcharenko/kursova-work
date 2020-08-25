using System;
using System.Xml.Serialization;

namespace HAS
{
    [XmlInclude(typeof(OilRadiator))]
    [XmlInclude(typeof(FanHeater))]
    [XmlInclude(typeof(InfraRedHeater))]
    [XmlInclude(typeof(CeramicPanel))]
    [XmlInclude(typeof(HeatGun))]
    [XmlInclude(typeof(Convector))]
    [Serializable]
    public class Heater
    {
        private int id;
        private string heater_type;
        private string manufacturer;
        private string model;
        private string service_area;
        private int power;
        private string power_supply;
        private string placing;
        private string purpose;
        private string control;
        private string heating_element;
        private string dimensions;
        private double cost;
        private int section_count;
        
        public int Id { get => id; set => id = value; }
        public string Heater_type { get => heater_type; set => heater_type = value; }
        public string Manufacturer { get => manufacturer; set => manufacturer = value; }
        public string Model { get => model; set => model = value; }
        public string Service_area { get => service_area; set => service_area = value; }
        public int Power { get => power; set => power = value; }
        public string Power_supply { get => power_supply; set => power_supply = value; }
        public string Placing { get => placing; set => placing = value; }
        public string Purpose { get => purpose; set => purpose = value; }
        public string Control { get => control; set => control = value; }
        public string Heating_element { get => heating_element; set => heating_element = value; }
        public string Dimensions { get => dimensions; set => dimensions = value; }
        public double Cost { get => cost; set => cost = value; }
        public int Section_count { get => section_count; set => section_count = value; }

        public Heater()
        { }
        public Heater(string manufacturer, string model, string service_area, int power, string power_supply, string placing,
            string purpose, string control, string heating_element, string dimensions, double cost)
        {
            Random ran = new Random((Int32)DateTime.Now.Ticks);
            Id = ran.Next(1000,9999);
            Manufacturer = manufacturer;
            Model = model;
            Service_area = service_area;
            Power = power;
            Power_supply = power_supply;
            Placing = placing;
            Purpose = purpose;
            Control = control;
            Heating_element = heating_element;
            Dimensions = dimensions;
            Cost = cost;
        }

        public virtual string OutputInfo()
        {
            return $"{Id}*{Heater_type}*{Manufacturer}*{Model}*{Service_area}*{Power}*{Power_supply}*{Placing}*" +
                $"{Purpose}*{Control}*{Heating_element}*{Dimensions}*{Cost}*";
        }
    }
}
