using QueenLocalDataHandling;
class Program
{

    static void Main(string[] args)
    {
        OrderCRUD orderCrud = new();
        while (true)
        {
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("\t\tWelcome to the Queen's Shop");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("1. Create Order");
            Console.WriteLine("2. Read Order");
            Console.WriteLine("3. Update Order");
            Console.WriteLine("4. Delete Order");
            Console.WriteLine("5. Confirm Order");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice from (1-6): ");
            string choiceString = Console.ReadLine();
            int choice = InputValidation.IntegerValidation(choiceString);
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("\n-------------------1-Insert Order-------------------\n");

                    Order order = new Order { CustomerId = "", ProductCode = "", ProductSize = "", ProductQuantity = 0 };
                    Console.Write("Enter Order ID: ");
                    order.OrderId = InputValidation.IntegerValidation(Console.ReadLine());
                    Console.Write("Enter Customer ID: ");
                    order.CustomerId = Console.ReadLine();
                    Console.Write("Enter Product Code: ");
                    order.ProductCode = Console.ReadLine();
                    Console.Write("Enter Product Size: ");
                    order.ProductSize = InputValidation.inputValidater(Console.ReadLine(), "Product Size");
                    Console.Write("Enter Product Quantity: ");
                    order.ProductQuantity = InputValidation.IntegerValidation(Console.ReadLine());
                    orderCrud.InsertOrder(order);
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("\n-------------------2-Read Orders-------------------\n");
                    Console.Write("Enter the Order ID: ");
                    int orderId = InputValidation.IntegerValidation(Console.ReadLine());
                    orderCrud.GetOrders(orderId);
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("\n-------------------3-Update Order-------------------\n");
                    Order orderUpdate = new Order { CustomerId = "", ProductCode = "", ProductSize = "", ProductQuantity = 0 };
                    Console.Write("Enter Order ID to update: ");
                    orderUpdate.OrderId = InputValidation.IntegerValidation(Console.ReadLine());
                    Console.Write("Enter the Product Code: ");
                    orderUpdate.ProductCode = Console.ReadLine();
                    Console.Write("Enter the Product Size: ");
                    orderUpdate.ProductSize = InputValidation.inputValidater(Console.ReadLine(), "Product Size");
                    Console.Write("Enter the Product Quantity: ");
                    orderUpdate.ProductQuantity = InputValidation.IntegerValidation(Console.ReadLine());
                    orderCrud.UpdateOrder(orderUpdate);
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("\n-------------------4-Delete Order-------------------\n");
                    Console.Write("Enter Order ID to delete:  ");
                    int id = InputValidation.IntegerValidation(Console.ReadLine());
                    orderCrud.DeleteOrder(id);
                    break;
                case 5:
                    Console.Clear();
                    Console.WriteLine("\n-------------------5-Confim Order-------------------\n");
                    orderCrud.ConfirmOrder();

                    break;
                case 6:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}