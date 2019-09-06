namespace Assets.Scripts.Workers.Helpers.Extensions
{
    public static class GenericExtensions
    {
        public static bool EqualsAny<T>(this T input, params T[] searchArray)
        {
            foreach(T item in searchArray)
            {
                if (input.Equals(item))
                {
                    return true;
                }
            }
            return false; 
        }
    }
}
