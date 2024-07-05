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
            Console.WriteLine("1. Insert Order");
            Console.WriteLine("2. Get All Orders");
            Console.WriteLine("3. Update Address");
            Console.WriteLine("4. Delete Order");
            Console.WriteLine("5. Update Order Address");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice from (1-6): ");
            string choiceString = Console.ReadLine();
            int choice = InputValidation.IntegerValidation(choiceString);
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("\n-------------------1-Insert Order-------------------\n");
                    Order order = new Order { ProductSize = "" };
                    Console.Write("Enter Order ID: ");
                    order.OrderId = InputValidation.IntegerValidation(Console.ReadLine());
                    Console.Write("Enter Customer Name: ");
                    order.CustomerName = InputValidation.inputValidater(Console.ReadLine(), "Customer Name");
                    Console.Write("Enter Customer CNIC Format(XXXXX-XXXXXXX-X): ");
                    order.CustomerCnic = InputValidation.CNICValidation(Console.ReadLine());
                    Console.Write("Enter Customer Phone Formar(03XX-XXXXXXX) ");
                    order.CustomerPhone = InputValidation.PhoneValidation(Console.ReadLine());
                    Console.Write("Enter Customer Address: ");
                    order.CustomerAddress = InputValidation.inputValidater(Console.ReadLine(), "Customer Address");
                    Console.Write("Enter Product ID: ");
                    order.ProductID = InputValidation.IntegerValidation(Console.ReadLine());
                    Console.Write("Enter Price: ");
                    order.Price = InputValidation.IntegerValidation(Console.ReadLine());
                    Console.Write("Enter Product Size: ");
                    order.ProductSize = InputValidation.inputValidater(Console.ReadLine(), "Product Size");
                    orderCrud.InsertOrder(order);
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("\n-------------------2-Get All Orders-------------------\n");
                    orderCrud.GetAllOrders();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("\n-------------------3-Update Address-------------------\n");
                    Console.Write("Enter Phone Number to update Address: ");
                    string phone = InputValidation.PhoneValidation(Console.ReadLine());
                    Console.Write("Enter New Address: ");
                    string address = InputValidation.inputValidater(Console.ReadLine(), "Address");
                    orderCrud.UpdateAddress(phone, address);
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("\n-------------------4-Delete Order-------------------\n");
                    Console.Write("Enter Order ID to delete:  ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    orderCrud.DeleteOrder(id);
                    break;
                case 5:
                    Console.Clear();
                    Console.WriteLine("\n-------------------5-Update Order Address-------------------\n");
                    Console.Write("Enter Phone Number: ");
                    string phoneNo = InputValidation.PhoneValidation(Console.ReadLine());
                    Console.Write("Enter New Address: ");
                    string newAddress = InputValidation.inputValidater(Console.ReadLine(), "Address");
                    orderCrud.updateOrderAddress(phoneNo, newAddress);
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}