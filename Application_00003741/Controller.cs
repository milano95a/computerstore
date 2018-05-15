using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Library_00003741;

namespace Application_00003741
{
    public class Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Db.Properties.Settings.DbConnectionString"].ConnectionString;
        SqlConnection connection;
        public string exception = "";
        
        public Order currentOrder = null;
        public List<OrderItem> currentOrderItems = new List<OrderItem>();
        public List<Order> allOrders = new List<Order>();

        public Item currentItem = null;
        public List<Item> items = new List<Item>();
        public List<OrderItem> orderItems = new List<OrderItem>();
        public List<OrderItem> placeholderForOrderItems = new List<OrderItem>();
        

        
        public bool editable = false;

        private static Controller con;
        static Controller()
        {
            con = new Controller();
        }
        public static Controller ControllerInstance
        {
            get
            {
                return con;
            }
        }
        // get computers
        public List<Item> GetAllComputers()
        {
            List<Item> allItems = GetAllItems();
            List<Item> computers = new List<Item>();

            foreach (Item item in allItems)
            {
                if (item.Category.Equals("computer"))
                {
                    computers.Add(item);
                }
            }
            this.items = computers;
            return items;
        }
        // get softwares
        public List<Item> GetAllSoftware()
        {
            List<Item> allItems = GetAllItems();
            List<Item> softwares = new List<Item>();

            foreach (Item item in allItems)
            {
                if (item.Category.Equals("software"))
                {
                    softwares.Add(item);
                }
            }
            this.items = softwares;
            return items;
        }
        // get accessoires
        public List<Item> GetAllAccessoires()
        {
            List<Item> allItems = GetAllItems();
            List<Item> accessoires = new List<Item>();

            foreach (Item item in allItems)
            {
                if (item.Category.Equals("accessoires"))
                {
                    accessoires.Add(item);
                }
            }
            this.items = accessoires;
            return items;
        }
        // get all items 
        public List<Item> GetAllItems()
        {
            List<Item> itemsList = new List<Item>();

            //try
            {
                string query = "select * from [Items]";

                using (connection = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    for (int i = 0, k = table.Rows.Count; i < k; i++)
                    {
                        int id = Convert.ToInt32(table.Rows[i][0].ToString());
                        string category = table.Rows[i][1].ToString();
                        string type = table.Rows[i][2].ToString();
                        string brand = table.Rows[i][3].ToString();
                        string model = table.Rows[i][4].ToString();
                        string characteristics = table.Rows[i][5].ToString();
                        float priceIn = (float)Convert.ToDouble(table.Rows[i][6].ToString());
                        float priceOut = (float)Convert.ToDouble(table.Rows[i][7].ToString());

                        Item item = new Item(id, category, type, brand, model, characteristics, priceIn, priceOut);
                        itemsList.Add(item);
                    }
                    items = itemsList;

                    return items;
                }
            }
            //catch (Exception e)
            {

                return null;
            }
        }

        // get open orders
        public List<Order> GetAllOpenOrders()
        {
            List<Order> allOrders = GetAllOrders();
            List<Order> openOrders = new List<Order>();

            foreach (Order order in allOrders)
            {
                if (order.Status.Equals("open"))
                {
                    openOrders.Add(order);
                }
            }

            this.allOrders = openOrders;
            return this.allOrders;
        }
        // get processed orders
        public List<Order> GetAllProcessedOrders()
        {
            List<Order> allOrders = GetAllOrders();
            List<Order> processedOrders = new List<Order>();

            foreach (Order order in allOrders)
            {
                if (order.Status.Equals("processed"))
                {
                    processedOrders.Add(order);
                }
            }

            this.allOrders = processedOrders;
            return processedOrders;
        }
        // get cancelled orders
        public List<Order> GetAllCancelled()
        {
            List<Order> allOrders = GetAllOrders();
            List<Order> cancelledOrders = new List<Order>();

            foreach (Order order in allOrders)
            {
                if (order.Status.Equals("cancelled"))
                {
                    cancelledOrders.Add(order);
                }
            }

            this.allOrders = cancelledOrders;
            return this.allOrders;
        }
        // get all orders
        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();

            StringBuilder builder = new StringBuilder();
            string query = "select * from [Orders]";

            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);

                for (int i = 0, k = table.Rows.Count; i < k; i++)
                {
                    int id = Convert.ToInt32(table.Rows[i][0].ToString());
                    DateTime date = Convert.ToDateTime(table.Rows[i][1].ToString());
                    string address = table.Rows[i][2].ToString();
                    string status = table.Rows[i][3].ToString();
                    float total = (float)Convert.ToDouble(table.Rows[i][4].ToString());

                    Order order = new Order(id, date, address, status);
                    orders.Add(order);
                }

                allOrders = orders;
                return allOrders;
            }
        }

        public void SetAllOrderItems()
        {
            GetAllItems();
            GetAllOrders();

            List<OrderItem> orderItems = new List<OrderItem>();

            StringBuilder builder = new StringBuilder();
            string query = "select * from [OrderItems]";

            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);

                for (int i = 0, k = table.Rows.Count; i < k; i++)
                {
                    int OrderId = Convert.ToInt32(table.Rows[i][0].ToString());
                    int ItemId = Convert.ToInt32(table.Rows[i][1].ToString());
                    int quantity; int.TryParse(table.Rows[i][2].ToString(), out quantity);
                    float sellingPrice; float.TryParse(table.Rows[i][3].ToString(), out sellingPrice);

                    OrderItem orderItem = new OrderItem(GetItemById(ItemId), quantity, sellingPrice);
                    SetOrderItem(orderItem, OrderId);
                }
            }
        }
        public void SetOrderItem(OrderItem orderItem, int orderId)
        {
            foreach (Order order in allOrders)
            {
                if (order.Id == orderId)
                {
                    order.AddOrderItem(orderItem);
                }
            }
        }
        public void InsertItem(Item item)
        {
            GetAllItems();

            string query = "insert into [Items] values (@id, @category, @type, @brand, @description, @characteristics, @priceIn, @priceOut)";
            int maxId = 0;

            //try
            {
                using (connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {


                    for (int i = 0, k = items.Count; i < k; i++)
                    {
                        int id = items[i].Id;
                        maxId = maxId <= id ? id += 1 : maxId;
                    }

                    connection.Open();

                    command.Parameters.AddWithValue("@id", maxId);
                    command.Parameters.AddWithValue("@category", item.Category);
                    command.Parameters.AddWithValue("@type", item.Type);
                    command.Parameters.AddWithValue("@brand", item.Brand);
                    command.Parameters.AddWithValue("@description", item.Description);
                    command.Parameters.AddWithValue("@characteristics", item.Characteristics);
                    command.Parameters.AddWithValue("@priceIn", item.PriceIn);
                    command.Parameters.AddWithValue("@priceOut", item.PriceOut);

                    command.ExecuteNonQuery();
                }
            }
            //catch (Exception e)
            {
                //exception = e.StackTrace;
            }
        }
        public void InserOrder(Order order)
        {
            GetAllOrders();

            string query = "insert into [Orders] values (@Id, @Date, @DeliveryAddress, @OrderStatus, @Total)";
            int maxId = 0;



            //try
            {
                using (connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    for (int i = 0, k = allOrders.Count; i < k; i++)
                    {
                        int id = allOrders[i].Id;
                        maxId = maxId <= id ? id += 1 : maxId;
                    }


                    connection.Open();

                    command.Parameters.AddWithValue("@Id", maxId);
                    command.Parameters.AddWithValue("@Date", order.OrderDate);
                    command.Parameters.AddWithValue("@DeliveryAddress", order.Address);
                    command.Parameters.AddWithValue("@OrderStatus", order.Status);
                    command.Parameters.AddWithValue("@Total", order.Total);

                    command.ExecuteNonQuery();


                }

                InsertOrderItem(order, maxId);

            }
            //catch (Exception e)
            //{
            //    exception = e.StackTrace;
            //}
        }
        public void InsertOrderItem(Order order, int id)
        {
            string query = "insert into [OrderItems] values (@OrderId, @ItemId, @Quantity, @SellingPrice,@Total)";

            //try
            {
                foreach (OrderItem orderItem in currentOrderItems)
                {
                    using (connection = new SqlConnection(connectionString))
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue("@OrderId", id);
                        command.Parameters.AddWithValue("@ItemId", orderItem.GetInt());
                        command.Parameters.AddWithValue("@Quantity", orderItem.Quantity);
                        command.Parameters.AddWithValue("@SellingPrice", orderItem.SellingPrice);
                        command.Parameters.AddWithValue("@Total", orderItem.Total);

                        command.ExecuteNonQuery();
                    }
                }

                currentOrderItems.Clear();
            }
            //catch (Exception e)
            //{
            //    exception = e.StackTrace;
            //}
        }
        public void PulUpDb()
        {
            GetAllOrders();
            GetAllItems();
            SetAllOrderItems();
        }
        public List<OrderItem> GetOrderItemsById(int orderId)
        {
            GetAllItems();
            
            List<OrderItem> orderItems = new List<OrderItem>();
            
            string query = "select * from [OrderItems]";

            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);

                for (int i = 0, k = table.Rows.Count; i < k; i++)
                {
                    int OrderId = Convert.ToInt32(table.Rows[i][1].ToString());
                    int ItemId = Convert.ToInt32(table.Rows[i][2].ToString());
                    int quantity; int.TryParse(table.Rows[i][3].ToString(), out quantity);
                    float sellingPrice; float.TryParse(table.Rows[i][4].ToString(), out sellingPrice);

                    if (orderId == OrderId)
                    {
                        OrderItem orderItem = new OrderItem(GetItemById(ItemId), quantity, sellingPrice);                       
                        orderItems.Add(orderItem);
                    }
                }
            }
            currentOrderItems = orderItems;
            return currentOrderItems;
        }
        public Item GetItemById(int id)
        {
            foreach (Item item in items)
            {
                if (id == item.Id)
                {
                    return item;
                }
            }

            return null;
        }
        public void UpdateItem()
        {
            string query = "update [Items] set Category = @Category, Type = @Type, Brand = @Brand, Description = @Description, Characteristics = @Characteristics, PriceIn = @PriceIn, PriceOut = @PriceOut where Id = @Id";

            //try
            {
                using (connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {                  
                    connection.Open();

                    command.Parameters.AddWithValue("@Id", currentItem.Id);
                    command.Parameters.AddWithValue("@Category", currentItem.Category);
                    command.Parameters.AddWithValue("@Type", currentItem.Type);
                    command.Parameters.AddWithValue("@Brand", currentItem.Brand);
                    command.Parameters.AddWithValue("@Description", currentItem.Description);
                    command.Parameters.AddWithValue("@Characteristics", currentItem.Characteristics);
                    command.Parameters.AddWithValue("@PriceIn", currentItem.PriceIn);
                    command.Parameters.AddWithValue("@PriceOut", currentItem.PriceOut);

                    command.ExecuteNonQuery();
                }

                currentItem = null;        
            }
        }
        public void UpdateOrder(Order order)
        {
            GetAllItems();

            List<OrderItem> orderItems = new List<OrderItem>();
        
            string query = "update [Orders] set Date = @Date, DeliveryAddress = @DeliveryAddress, OrderStatus = @OrderStatus, Total = @Total  where Id = @Id";

            {
                using (connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    connection.Open();

                    command.Parameters.AddWithValue("@Id", order.Id);
                    command.Parameters.AddWithValue("@Date", order.OrderDate);
                    command.Parameters.AddWithValue("@DeliveryAddress", order.Address);
                    command.Parameters.AddWithValue("@OrderStatus", order.Status);
                    command.Parameters.AddWithValue("@Total", order.Total);

                    command.ExecuteNonQuery();
                }

                InsertOrderItem(order, order.Id);

            }
        }
        public void DeleteOrder()
        {
            allOrders.Remove(currentOrder);

            string queryOrderDelete = "delete from [OrderItems] where OrderId = @OrderId";
                
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(queryOrderDelete, connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@OrderId", currentOrder.Id);

                command.ExecuteNonQuery();

            }

            string query = "delete from [Orders] where Id = @Id";

            {
                using (connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@Id", currentOrder.Id);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeleteOrderItem(OrderItem orderItem)
        {
            string query = "delete from [OrderItems] where ItemId = @ItemId and Quantity = @Quantity and SellingPrice = @SellingPrice";

            {
                using (connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@ItemId", orderItem.GetInt());
                    command.Parameters.AddWithValue("@Quantity", orderItem.Quantity);
                    command.Parameters.AddWithValue("@SellingPrice", orderItem.SellingPrice);

                    command.ExecuteNonQuery();
                }
            }
        }        

        // generate report
        public List<Order> GenerateReport(DateTime from, DateTime to)
        {
            List<Order> sorted = new List<Order>();
            List<Order> unsorted  = GetAllProcessedOrders();

            foreach(Order item in unsorted)
            {
                if(item.OrderDate > from && item.OrderDate < to)
                {
                    sorted.Add(item);
                }
            }            
            return sorted;
        }
    }
}
