using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Game
{
    public class EasyAIStrategy:IChessAIStrategy
    {
        public Tuple<int,int> MakeMove(EasyGrid<Chess> chessBoard,char playerChess,char aiChess)
        {
            List<Chess> availableMoves = new List<Chess>();

            // 遍历棋盘寻找所有空位
            for (int x = 0; x < chessBoard.Width; x++)
            {
                for (int y = 0; y < chessBoard.Height; y++)
                {
                    if (chessBoard[x, y].C == ' ')
                    {
                        availableMoves.Add(new Chess(x, y, ' '));
                    }
                }
            }
            if (availableMoves.Count == 0)
            {
                return null;
            }
            int moveIndex = Random.Range(0, availableMoves.Count);
            Chess move = availableMoves[moveIndex];
            return new Tuple<int, int>(move.X,move.Y);
        }
    }
}