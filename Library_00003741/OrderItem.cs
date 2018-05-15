using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_00003741
{
    public class OrderItem
    {
        private int orderId;
        private Item item = new Item();
        private int quantity = 0;
        private float sellingPrice = 0;
        private float orderItemTotal = 0;

        public OrderItem(Item item, int quantity, float sellingPrice)
        {
            this.item = item;
            this.quantity = quantity;
            this.sellingPrice = sellingPrice;
        }
        public OrderItem(int orderId, Item item, int quantity, float sellingPrice)
        {
            this.orderId = orderId;
            this.item = item;
            this.quantity = quantity;
            this.sellingPrice = sellingPrice;
        }
        public int Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
            }
        }
        public string Category
        {
            get
            {
                return item.Category;
            }
        }
        public string Description
        {
            get
            {
                return item.Description;
            }
        }
        public string Characteristics
        {
            get
            {
                return item.Characteristics;
            }
        }
        public string Brand
        {
            get
            {
                return item.Brand;
            }
        }
        public string Type
        {
            get
            {
                return item.Type;
            }
        }
        public float StandardPrice
        {
            get
            {
                return item.PriceIn;
            }           
        }
        public float SellingPrice
        {
            get
            {
                return sellingPrice;
            }
            set
            {
                sellingPrice = value;
            }
        }
        public float Total
        {
            get
            {
                return sellingPrice * quantity;
            }
            set
            {
                orderItemTotal = value;
            }
        }
        public int GetInt()
        {
            return item.Id;
        }
        public int GetOrderId()
        {
            return orderId;
        }        
    }
}
