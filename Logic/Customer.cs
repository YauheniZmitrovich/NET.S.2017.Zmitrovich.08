using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Customer : IFormattable
    {
        #region Properties

        /// <summary>
        /// Name of the customer.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Contact phone number of the customer.
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// Revenue of the customer.
        /// </summary>
        public decimal Revenue { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        /// <param name="name"> Name of the customer. </param>
        /// <param name="contactPhone"> Contact phone number of the customer. </param>
        /// <param name="revenue"> Revenue of the customer. </param>
        public Customer(string name, string contactPhone, decimal revenue)
        {
            Name = name;
            ContactPhone = contactPhone;
            Revenue = revenue;
        }

        #endregion

        #region ToString methods

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns> A string that represents the current object. </returns>     
        public override string ToString()
        {
            return $"{Name}, {Revenue:###,###.00}, {ContactPhone}";
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <param name="format">
        /// The format to use.-or- A null reference to use the
        /// default format defined for the type of the System.IFormattable implementation.
        /// Use combination of characters 'N' - Name, 'P' - contact phone number, 'R'- Revenue
        /// For example: "NRP" => Jeffrey Richter, 1,000,000.00, +1 (425) 555-0100
        /// </param>
        /// <param name="formatProvider"> 
        ///  The provider to use to format the value.-or- A null reference
        ///  to obtain the numeric format information from the current locale setting
        ///  of the operating system.
        ///  </param>
        ///  <returns> The value of the current instance in the specified format. </returns> 
        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (string.IsNullOrEmpty(format) || format == "G")
                return $"{Name}, {Revenue:###,###.00}, {ContactPhone}";

            if (formatProvider == null)
                formatProvider = CultureInfo.CurrentCulture;
 
                    if (format.Length > 3)
                throw new ArgumentException();

            if (formatProvider == CultureInfo.CurrentCulture)
            {
                string allowChar = "NPRnpr";

                foreach (char c in format)
                {
                    int index = allowChar.IndexOf(c);
                    if (index == -1)
                        throw new ArgumentException();
                    allowChar = allowChar.Remove(index, 1); //Symbols can not be repeated.
                }
            }

            return GenerateStringByFormat(format, formatProvider);
        }

        #endregion

        #region  Private

        private string GenerateStringByFormat(string format, IFormatProvider formatProvider)
        {
            string str = string.Empty;

            for (int i = 0; i < format.Length; i++)
            {
                switch (char.ToUpper(format[i]))
                {
                    case 'N':
                        str += string.Format(formatProvider, "{0:N}", Name);
                        break;
                    case 'P':
                        str += string.Format(formatProvider, "{0:P}", ContactPhone);
                        break;
                    case 'R':
                        str += string.Format(formatProvider, "{0:R}", Revenue.ToString("###,###.00"));
                        break;
                }
                if (i != format.Length - 1)
                    str += ", ";
            }

            return str;
        }

        #endregion
    }
}
