
using System;
using Lab_1_web;
class Program

{
    static void Main(string[] args)
    {
        Person person = new Person  //Object Initializer
        {
            Name = "Zohaib",
            Age = 20,
            Email = new List<string>()
        };

        string[] emails = { "zohaibshahid200@gmail.com", "bsef21m008@pucit.edu.pk" };
        person.AddEmails(emails);   //Added Emails in person's object
        Console.WriteLine(person);  //Printed on the console

        person.saveToFile("file.txt");  //Saved the persons information to the file

    }

}
