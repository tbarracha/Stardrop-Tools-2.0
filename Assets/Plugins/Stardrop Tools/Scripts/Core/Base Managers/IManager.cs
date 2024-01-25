
namespace StardropTools
{
    public interface IManager
    {
        public void InitializeManager();
        public void LateInitializeManager();
        public void GameStateChanged(GameState newGameState, GameState previousGameState);
    }
}