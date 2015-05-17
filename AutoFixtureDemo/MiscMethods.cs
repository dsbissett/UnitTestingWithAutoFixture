using System;
using System.Net.Mail;

namespace AutoFixtureDemo
{
    public class MiscMethods
    {
        public bool IsEmailAddressValid(string email)
        {
            try
            {
                var result = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}