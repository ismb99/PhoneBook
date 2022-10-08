using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhoneBook
{
    internal class Validator
    {
        public static bool IsStringValid(string str)
        {
            bool isValid = true;
            if (String.IsNullOrEmpty(str))
                isValid = false;

            else
            {
                isValid = Regex.IsMatch(str, @"^[a-zA-Z]+$");

                foreach (char c in str)
                {
                    if (!Char.IsLetter(c))
                        isValid = false;
                }
            }
            return isValid;
        }


        public static bool IsOnlyDigits(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return false;
            }

            foreach (char c in str)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
      
        public static bool IsValidEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;

            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}
