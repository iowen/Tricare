using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriCare.Models;

namespace TriCare.Validators
{
    public class PrescriberValidator 
    {
        public static bool Validate(Prescriber item, out string message)
        {
            if (string.IsNullOrWhiteSpace(item.Address))
            {
                message = "Address is required";
                return false;
            }
            if (string.IsNullOrWhiteSpace(item.City))
            {
                message = "City is required";
                return false;
            }
            if (string.IsNullOrWhiteSpace(item.State))
            {
                message = "State is required";

                return false;
            }
            if (item.Zip <= 0)
            {
                message = "Zip is required";

                return false;
            }
            if (string.IsNullOrWhiteSpace(item.FirstName))
            {
                message = "First Name is required";

                return false;
            }
            if (string.IsNullOrWhiteSpace(item.LastName))
            {
                message = "Last Name is required";

                return false;
            }
            if (string.IsNullOrWhiteSpace(item.Email))
            {
                message = "Email is required";

                return false;
            }
            if (string.IsNullOrWhiteSpace(item.DeaNumber))
            {
                message = "DEA Number is required";

                return false;
            }
            if (string.IsNullOrWhiteSpace(item.LicenseNumber))
            {
                message = "License is required";

                return false;
            }
            if (string.IsNullOrWhiteSpace(item.NpiNumber))
            {
                message = "NPI Number is required";

                return false;
            }
            if (string.IsNullOrWhiteSpace(item.Password))
            {
                message = "Password is required";

                return false;
            }
            if (string.IsNullOrWhiteSpace(item.Phone))
            {
                message = "Phone is required";

                return false;
            }
            if (string.IsNullOrWhiteSpace(item.Fax))
            {
                message = "Fax is required";

                return false;
            }
            message = "";

            return true;
        }
    }
}
