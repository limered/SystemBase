using UnityEngine;
using Utils;

namespace SystemBase
{
    public class GameComponent : MonoBehaviour, IGameComponent
    {
        protected void Start()
        {
            RegisterToGame();
        }

        public void RegisterToGame()
        {
            IoC.Resolve<Game>().RegisterComponent(this);
        }

        public virtual IGameSystem System { get; private set; }
    }
}
