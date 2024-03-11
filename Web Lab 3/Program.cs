using System.Data;

class Program
{

    static void Main(string[] args)
    {

        DataTable Order = new DataTable("Order");
        DataTable Product = new DataTable("Product");
        DataTable ProductOrder = new DataTable("ProductOrder");


        DataColumn pidColumn = new DataColumn("ProductId", typeof(int));
        DataColumn nameColumn = new DataColumn("Name", typeof(string));
        DataColumn descColumn = new DataColumn("Desc", typeof(string));
        DataColumn priceColumn = new DataColumn("Price", typeof(int));

        pidColumn.AutoIncrement = true;
        pidColumn.AutoIncrementSeed = 1;
        pidColumn.AutoIncrementStep = 1;

        Product.Columns.Add(pidColumn);
        Product.Columns.Add(nameColumn);
        Product.Columns.Add(descColumn);
        Product.Columns.Add(priceColumn);

        Product.PrimaryKey = [pidColumn];

        DataColumn oidColumn = new DataColumn("OrderId", typeof(int));
        DataColumn createdAtColumn = new("CreatedAt", typeof(DateTime));
        oidColumn.AutoIncrement = true;
        oidColumn.AutoIncrementSeed = 1;
        oidColumn.AutoIncrementStep = 1;

        Order.Columns.Add(oidColumn);
        Order.Columns.Add(createdAtColumn);
        Order.PrimaryKey = [oidColumn];


        DataColumn orderIdColumn = new DataColumn("OrderId", typeof(int));
        DataColumn productIdColumn = new DataColumn("ProductId", typeof(int));
        ProductOrder.Columns.Add(productIdColumn);
        ProductOrder.Columns.Add(orderIdColumn);
        ProductOrder.PrimaryKey = [productIdColumn, orderIdColumn];


        DataSet dataSet = new();
        dataSet.Tables.Add(Product);
        dataSet.Tables.Add(Order);
        dataSet.Tables.Add(ProductOrder);

        DataRelation relationProduct = new DataRelation("ProductRelation", Product.Columns["ProductId"], ProductOrder.Columns["ProductId"]);
        dataSet.Relations.Add(relationProduct);
        DataRelation relationOrder = new DataRelation("OrderRelation", Order.Columns["OrderId"], ProductOrder.Columns["OrderId"]);
        dataSet.Relations.Add(relationOrder);

        // Adding rows to the tables

        DataRow[] ProductRows = new DataRow[5];
        ProductRows[0] = Product.NewRow();
        ProductRows[0]["Name"] = "Product 1";
        ProductRows[0]["Desc"] = "It is the product 1";
        ProductRows[0]["Price"] = 23;

        ProductRows[1] = Product.NewRow();
        ProductRows[1]["Name"] = "Product 2";
        ProductRows[1]["Desc"] = "It is the product 2";
        ProductRows[1]["Price"] = 245;

        ProductRows[2] = Product.NewRow();
        ProductRows[2]["Name"] = "Product 3";
        ProductRows[2]["Desc"] = "It is the product 3";
        ProductRows[2]["Price"] = 45;

        ProductRows[3] = Product.NewRow();
        ProductRows[3]["Name"] = "Product 4";
        ProductRows[3]["Desc"] = "It is the product 4";
        ProductRows[3]["Price"] = 233;

        ProductRows[4] = Product.NewRow();
        ProductRows[4]["Name"] = "Product 5";
        ProductRows[4]["Desc"] = "It is the product 5";
        ProductRows[4]["Price"] = 2223;

        foreach (DataRow row in ProductRows)
        {
            Product.Rows.Add(row);

        }

        DateTime date = new DateTime(2024, 3, 7, 2, 59, 23);
        DataRow[] OrderRows = new DataRow[3];
        OrderRows[0] = Order.NewRow();
        OrderRows[0]["CreatedAt"] = date;

        date = new DateTime(2024, 3, 7, 2, 51, 23);
        OrderRows[1] = Order.NewRow();
        OrderRows[1]["CreatedAt"] = date;

        date = new DateTime(2024, 3, 7, 2, 52, 13);
        OrderRows[2] = Order.NewRow();
        OrderRows[2]["CreatedAt"] = date;


        foreach (DataRow row in OrderRows)
        {
            Order.Rows.Add(row);
        }

        //Adding rows to the ProductOrder table

        DataRow[] ProductOrderRows = new DataRow[5];

        for (int i = 0; i < 5; i++)
        {
            ProductOrderRows[i] = ProductOrder.NewRow();
            ProductOrderRows[i]["ProductId"] = i + 1;
            ProductOrderRows[i]["OrderId"] = 1;
        }
        for (int i = 0; i < 5; i++)
        {
            ProductOrderRows[i]["ProductId"] = i + 1;
            ProductOrderRows[i]["OrderId"] = 2;
        }
        for (int i = 0; i < 5; i++)
        {
            ProductOrderRows[i]["ProductId"] = i + 1;
            ProductOrderRows[i]["OrderId"] = 3;
        }


        foreach (DataRow row in ProductOrderRows)
        {
            ProductOrder.Rows.Add(row);

        }

        //Task 1
        //Display all the products with matching product id


        Console.Write("Enter product id: ");
        int id = int.Parse(Console.ReadLine());
        DataRow p = Product.Rows.Find(id);
        if (p != null)
        {
            Console.WriteLine($"The required Product is:{p[0]}   {p[1]}    {p[2]}    {p[3]}");

        }
        else
        {
            Console.WriteLine("No Product Found");
        }

        //Task 2
        //Display all the products with matching order id

        Console.Write("Enter order id: ");
        int oid = int.Parse(Console.ReadLine());
        DataRow o = Order.Rows.Find(oid);
        if (o != null)
        {
            DataRow[] childRows = o.GetChildRows(relationOrder);
            if (childRows != null)
            {
                Console.WriteLine($"The required Products with the given OrderId {oid}are:");
                for (int i = 0; i < childRows.Length; i++)
                {
                    DataRow product = Product.Rows.Find(childRows[i][0]);
                    if (product != null)
                    {
                        Console.WriteLine($"{product[0]}   {product[1]}    {product[2]}    {product[3]}");
                    }

                }
            }
            else
                Console.WriteLine("No Products Found");
        }
        else
        {
            Console.WriteLine("No Order Found");
        }
        //Task 3
        //Display the orders which are created after the given date

        Console.Write("Enter the date: ");
        DateTime datetime = new DateTime(2024, 3, 7, 2, 52, 13);
        DataRow[] rows = Order.Select($"CreatedAt > '{datetime}'");
        if (rows.Length > 0)
        {
            Console.WriteLine($"The orders created after the {datetime} are:");
            foreach (DataRow row in rows)
            {
                Console.WriteLine($"{row[0]}   {row[1]}");
            }
        }
        else
        {
            Console.WriteLine("No Orders Found");
        }

    }
}



