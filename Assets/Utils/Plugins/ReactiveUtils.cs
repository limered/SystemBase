using System;
using UniRx;
using UnityEngine;

namespace Utils.Plugins
{
    public static class ReactiveUtils
    {
        public static IObservable<T> LogOnNext<T>(this IObservable<T> obs, string format = null)
        {
            return obs.Select(t => { Debug.Log(format == null ? (t == null ? null : t.ToString()) : string.Format(format, t)); return t; });
        }

        public static IObservable<T> LogError<T>(this IObservable<T> obs, Func<Exception, string> convertToString)
        {
            return obs.Do(x => { }, err =>
            {
                if (convertToString != null) Debug.LogError(convertToString(err));
                else Debug.LogError(err.Message + "\n" + err.StackTrace);
            });
        }

        public static IObservable<T> LogError<T>(this IObservable<T> obs)
        {
            return obs.Do(x => { }, Debug.LogException);
        }
    }
}
