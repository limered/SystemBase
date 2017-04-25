using System;
using System.Runtime.InteropServices;

namespace Assets.Utils
{
    public static class MarshallingUtils
    {
        public static void MarshalUnmananagedArray2Struct<T>(IntPtr unmanagedArray, int length, out T[] mangagedArray)
        {
            var size = Marshal.SizeOf(typeof(T));
            mangagedArray = new T[length];

            for (var i = 0; i < length; i++)
            {
                var ins = new IntPtr(unmanagedArray.ToInt32() + i * size);
                mangagedArray[i] = (T)Marshal.PtrToStructure(ins, typeof(T));
            }
        }

        [DllImport("LimeredVO", EntryPoint = "clearXYArray")]
        public static extern void ClearXYArray(IntPtr p);

        [DllImport("LimeredVO", EntryPoint = "clearByteArray")]
        public static extern void ClearByteArray(IntPtr ptr);
    }
}
