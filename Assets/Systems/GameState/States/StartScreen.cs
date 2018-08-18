using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SystemBase.StateMachineBase;
using Systems.GameState.Messages;
using UniRx;

namespace Systems.GameState.States
{
    public class StartScreen : BaseState<Game>
    {
        private readonly ReadOnlyCollection<Type> _validNextStates =
            new ReadOnlyCollection<Type>(new List<Type> { typeof(Running) });

        public override ReadOnlyCollection<Type> ValidNextStates
        {
            get { return _validNextStates; }
        }

        public override bool Enter(StateContext<Game> context)
        {
            MessageBroker.Default.Receive<GameMsgStart>()
                .Subscribe(start => context.GoToState(new Running()))
                .AddTo(this);
            return true;
        }
    }
}