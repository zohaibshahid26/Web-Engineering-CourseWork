using System.Data.SqlClient;
namespace QueenLocalDataHandling
{
    internal class OrderCRUD
    {
        // Inserting Order in the system by taking Order object as parameter
        public void InsertOrder(Order order)
        {
            try
            {
                string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=QueensDB;Integrated Security=True;";
                SqlConnection sqlCon = new(connection);
                sqlCon.Open();
                string query = $"Insert into Orders values({order.OrderId},'{order.CustomerName}','{order.CustomerCnic}','{order.CustomerPhone}','{order.CustomerAddress}',{order.ProductID},{order.Price},'{order.ProductSize}')";
                SqlCommand cmd = new(query, sqlCon);
                cmd.ExecuteNonQuery();
                Console.WriteLine("\nOrder successfully inserted in the system!\n");
                sqlCon.Close();
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("Violation of PRIMARY KEY constraint"))
                {
                    Console.WriteLine("\nOrder already exists in the system\n");
                }
                else
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        // Getting all the orders from the system and displaying them on the console
        public void GetAllOrders()
        {
            try
            {
                string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=QueensDB;Integrated Security=True;";
                SqlConnection sqlCon = new(connection);
                sqlCon.Open();
                string query = "Select * from Orders";
                SqlCommand cmd = new(query, sqlCon);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    int count = 1;
                    Console.WriteLine("----------------Orders in the system----------------\n");
                    while (dr.Read())
                    {
                        Console.WriteLine($"{count++}- Order ID: {dr[0]}\nCustomer Name: {dr[1]}\nCustomer CNIC: {dr[2]}\nCustomer Phone: {dr[3]}\nCustomer Address: {dr[4]}\nProduct ID: {dr[5]}\nPrice: {dr[6]}\nProduct Size: {dr[7]}\n");
                    }
                }
                else
                {
                    Console.WriteLine("No orders found in the system\n");
                }
                sqlCon.Close();
                dr.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        // Updating the address of the customer by taking phone number and new address as parameters
        public void UpdateAddress(string PhoneNo, string Address)
        {
            try
            {
                string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=QueensDB;Integrated Security=True;";
                SqlConnection sqlCon = new(connection);
                sqlCon.Open();
                string query = $"Update Orders set Customer_Address='{Address}' where Customer_Phone='{PhoneNo}'";
                SqlCommand cmd = new(query, sqlCon);
                if (cmd.ExecuteNonQuery() == 0)
                {
                    Console.WriteLine("\nNo record found in the system\n");
                }
                else
                {
                    Console.WriteLine("\nRecord updated in the system\n");
                }
                sqlCon.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        // Deleting the order from the system by taking OrderID as parameter
        public void DeleteOrder(int OrderID)
        {
            try
            {
                string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=QueensDB;Integrated Security=True;";
                SqlConnection sqlCon = new(connection);
                sqlCon.Open();
                string query = $"Delete from Orders where OrderID={OrderID}";
                SqlCommand cmd = new(query, sqlCon);
                if (cmd.ExecuteNonQuery() == 0)
                {
                    Console.WriteLine("\nNo record found in the system\n");
                }
                else
                {
                    Console.WriteLine("\nRecord deleted from the system\n");
                }
                sqlCon.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

        }

        // Updating the address of the customer by taking phone number and new address as parameters using parameterized query
        public void updateOrderAddress(string PhoneNo, string Address)
        {
            try
            {
                string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=QueensDB;Integrated Security=True;";
                SqlConnection sqlCon = new(connection);
                sqlCon.Open();
                string query = $"Update Orders set Customer_Address=@address where Customer_Phone=@phoneNo";
                SqlCommand cmd = new(query, sqlCon);
                cmd.Parameters.AddWithValue("@address", Address);
                cmd.Parameters.AddWithValue("@phoneNo", PhoneNo);
                if (cmd.ExecuteNonQuery() == 0)
                {
                    Console.WriteLine("\nNo record found\n");
                }
                else
                {
                    Console.WriteLine("\nRecord updated int the system\n");
                }
                sqlCon.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

    }
}