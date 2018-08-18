using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SystemBase.StateMachineBase;
using Systems.GameState.Messages;
using UniRx;

namespace Systems.GameState.States
{
    public class Running : BaseState<Game>
    {
        private readonly ReadOnlyCollection<Type> _validNextStates = 
            new ReadOnlyCollection<Type>(new List<Type>{typeof(GameOver), typeof(Paused)});

        public override ReadOnlyCollection<Type> ValidNextStates
        {
            get { return _validNextStates; }
        }

        public override bool Enter(StateContext<Game> context)
        {
            MessageBroker.Default.Receive<GameMsgEnd>()
                .Subscribe(end => context.GoToState(new GameOver()))
                .AddTo(this);

            MessageBroker.Default.Receive<GameMsgPause>()
                .Subscribe(pause => context.GoToState(new Paused()))
                .AddTo(this);

            return true;
        }
    }
}
