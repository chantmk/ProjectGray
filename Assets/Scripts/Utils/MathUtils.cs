namespace Utils
{
    public static class MathUtils
    {
        public static float Epsilon = 1e-5f;
        public static int Mod(int x, int m) {
            int r = x%m;
            return r<0 ? r+m : r;
        }
    }
}