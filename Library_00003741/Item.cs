using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_00003741
{
    public class Item
    {
        private int id = -1;
        private string category = "";
        private string description = "";
        private string characteristics = "";
        private string brand = "";
        private string type = "";
        private float priceIn = 0;
        private float priceOut = 0;

        public Item(int id, string category,string description, string characteristics, string brand, string type, float priceIn, float priceOut)
        {
            this.id = id;
            this.category = category;
            this.description = description;
            this.characteristics = characteristics;
            this.brand = brand;
            this.type = type;
            this.priceIn = priceIn;
            this.priceOut = priceOut;
        }

        public Item(string category, string description, string characteristics, string brand, string type, float priceIn, float priceOut)
        {
            this.category = category;
            this.description = description;
            this.characteristics = characteristics;
            this.brand = brand;
            this.type = type;
            this.priceIn = priceIn;
            this.priceOut = priceOut;
        }

        public Item() { }
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        public string Characteristics
        {
            get
            {
                return characteristics;
            }
            set
            {
                characteristics = value;
            }
        }               
        public string Brand
        {
            get
            {
                return brand;
            }
            set
            {
                brand = value;
            }
        }
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
        public float PriceIn
        {
            get
            {
                return priceIn;
            }
            set
            {
                priceIn = value;
            }            
        }    
        public float PriceOut
        {
            get
            {
                return priceOut;
            }
            set
            {
                priceOut = value;
            }
        }
    }
}
