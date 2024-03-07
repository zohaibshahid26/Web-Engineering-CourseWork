using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1_web
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> Email { get; set; }

        public void AddEmails(params string[] emails)
        {
            for (int i = 0; i < emails.Length; i++)
            {
                Email.Add(emails[i]);
            }
        }
        public override string ToString()
        {
            string email = string.Empty;
            foreach (string s in Email)
            {
                email += s;
                email += " , ";
            }
            return "Name: " + Name + " " + " Age: " + Age + " Emails: " + email + '\n';
        }
        public void saveToFile(string path)
        {
            FileStream fin = new FileStream(path, FileMode.Append);
            string information = ToString();

            for (int i = 0; i < information.Length; i++)
            {
                fin.WriteByte((byte)information[i]);
            }
            fin.Close();

        }
    }
}
