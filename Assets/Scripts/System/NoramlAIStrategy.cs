using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Game
{
    public class NormalAIStrategy : IChessAIStrategy
    {
        public Tuple<int, int> MakeMove(EasyGrid<Chess> chessBoard, char playerChess, char aiChess)
        {
            // 首先检查AI是否有立即获胜的机会
            var winMove = FindWinningMove(chessBoard, aiChess);
            if (winMove != null)
            {
                return winMove;
            }
            // 检查是否需要阻止对手获胜
            var blockMove = FindWinningMove(chessBoard, playerChess);
            if (blockMove != null)
            {
                return blockMove;
            }
            // 随机移动
            return ChooseRandomMove(chessBoard, playerChess, aiChess);
        }

        private Tuple<int, int> FindWinningMove(EasyGrid<Chess> chessBoard, char chess)
        {
            // 检查所有行
            for (int y = 0; y < chessBoard.Height; y++)
            {
                int count = 0;
                int emptyX = -1, emptyY = -1;
                for (int x = 0; x < chessBoard.Width; x++)
                {
                    if (chessBoard[x, y].C == chess) count++;
                    else if (chessBoard[x, y].C == ' ')
                    {
                        emptyX = x;
                        emptyY = y;
                    }
                }

                if (count == 2 && emptyX != -1) return new Tuple<int, int>(emptyX, emptyY);
            }

            // 检查所有列
            for (int x = 0; x < chessBoard.Width; x++)
            {
                int count = 0;
                int emptyX = -1, emptyY = -1;
                for (int y = 0; y < chessBoard.Height; y++)
                {
                    if (chessBoard[x, y].C == chess) count++;
                    else if (chessBoard[x, y].C == ' ')
                    {
                        emptyX = x;
                        emptyY = y;
                    }
                }

                if (count == 2 && emptyX != -1) return new Tuple<int, int>(emptyX, emptyY);
            }

            // 检查对角线（左上到右下）
            int diagCount = 0;
            int emptyDiagX = -1, emptyDiagY = -1;
            for (int i = 0; i < chessBoard.Width; i++)
            {
                if (chessBoard[i, i].C == chess) diagCount++;
                else if (chessBoard[i, i].C == ' ')
                {
                    emptyDiagX = i;
                    emptyDiagY = i;
                }
            }

            if (diagCount == 2 && emptyDiagX != -1) return new Tuple<int, int>(emptyDiagX, emptyDiagY);

            // 检查对角线（右上到左下）
            diagCount = 0;
            for (int i = 0; i < chessBoard.Width; i++)
            {
                if (chessBoard[i, chessBoard.Height - 1 - i].C == chess) diagCount++;
                else if (chessBoard[i, chessBoard.Height - 1 - i].C == ' ')
                {
                    emptyDiagX = i;
                    emptyDiagY = chessBoard.Height - 1 - i;
                }
            }

            if (diagCount == 2 && emptyDiagX != -1) return new Tuple<int, int>(emptyDiagX, emptyDiagY);
            return null;
        }

        private Tuple<int, int> ChooseRandomMove(EasyGrid<Chess> chessBoard, char playerChess, char aiChess)
        {
            // 实现随机选择一个空位的逻辑
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
            return new Tuple<int, int>(move.X, move.Y);
        }
    }
}