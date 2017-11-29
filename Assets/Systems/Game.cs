using System;
using System.Linq;
using SystemBase;
using Utils;

namespace Systems
{
    public class Game : GameBase
    {
        // Why habe this? Maybe Pause Game etc...
        private void Awake()
        {
            IoC.RegisterSingleton(this);

            #region System Registration

            foreach (var t in from a in AppDomain.CurrentDomain.GetAssemblies()
                              from t in a.GetTypes()
                              where Attribute.IsDefined(t, typeof(GameSystemAttribute))
                              select t)
            {
                RegisterSystem(Activator.CreateInstance(t) as IGameSystem);
            }

            #endregion System Registration

            Init();
        }
    }
}