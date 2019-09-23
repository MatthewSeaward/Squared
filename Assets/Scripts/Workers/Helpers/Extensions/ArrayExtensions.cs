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
        
        public static T RandomElement<T>(this T[] array)
        {
            return array[UnityEngine.Random.Range(0, array.Length)];
        }
    }
}
