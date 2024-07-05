using System.Data.SqlClient;
using System.Data;
namespace QueenLocalDataHandling
{
    internal class OrderCRUD
    {
        private readonly DataSet dataSet;
        private readonly SqlDataAdapter dataAdapter;
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=QueensDB2.0;Integrated Security=True;";
        public OrderCRUD()
        {
            try
            {
                // Creating the dataset and dataadapter objects
                dataSet = new("QueensDB2.0");
                dataAdapter = new();

                dataSet.EnforceConstraints = false;

                // Creating the Orders table in memory
                DataTable orderTable = new("Orders");
                orderTable.Columns.Add("OrderId", typeof(int));
                orderTable.Columns.Add("ProductCode", typeof(string));
                orderTable.Columns.Add("CustomerId", typeof(string));
                orderTable.Columns.Add("ProductSize", typeof(string));
                orderTable.Columns.Add("ProductQuantity", typeof(int));
                orderTable.PrimaryKey = new DataColumn[] { orderTable.Columns["OrderId"], orderTable.Columns["ProductCode"], orderTable.Columns["ProductSize"] };
                dataSet.Tables.Add(orderTable);


                // Creating the Products table in memory
                DataTable productTable = new("Product");
                productTable.Columns.Add("ProductCode", typeof(string));
                productTable.Columns.Add("ProductName", typeof(string));
                productTable.Columns.Add("ProductPrice", typeof(int));
                productTable.Columns.Add("ProductPicture", typeof(string));
                productTable.Columns.Add("ProductSize", typeof(string));
                productTable.PrimaryKey = new DataColumn[] { productTable.Columns["ProductCode"], productTable.Columns["ProductSize"] };
                dataSet.Tables.Add(productTable);


                // Creating the Customer table in memory
                DataTable customerTable = new("Customer");
                customerTable.Columns.Add("CustomerId", typeof(string));
                customerTable.Columns.Add("CustomerName", typeof(string));
                customerTable.Columns.Add("CustomerContact", typeof(string));
                customerTable.Columns.Add("CustomerAddress", typeof(string));
                customerTable.PrimaryKey = [customerTable.Columns["CustomerId"]];
                dataSet.Tables.Add(customerTable);

                //Creating the relationship between the tables
                DataRelation customerOrder = new("CustomerOrder", customerTable.Columns["CustomerId"], orderTable.Columns["CustomerId"], true);
                dataSet.Relations.Add(customerOrder);
                DataRelation productOrder = new("ProductOrder", [productTable.Columns["ProductCode"], productTable.Columns["ProductSize"]], [orderTable.Columns["ProductCode"], orderTable.Columns["ProductSize"]], true);
                dataSet.Relations.Add(productOrder);

                string orderQuery = "Select * from Orders";
                dataAdapter.SelectCommand = new(orderQuery, new(connectionString));
                dataAdapter.Fill(orderTable);

                string productQuery = "Select * from Product";
                dataAdapter.SelectCommand = new(productQuery, new(connectionString));
                dataAdapter.Fill(productTable);

                string customerQuery = "Select * from Customer";
                dataAdapter.SelectCommand = new(customerQuery, new(connectionString));
                dataAdapter.Fill(customerTable);

                dataSet.EnforceConstraints = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }



        // Get all the orders based on the Order ID
        public void GetOrders(int orderId)
        {
            try
            {
                DataRow[] orderRows = dataSet.Tables["Orders"].Select($"OrderId='{orderId}'");
                if (orderRows.Length == 0)
                {
                    Console.WriteLine($"\nNo orders found in the system for the Order ID: {orderId}\n");
                }
                else
                {
                    Console.WriteLine("\nOrders found in the system\n");
                    foreach (DataRow orderRow in orderRows)
                    {
                        Console.WriteLine($"Order ID: {orderRow["OrderId"]}");
                        Console.WriteLine($"Customer ID: {orderRow["CustomerId"]}");
                        Console.WriteLine($"Product Code: {orderRow["ProductCode"]}");
                        Console.WriteLine($"Product Size: {orderRow["ProductSize"]}");
                        Console.WriteLine($"Product Quantity: {orderRow["ProductQuantity"]}\n");
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }


        // Inserting the order in the system
        public void InsertOrder(Order order)
        {
            try
            {
                string insertQuery = $"Insert into Orders(OrderId,ProductCode,CustomerId,ProductSize,ProductQuantity) values(@OrderId,@ProductCode,@CustomerId,@ProductSize,@ProductQuantity)";
                SqlParameter orderIdParam = new("OrderId", SqlDbType.Int, 0, "OrderId");
                SqlParameter productCodeParam = new("ProductCode", SqlDbType.VarChar, 20, "ProductCode");
                SqlParameter customerIdParam = new("CustomerId", SqlDbType.VarChar, 20, "CustomerId");
                SqlParameter productSizeParam = new("ProductSize", SqlDbType.VarChar, 20, "ProductSize");
                SqlParameter productQuantityParam = new("ProductQuantity", SqlDbType.Int, 0, "ProductQuantity");
                SqlCommand insertCommand = new(insertQuery, new(connectionString));
                insertCommand.Parameters.Add(orderIdParam);
                insertCommand.Parameters.Add(productCodeParam);
                insertCommand.Parameters.Add(customerIdParam);
                insertCommand.Parameters.Add(productSizeParam);
                insertCommand.Parameters.Add(productQuantityParam);
                dataAdapter.InsertCommand = insertCommand;

                DataRow orderRow = dataSet.Tables["Orders"].NewRow();
                orderRow["OrderId"] = order.OrderId;
                orderRow["ProductCode"] = order.ProductCode;
                orderRow["CustomerId"] = order.CustomerId;
                orderRow["ProductSize"] = order.ProductSize;
                orderRow["ProductQuantity"] = order.ProductQuantity;

                dataSet.Tables["Orders"].Rows.Add(orderRow);

            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("Column 'OrderId, ProductCode, ProductSize' is constrained to be unique."))
                {
                    Console.WriteLine("\nOrder already exists in the system\n");
                }
                else if (exception.Message.Contains("ForeignKeyConstraint"))
                {
                    if (exception.Message.Contains("CustomerOrder"))
                    {
                        Console.WriteLine("\nCustomer does not exist in the system\n");
                    }
                    else
                    {
                        Console.WriteLine("\nProduct does not exist in the system\n");
                    }
                }
                else
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        // Updating the order in the system

        public void UpdateOrder(Order order)
        {
            try
            {
                string updateQuery = "Update Orders set ProductQuantity=@ProductQuantity where OrderId=@OrderId and ProductCode=@ProductCode and ProductSize=@ProductSize";
                SqlParameter orderIdParam = new("OrderId", SqlDbType.Int, 0, "OrderId");
                SqlParameter productCodeParam = new("ProductCode", SqlDbType.VarChar, 20, "ProductCode");
                SqlParameter productSizeParam = new("ProductSize", SqlDbType.VarChar, 20, "ProductSize");
                SqlParameter productQuantityParam = new("ProductQuantity", SqlDbType.Int, 0, "ProductQuantity");
                SqlCommand updateCommand = new(updateQuery, new(connectionString));
                updateCommand.Parameters.Add(orderIdParam);
                updateCommand.Parameters.Add(productCodeParam);
                updateCommand.Parameters.Add(productSizeParam);
                updateCommand.Parameters.Add(productQuantityParam);
                dataAdapter.UpdateCommand = updateCommand;


                DataRow[] orderRow = dataSet.Tables["Orders"].Select($"OrderId={order.OrderId} and ProductCode='{order.ProductCode}' and ProductSize='{order.ProductSize}'");
                if (orderRow.Length == 0)
                {
                    Console.WriteLine($"\nOrder does not exist in the system\n");
                    return;
                }
                else
                {
                    orderRow[0]["ProductCode"] = order.ProductCode;
                    orderRow[0]["ProductSize"] = order.ProductSize;
                    orderRow[0]["ProductQuantity"] = order.ProductQuantity;
                    Console.WriteLine("\nOrder has been updated in the system\n");
                }
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("Column 'OrderId, ProductCode, ProductSize' is constrained to be unique."))
                {
                    Console.WriteLine("\nOrder already exists in the system\n");
                }
                else if (exception.Message.Contains("ForeignKeyConstraint"))
                {
                    if (exception.Message.Contains("CustomerOrder"))
                    {
                        Console.WriteLine("\nCustomer does not exist in the system\n");
                    }
                    else
                    {
                        Console.WriteLine("\nProduct does not exist in the system\n");
                    }
                }
                else
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }



        // Deleting the order from the system by taking OrderID as parameter
        public void DeleteOrder(int OrderID)
        {
            try
            {
                string deleteQuery = "Delete from Orders where OrderId=@OrderId";
                SqlParameter orderIdParam = new("OrderId", SqlDbType.Int, 0, "OrderId");
                SqlCommand deleteCommand = new(deleteQuery, new(connectionString));
                deleteCommand.Parameters.Add(orderIdParam);
                dataAdapter.DeleteCommand = deleteCommand;
                DataRow[] orderRows = dataSet.Tables["Orders"].Select($"OrderId={OrderID}");
                if (orderRows.Length == 0)
                {
                    Console.WriteLine("\nOrder does not exist in the system\n");
                    return;
                }
                else
                {
                    foreach (DataRow orderRow in orderRows)
                    {
                        orderRow.Delete();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

        }

        //Synchronizing the changes in the database upon order conformation
        public void ConfirmOrder()
        {
            try
            {
                dataAdapter.Update(dataSet.Tables["Orders"]);
                dataAdapter.Update(dataSet.Tables["Product"]);
                dataAdapter.Update(dataSet.Tables["Customer"]);
                Console.WriteLine("\nChanges have been synchronized with the database\n");

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        ~OrderCRUD()
        {
            dataSet.Dispose();
            dataAdapter.Dispose();
        }

    }



}