using System;

namespace Assets.Scripts.Workers.Helpers
{
    public static class EnumHelpers
    {
        public static bool IsValue<T>(char c) where T : Enum
        {
            var values = Enum.GetValues(typeof(T));

            bool found = false;
            foreach(int value in values)
            {
                if (value  == c)
                {
                    found = true;
                    break;
                }
            }

            return found;
        }
    }
}