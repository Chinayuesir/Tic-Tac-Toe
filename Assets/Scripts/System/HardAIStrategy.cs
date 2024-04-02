using System;

namespace Game
{
    /// <summary>
    /// 没有实现最难的AI，可以基于极大极小值算法或者其它方法做一个更强的AI
    /// </summary>
    public class HardAIStrategy:IChessAIStrategy
    {
        public Tuple<int, int> MakeMove(EasyGrid<Chess> chessBoard, char playerChess, char aiChess)
        {
            throw new NotImplementedException();
        }
    }
}