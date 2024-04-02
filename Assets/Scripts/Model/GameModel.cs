using QFramework;

namespace Game
{
    public interface IGameModel:IModel
    {
        bool IsPlayerTurn { get; set; }
        bool IsPlayerFirstMove { get; set; }
        int Turn { get; set; }
        bool IsGameOver { get; set; }
        int CurDifficulty { get; set; }
    }

    public class GameModel : AbstractModel,IGameModel
    {
        public bool IsPlayerTurn { get; set; }
        public bool IsPlayerFirstMove { get; set; }
        public int Turn { get; set; }
        public bool IsGameOver { get; set; } = false;
        public int CurDifficulty { get; set; }
        protected override void OnInit()
        {
           
        }
    }
}