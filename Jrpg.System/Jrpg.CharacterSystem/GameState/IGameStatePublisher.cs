﻿using System;
namespace Jrpg.CharacterSystem.GameState
{
    public interface IGameStatePublisher
    {
        void Register(IGameStateSubscriber subscriber);
        void Unregister(IGameStateSubscriber subscriber);
        void PublishStateUpdate(GameStateValue state);
        GameStateValue CurrentState();
    }
}
