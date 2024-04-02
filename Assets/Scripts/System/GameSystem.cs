using QFramework;
using UnityEngine;

namespace Game
{
    
    public interface IGameSystem:ISystem
    {
        /// <summary>
        /// 初始化游戏
        /// </summary>
        /// <param name="isPlayerFirstMove"></param>
        void InitGame(bool isPlayerFirstMove);
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
        
        public void InitGame(bool isPlayerFirstMove)
        {
            mGameModel.IsPlayerFirstMove = isPlayerFirstMove;
            mGameModel.Turn = 1;
            mGameModel.IsPlayerTurn = isPlayerFirstMove;
            Board = new EasyGrid<Chess>(3, 3);
            Board.Fill((x, y) =>
            {
                return new Chess(x, y, ' ');
            });

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