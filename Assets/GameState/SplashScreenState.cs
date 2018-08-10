using System;
using GameState.Messages;
using UniRx;

namespace GameState
{
    public class SplashScreenState : IGameState
    {
        private IDisposable _waitForStartDisposable;
        private GameControllerSystem _context;

        public void Enter(GameControllerSystem context)
        {
            _context = context;
            MessageBroker.Default.Publish(new MessageShowSplashScreen());

            _waitForStartDisposable = MessageBroker.Default.Receive<MessageStartGame>()
                .Subscribe(StartGameRecieved);
        }

        private void StartGameRecieved(MessageStartGame messageStartGame)
        {
            _context.NextState();
        }

        public void Exit()
        {
            _waitForStartDisposable.Dispose();
        }
    }
}