using C__Basics;

class Program
{
    static void PassByValueAndReference(int x, ref int y)
    {
        Console.WriteLine("In function PassByValueandReference");
        Console.WriteLine("x is passsed by value and y is by refernce");
        Console.WriteLine($"x=x+10= {x += 10} , y=y+10 = {y += 10}");

    }
    static void Main(string[] args)
    {

        ShoppingCart shoppingCart = new ShoppingCart();
        int numberOfItems = int.Parse(Console.ReadLine());
        List<double> items = new();
        Console.Write("Enter the items: ");
        for (int i = 0; i < numberOfItems; i++)
        {
            Console.Write($"Enter Item No:{i} ");
            double item = double.Parse(Console.ReadLine());
            items.Add(item);
        }

        //Console.Write("Enter Discout: ");
        //int discount = int.Parse(Console.ReadLine());
        //shoppingCart.CalculateTotalPrice(items, discount);



        //Console.Write("Enter x:");
        //int x = int.Parse(Console.ReadLine());
        //Console.Write("Enter y:");
        //int y = int.Parse(Console.ReadLine());
        //MathHelper mathHelper = new MathHelper();
        //Console.WriteLine($"X= {x}, Y={y}");

        //Console.WriteLine("After Swapping");
        //mathHelper.SwapIntegers(ref x, ref y);
        //Console.WriteLine($"X= {x}, Y={y}");

        //int remainder;
        //int quotient;
        //mathHelper.DivideWithRemainder(x, y, out remainder, out quotient);
        //Console.WriteLine($"x/y={quotient} and x%y= {remainder}");








        //Console.Write("Enter X: ");
        //int x = int.Parse(Console.ReadLine());
        //Console.Write("Enter Y: ");
        //int y = int.Parse(Console.ReadLine());
        //Console.WriteLine($"In main: x={x}, y={y}");
        //PassByValueAndReference(x, ref y);
        //Console.WriteLine($"Back In main: x={x}, y={y}");



        //Console.WriteLine("Adding FIve Players in Game: ");
        //for (int i = 0; i < 5; i++)
        //{
        //    MultiplayerGame.AddPlayer();
        //}
        //Console.WriteLine("Players Count: " + MultiplayerGame.GetPlayerCount());
        //Console.WriteLine("Removing 2 players: ");
        //MultiplayerGame.RemovePlayer();
        //MultiplayerGame.RemovePlayer();
        //Console.WriteLine("Players Count: " + MultiplayerGame.GetPlayerCount());

        //var Name = "Zohaib";
        //Console.WriteLine("The type of var Name: " + Name.GetType());
        //var Age = 20;
        //Console.WriteLine("The type of var Age: " + Age.GetType() + "\n");

        //dynamic data;
        //data = 10;
        //Console.WriteLine($"Value: {data}, Type: {data.GetType()}");
        //data = "jasdas";
        //Console.WriteLine($"Value: {data}, Type: {data.GetType()}");
        //data = 34.6;
        //Console.WriteLine($"Value: {data}, Type: {data.GetType()}");
        //data = 0.03M;
        //Console.WriteLine($"Value: {data}, Type: {data.GetType()}");
        //data = 9.03f;
        //Console.WriteLine($"Value: {data}, Type: {data.GetType()}");




    }

}

