
namespace StardropTools.GameStateManagement
{
    public interface IGameStateMachine
    {
        bool ChangeGameState(GameState state);
        bool ChangeGameState(string stateName);
        bool ChangeGameState(int stateId);
    }
}
