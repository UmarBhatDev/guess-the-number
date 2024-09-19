using System.Collections.Generic;

namespace GTN.Features.GameScreen
{
    public class PlayersInputModel
    {
        public List<PlayerInputData> InputVariables { get; private set; } = new();

        public void AddPlayerInputResult(int playerInputResult, ResultComparedToGuessed resultComparedToGuessed)
        {
            InputVariables.Add(new PlayerInputData {InputVariable = playerInputResult, ResultComparedToGuessed = resultComparedToGuessed});
        }

        public void Clear() => InputVariables.Clear();
    }

    public struct PlayerInputData
    {
        public int InputVariable;
        public ResultComparedToGuessed ResultComparedToGuessed;
    }

    public enum ResultComparedToGuessed
    {
        Equal,
        Less,
        Greater,
    }
}