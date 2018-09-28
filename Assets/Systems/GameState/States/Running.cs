using SystemBase.StateMachineBase;
using Systems.GameState.Messages;
using UniRx;

namespace Systems.GameState.States
{
    [NextValidStates(typeof(GameOver), typeof(Paused))]
    public class Running : BaseState<Game>
    {
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
