
using StardropTools.GameStateManagement;

namespace StardropTools
{
    public interface IGameManager : IManager
    {
        public void GameStateChanged(GameState newGameState, GameState previousGameState);
    }
}