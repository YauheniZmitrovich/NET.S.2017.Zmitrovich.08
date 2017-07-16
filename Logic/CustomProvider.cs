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

        public CustomProvider() : this(CultureInfo.CurrentCulture) { }

        public CustomProvider(IFormatProvider parent)
        {
            _parent = parent;
        }

        public object GetFormat(Type formatType)
        {
            return (formatType == typeof(ICustomFormatter)) ? this : null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg==null)
                throw  new ArgumentNullException();


            string str = string.Empty;

            for (int i = 0; i < format.Length; i++)
            {
                switch (char.ToUpper(format[i]))
                {
                    case 'N':
                        str+= $"Name: {arg.ToString()}";
                        break;
                    case 'P':
                        str += $"Phone: {arg.ToString()}";
                        break;
                    case 'R':
                        str += $"Revenue: {arg.ToString()}";
                        break;
                }
                if (i != format.Length - 1)
                    str+=", ";
            }

            return str;
        }
    }
}
