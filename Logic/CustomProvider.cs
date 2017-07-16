using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class CustomProvider : IFormatProvider, ICustomFormatter
    {
        private readonly IFormatProvider _parent;

        public CustomProvider() : this(CultureInfo.CurrentCulture) {}

        public CustomProvider(IFormatProvider parent)
        {
            _parent = parent;
        }

        public object GetFormat(Type formatType)
        {
            return (formatType == typeof(ICustomFormatter)) ? this : null;
        }

        public string Format(string format, object arg, IFormatProvider prov)
        {
            Customer customer = arg as Customer;

            if (customer == null || format != "A")
                return string.Format(_parent, format, customer);

            return $"Name: {customer.Name}, Phone number: {customer.ContactPhone}, Revenue: {customer.Revenue}.";
        }
    }
}
