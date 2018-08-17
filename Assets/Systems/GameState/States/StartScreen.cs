using System;
using System.Collections.ObjectModel;
using SystemBase.StateMachineBase;

namespace Systems.GameState.States
{
    public class StartScreen : BaseState<Game>
    {
        private ReadOnlyCollection<Type> _validNextStates;

        public override ReadOnlyCollection<Type> ValidNextStates
        {
            get { return _validNextStates; }
        }

        public override bool Enter(StateContext<Game> context)
        {
            throw new NotImplementedException();
        }
    }
}
