using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_00003741
{
    public class Order
    {
        private int orderId;
        private DateTime orderDate;
        private string deliveryAddress;
        private string status;
        private List<OrderItem> orderedItems;
        private float orderTotal = 0;
        public Order(int orderId, DateTime orderDate, string deliveryAddress,string status)
        {
            this.orderId = orderId;
            this.orderDate = orderDate;
            this.deliveryAddress = deliveryAddress;
            this.status = status;
            orderedItems = new List<OrderItem>();            
        }
        public Order(DateTime orderDate, string deliveryAddress, string status)
        {
            this.orderDate = orderDate;
            this.deliveryAddress = deliveryAddress;
            this.status = status;
            orderedItems = new List<OrderItem>();
        }
        public int Id
        {
            get
            {
                return orderId;
            }
            set
            {
                Id = value;
            }
        }
        public DateTime OrderDate
        {
            get
            {
                return orderDate;
            }
            set
            {
                orderDate = value;
            }
        }
        public string Address
        {
            get
            {
                return deliveryAddress;
            }

            set
            {
                deliveryAddress = value;
            }
        }
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }

        }
        public float Total
        {
            get
            {                
                foreach(OrderItem orderItem in orderedItems)
                {
                    orderTotal += orderItem.Total;
                }

                return orderTotal;
            }        
        }
        public void AddOrderItem(OrderItem item)
        {
            orderedItems.Add(item);
        }
        public List<OrderItem> GetOrderedItems()
        {
            return orderedItems;
        }
    }
}
