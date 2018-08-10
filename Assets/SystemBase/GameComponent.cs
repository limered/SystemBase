using Systems;
using UnityEngine;
using Utils;

namespace SystemBase
{
    public class GameComponent : MonoBehaviour, IGameComponent
    {
        public virtual IGameSystem System { get; set; }

        public void RegisterToGame()
        {
            IoC.Resolve<Game>().RegisterComponent(this);
        }

        protected void Start()
        {
            RegisterToGame();
        }
    }
}
