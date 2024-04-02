using QFramework;
using UnityEngine;

namespace Game
{
    
    public interface IGameSystem:ISystem
    {
        /// <summary>
        /// 初始化游戏
        /// </summary>
        void InitGame();
        /// <summary>
        /// 棋盘
        /// </summary>
        EasyGrid<Chess> Board { get; set; }
        /// <summary>
        /// 根据棋局判断输赢
        /// </summary>
        void JudgeWinOrLose();
        /// <summary>
        /// 玩家下棋
        /// </summary>
        void PlayerMove(int x,int y);
        /// <summary>
        /// AI下棋
        /// </summary>
        void AIMove();
    }

    public class GameSystem : AbstractSystem, IGameSystem
    {
        private IGameModel mGameModel;
        public EasyGrid<Chess> Board { get; set; }
        //固定先手O，后手X
        private const char mFirstMoveChess = 'O';
        private const char mSecondMoveChess = 'X';

        private char mPlayerChess;
        private char mAIChess;
        
        protected override void OnInit()
        {
            mGameModel = this.GetModel<IGameModel>();
        }
        
        public void InitGame()
        {
            //TODO:根据难度注入AI
            mGameModel.Turn = 1;
            mGameModel.IsPlayerTurn = mGameModel.IsPlayerFirstMove;
            Board = new EasyGrid<Chess>(3, 3);
            Board.Fill((x, y) => new Chess(x, y, ' '));

            if (mGameModel.IsPlayerFirstMove)
            {
                mPlayerChess = 'O';
                mAIChess = 'X';
            }
            else
            {
                mPlayerChess = 'X';
                mAIChess = 'O';
            }
        }
        
        public void JudgeWinOrLose()
        {
            bool playerWin=JudgeWin(mPlayerChess);
            bool aiWin=JudgeWin(mAIChess);
            if (playerWin)
            {
                int curDif = mGameModel.CurDifficulty;
                int historyDif = PlayerPrefs.GetInt(TicTacToe.Difficulty);
                if(curDif==historyDif) 
                    PlayerPrefs.SetInt(TicTacToe.Difficulty,curDif+1);
                TicTacToe.GameOverEvent.Trigger(true);
                mGameModel.IsGameOver = true;
            }

            if (aiWin)
            {
                TicTacToe.GameOverEvent.Trigger(false);
                mGameModel.IsGameOver = true;
            }
        }

        /// <summary>
        /// 判断c字符代表的玩家是否赢了
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool JudgeWin(char c)
        {
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                count = 0;
                Board.Column(i, (_, _, chess) =>
                {
                    if (chess.C == c) count++;
                });
                if (count == 3) return true;
            }
            
            for (int i = 0; i < 3; i++)
            {
                count = 0;
                Board.Row(i, (_, _, chess) =>
                {
                    if (chess.C == c) count++;
                });
                if (count == 3) return true;
            }

            count = 0;
            Board.Diagonal(true, (_, _, chess) =>
            {
                if (chess.C == c) count++;
            });
            if (count == 3) return true;
            
            count = 0;
            Board.Diagonal(false, (_, _, chess) =>
            {
                if (chess.C == c) count++;
            });
            if (count == 3) return true;
            return false;
        }

        public void PlayerMove(int x,int y)
        {
            if (mGameModel.IsPlayerTurn)
            {
                //这个位置已经有棋了
                if (Board[x, y].C != ' ')
                {
                    Debug.Log("已经有棋了，请下在别处！");
                    return;
                }
                Board[x, y].C = mPlayerChess;
                mGameModel.Turn++;
                mGameModel.IsPlayerTurn = false;
            }
            else
            {
                Debug.Log("还未到你的回合！");
            }
        }

        public void AIMove()
        {
            if (!mGameModel.IsPlayerTurn)
            {
                //乱下
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Board[i, j].C == ' ')
                        {
                            Board[i, j].C = mAIChess;
                            mGameModel.Turn++;
                            mGameModel.IsPlayerTurn = true;
                            return;
                        }
                    }
                }
            }
        }
    }
}