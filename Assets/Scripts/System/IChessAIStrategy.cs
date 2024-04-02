using System;

namespace Game
{
    public interface IChessAIStrategy
    {
        Tuple<int,int> MakeMove(EasyGrid<Chess> chessBoard,char playerChess,char aiChess);
    }
}