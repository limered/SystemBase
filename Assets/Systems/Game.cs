﻿using GameState.States;
using SystemBase;
using SystemBase.StateMachineBase;
using Systems.GameState.Messages;
using UniRx;
using Utils;

namespace Systems
{
    public class Game : GameBase
    {
        public readonly StateContext<Game> GameStateContext = new StateContext<Game>();

        private void Awake()
        {
            IoC.RegisterSingleton(this);

            GameStateContext.Start(new Loading());

            InstantiateSystems();

            Init();

            MessageBroker.Default.Publish(new GameMsgFinishedLoading());
        }
    }
}