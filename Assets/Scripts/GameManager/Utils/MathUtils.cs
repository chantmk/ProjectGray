﻿using UnityEngine;

namespace Utils
{
    public static class MathUtils
    {
        public static int Mod(int x, int m) 
        {
            int r = x%m;
            return r<0 ? r+m : r;
        }

        public static bool IsFloatEqual(float x, float y)
        {
            return Mathf.Abs(x - y) < float.Epsilon;
        }

        public static Vector2 RandomVec2(float min, float max)
        {
            return new Vector2(Random.Range(min, max), Random.Range(min, max));
        }
        
    }

    public static class GrayConstants
    {
        public const float EPSILON = 1e-5f;
        public const float MINIMUM_TIME = 1e-2f;
    }
}