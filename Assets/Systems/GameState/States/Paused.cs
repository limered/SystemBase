using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SystemBase.StateMachineBase;
using Systems.GameState.Messages;
using UniRx;

namespace Systems.GameState.States
{
    public class Paused : BaseState<Game>
    {
        private readonly ReadOnlyCollection<Type> _validNextStates = 
            new ReadOnlyCollection<Type>(new List<Type>{typeof(Running)});

        public override ReadOnlyCollection<Type> ValidNextStates
        {
            get { return _validNextStates; }
        }

        public override bool Enter(StateContext<Game> context)
        {
            MessageBroker.Default.Receive<GameMsgUnpause>()
                .Subscribe(unpause => context.GoToState(new Running()))
                .AddTo(this);

            return true;
        }
    }
}
