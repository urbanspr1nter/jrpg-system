using System;
using UnityEngine;

namespace Jrpg.Unity
{
    public class VectorUtil
    {
        public static float Distance(Vector2 first, Vector2 second)
        {
            return (float)Math.Sqrt(
                Math.Pow(first.x - second.x, 2) + Math.Pow(first.y - second.y, 2)
            );
        }
    }
}
