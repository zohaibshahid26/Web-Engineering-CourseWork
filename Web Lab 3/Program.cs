using System.Data;

class Program
{
    static void Main(string[] args)
    {

        DataTable Order = new DataTable();
        DataTable Product = new DataTable();
        DataTable ProductOrder = new DataTable();


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

        Product.PrimaryKey = new DataColumn[] { pidColumn };

        DataColumn oidColumn = new DataColumn("OrderId", typeof(int));
        DataColumn createdAtColumn = new("CreatedAt", typeof(DateTime));
        oidColumn.AutoIncrement = true;
        oidColumn.AutoIncrementSeed = 1;
        oidColumn.AutoIncrementStep = 1;

        Order.Columns.Add(oidColumn);
        Order.Columns.Add(createdAtColumn);
        Order.PrimaryKey = new DataColumn[] { oidColumn };


        DataColumn orderIdColumn = new DataColumn("OrderId", typeof(int));
        DataColumn productIdColumn = new DataColumn("ProductId", typeof(int));
        ProductOrder.Columns.Add(productIdColumn);
        ProductOrder.Columns.Add(orderIdColumn);
        ProductOrder.PrimaryKey = [orderIdColumn, productIdColumn];


        DataSet dataSet = new();
        dataSet.Tables.Add(Product);
        dataSet.Tables.Add(Order);
        dataSet.Tables.Add(ProductOrder);

        DataRelation relationProduct = new DataRelation("ProductRelation", Product.Columns["ProductId"], ProductOrder.Columns["ProductId"]);
        dataSet.Relations.Add(relationProduct);
        DataRelation relationOrder = new DataRelation("OrderRelation", Order.Columns["OrderId"], ProductOrder.Columns["OrderId"]);
        dataSet.Relations.Add(relationOrder);


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

        foreach (DataRow row in Order.Rows)
        {
            Console.WriteLine($"{row[0]}   {row[1]}");
        }

        foreach (DataRow row in Product.Rows)
        {
            Console.WriteLine($"{row[0]}   {row[1]}    {row[2]}    {row[3]}");
        }

        DataRow[] ProductOrderRows = new DataRow[5];
        ProductOrderRows[0] = ProductOrder.NewRow();
        ProductOrderRows[0]["OrderId"] = ProductRows[0][0];
        ProductOrderRows[0]["ProductId"] = OrderRows[1][0];

        ProductOrderRows[1] = ProductOrder.NewRow();
        ProductOrderRows[1]["OrderId"] = ProductRows[0][0];
        ProductOrderRows[1]["ProductId"] = OrderRows[2][0];

        ProductOrderRows[2] = ProductOrder.NewRow();
        ProductOrderRows[2]["OrderId"] = ProductRows[2][0];
        ProductOrderRows[2]["ProductId"] = OrderRows[2][0];

        ProductOrderRows[3] = ProductOrder.NewRow();
        ProductOrderRows[3]["OrderId"] = ProductRows[3][0];
        ProductOrderRows[3]["ProductId"] = OrderRows[0][0];

        ProductOrderRows[4] = ProductOrder.NewRow();
        ProductOrderRows[4]["OrderId"] = ProductRows[1][0];
        ProductOrderRows[4]["ProductId"] = OrderRows[0][0];

        //foreach (DataRow row in ProductOrderRows)
        //{
        //    ProductOrder.Rows.Add(row);

        //}

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


        Console.Write("Enter order id: ");
        int oid = int.Parse(Console.ReadLine());
        DataRow[] childRows = Order.Rows[0].GetChildRows(relationOrder);
        if (childRows.Length != 0)
        {

            Console.WriteLine($"The Name of the Products in Order {oid}");
            foreach (DataRow row in childRows)
            {
                Console.WriteLine($"{row[1]}");
            }
        }
        else
            Console.WriteLine("No Prducts Found");

        DateTime datetime = DateTime.Parse(Console.ReadLine());
        foreach (DataRow row in Order.Rows)
        {
            {
                row.

            }


        }

    }




}



