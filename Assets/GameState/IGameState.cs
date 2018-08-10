namespace GameState
{
    public interface IGameState
    {
        void Enter(GameControllerSystem context);
        void Exit();
    }
}