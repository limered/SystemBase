using System;
using UnityEngine;

namespace Assets.Utils
{
    public static class ExceptionUtils
    {
        public static string ToLongMessage(this Exception e)
        {
            return e.GetType().FullName + ": " + e.Message + "\n" + e.StackTrace;
        }

        public static void PrintException(this Exception e)
        {
            Debug.LogError(e.ToLongMessage());
        }
    }
}
