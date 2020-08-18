using System;

namespace HAS
{
    abstract class Heater
    {
        private string manufacturer;
        private string model;
        private int service_area;
        private int power;
        private string power_suply;
        private string placing;
        private string purpose;
        private string control;
        private string heating_element;
        private string dimensions;
        private double cost;
        private int count;

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
        public int Service_area
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

        public Heater()
        { }
        public Heater(string manufacturer, string model,int service_area, int power, string power_suply, string placing,
            string purpose, string control, string heating_element, string dimensions, double cost, int count)
        {
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
        public Heater(string filestring)
        {
            //--------------------------
        }
    }
}
