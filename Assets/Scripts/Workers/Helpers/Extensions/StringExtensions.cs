using System;
using System.Text;

namespace Assets.Scripts.Workers.Helpers.Extensions
{
    public static class StringExtensions
    {
        public static string Spaced(this string input)
        {
            var sb = new StringBuilder();
            bool lastWasUpper = false;
            bool lastWasNumber = false;

            for (int i = 0; i < input.Length; i++)
            {
                var letter = input[i];

                bool isUpper = Char.IsUpper(letter);
                bool isNumber = Char.IsNumber(letter);

                if (sb.Length > 0)
                {


                    if ((isUpper && !lastWasUpper) || (isNumber && !lastWasNumber))
                    {
                        sb.Append(" ");
                    }

                    
                }
                lastWasUpper = isUpper;
                lastWasNumber = isNumber;
                sb.Append(letter);
            }

            return sb.ToString();
        }
    }
}
