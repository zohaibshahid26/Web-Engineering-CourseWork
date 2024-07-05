namespace QueenLocalDataHandling
{
    // This class is used to validate the input from the user
    internal static class InputValidation
    {
        // This method is used to validate the integer input from the user
        static public int IntegerValidation(string input)
        {
            string newId = input;
            while (true)
            {
                if (ValidateInteger(newId))
                {
                    return int.Parse(newId);
                }
                else
                {
                    Console.Write("Invalid Input. Enter Again: ");
                    newId = Console.ReadLine();
                }
            }
        }
        static public bool ValidateInteger(string input)
        {
            if (input.Length == 0)
                return false;

            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsDigit(input[i]))
                    return false;
            }
            return true;
        }

        // This method is used to validate the string input from the user
        static public string inputValidater(string input, string inputType)
        {

            string newInput = input;
            while (true)
            {
                if (ValidateInput(newInput))
                {
                    return newInput;
                }
                else
                {
                    Console.Write($"Invalid {inputType}. Enter {inputType}: ");
                    newInput = Console.ReadLine();
                }
            }
        }
        static public bool ValidateInput(string input)
        {
            if (input.Length == 0)
                return false;
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]))
                    return false;
            }
            return true;
        }

        // This method is used to validate the phone input from the user
        public static string PhoneValidation(string phone)
        {
            string newPhone = phone;
            while (true)
            {
                if (ValidatePhone(newPhone))
                {
                    return newPhone;
                }
                else
                {
                    Console.Write("Invalid Phone Number. Valid Phone format: 03XX-XXXXXXX\nEnter Phone Number: ");
                    newPhone = Console.ReadLine();
                }
            }
        }
        public static bool ValidatePhone(string phone)
        {
            if (phone.Length != 12)
                return false;
            if (phone[4] != '-')
                return false;
            if (phone[0] != '0' || phone[1] != '3')
                return false;
            for (int i = 0; i < phone.Length; i++)
            {
                if (i == 4)
                    continue;
                if (!char.IsDigit(phone[i]))
                    return false;
            }
            return true;
        }
    }

}