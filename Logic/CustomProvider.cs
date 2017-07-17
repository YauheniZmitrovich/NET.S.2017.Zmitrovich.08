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
        #region Private fields

        private readonly IFormatProvider _parent;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomProvider"/> class.
        /// </summary>
        public CustomProvider() : this(CultureInfo.CurrentCulture) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomProvider"/> class.
        /// </summary>
        public CustomProvider(IFormatProvider parent)
        {
            _parent = parent;
        }

        #endregion

        #region Interfaces implementations

        /// <summary>
        /// Returns an object that provides formatting services for the specified type.
        /// </summary>
        /// <param name="formatType">  
        /// An object that specifies the type of format object to return. 
        /// </param>
        /// <returns>    
        /// An instance of the object specified by formatType, if the System.IFormatProvider
        /// implementation can supply that type of object; otherwise, null.
        ///</returns>
        public object GetFormat(Type formatType)
        {
            return (formatType == typeof(ICustomFormatter)) ? this : null;
        }

        /// <summary>
        /// Converts the value of a specified object to an equivalent string representation
        /// using specified format and culture-specific formatting information.
        /// </summary>
        /// <param name="format"> A format string containing formatting specifications. </param>
        /// <param name="arg"> An object to format. </param>
        /// <param name="formatProvider"> 
        /// An object that supplies format information about the current instance.
        /// </param>
        /// <returns>
        /// The string representation of the value of arg, formatted as specified by format
        /// and formatProvider.
        ///</returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null)
                throw new ArgumentNullException();

            if (format == null)
            {
                return string.Format(_parent, "{0:" + format + "}", arg);
            }

            string str = string.Empty;

            for (int i = 0; i < format.Length; i++)
            {
                switch (char.ToUpper(format[i]))
                {
                    case 'N':
                        str += $"Name: {arg.ToString()}";
                        break;
                    case 'P':
                        str += $"Phone: {arg.ToString()}";
                        break;
                    case 'R':
                        str += $"Revenue: {arg.ToString()}";
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
