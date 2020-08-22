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
        private string heaterType;
        private string manufacturer;
        private string model;
        private string service_area;
        private int power;
        private string power_suply;
        private string placing;
        private string purpose;
        private string control;
        private string heating_element;
        private string dimensions;
        private double cost;
        private int count;
        private int section_count;

        
        public int Id
        {
            get => id;
            set => id = value;
        }
        public string HeaterType
        {
            get => heaterType;
            set => heaterType = value;
        }
        public string Manufacturer
        {
            get => manufacturer;
            set => manufacturer = value;
        }
        public string Model
        {
            get => model;
            set => model = value;
        }
        public string Service_area
        {
            get => service_area;
            set => service_area = value;
        }
        public int Power
        {
            get => power;
            set => power = value;
        }
        public string Power_suply
        {
           get => power_suply;
           set => power_suply = value;
        }
        public string Placing
        {
            get => placing;
            set => placing = value;
        }
        public string Purpose
        {
            get => purpose;
            set => purpose = value;
        }
        public string Control
        {
            get => control;
            set => control = value;
        }
        public string Heating_element
        {
            get => heating_element;
            set => heating_element = value;
        }
        public string Dimensions
        {
            get => dimensions;
            set => dimensions = value;
        }
        public double Cost
        {
            get => cost;
            set => cost = value;
        }
        public int Count
        {
            get => count;
            set => count = value;
        }
        public int Section_count
        {
            get => section_count;
            set => section_count = value;
        }

        public Heater()
        { }
        public Heater(string heatertype, string manufacturer, string model, string service_area, int power, string power_suply, string placing,
            string purpose, string control, string heating_element, string dimensions, double cost, int count)
        {
            Random ran = new Random((Int32)DateTime.Now.Ticks);
            Id = ran.Next(1000,9999);
            HeaterType = heatertype;
            Manufacturer = manufacturer;
            Model = model;
            Service_area = service_area;
            Power = power;
            Power_suply = power_suply;
            Placing = placing;
            Purpose = purpose;
            Control = control;
            Heating_element = heating_element;
            Dimensions = dimensions;
            Cost = cost;
            Count = count;
        }
        public virtual string OutputInfo()
        {
            return $"{Id}*{HeaterType}*{Manufacturer}*{Model}*{Service_area}*{Power}*{Power_suply}*{Placing}*" +
                $"{Purpose}*{Control}*{Heating_element}*{Dimensions}*{Cost}*{Count}*";
        }
    }
}
