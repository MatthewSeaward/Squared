namespace Assets.Scripts.Workers.Helpers.Extensions
{
    public static class ArrayExtensions
    {
        public static T SafeGet<T>(this T[] array, int index)
        {
            if (array.Length <= index)
            {
                return default(T);
            }
            else
            {
                return array[index];
            }
        }
    }
}
