using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SystemBase.StateMachineBase;
using Systems.GameState.Messages;
using UniRx;

namespace Systems.GameState.States
{
    public class GameOver : BaseState<Game>
    {
        private readonly ReadOnlyCollection<Type> _validNextStates = 
            new ReadOnlyCollection<Type>(new List<Type>{typeof(StartScreen)});

        public override ReadOnlyCollection<Type> ValidNextStates
        {
            get { return _validNextStates; }
        }

        public override bool Enter(StateContext<Game> context)
        {
            MessageBroker.Default.Receive<GameMsgRestart>()
                .Subscribe(restart => context.GoToState(new StartScreen()))
                .AddTo(this);

            return true;
        }
    }
}
