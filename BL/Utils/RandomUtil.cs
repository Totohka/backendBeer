namespace BL.Utils
{
    public static class RandomUtil
    {
        [ThreadStatic]
        private static Random __random;

        public static Random Random => __random ?? (__random = new Random());
    }
}
