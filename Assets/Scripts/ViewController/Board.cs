using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Board:MonoBehaviour,IController
    {
        private List<Text> mTexts=new List<Text>();
        private List<Transform> mChildren = new List<Transform>();
        private Action<int, int> mOnClick;

        private IGameSystem mGameSystem;
        private void Awake()
        {
            mGameSystem = this.GetSystem<IGameSystem>();
            //按顺序存储及初始化
            for (int i = 0; i < transform.childCount; i++)
            {
                int x = i / 3;
                int y = i % 3;
                mChildren.Add(transform.GetChild(i));
                mTexts.Add(mChildren[i].GetComponentInChildren<Text>());
                mChildren[i].GetComponent<Button>().onClick.AddListener(() =>
                {
                    mOnClick(x, y);
                });
            }
            mGameSystem.InitGame(true);
            Refresh();
            mOnClick = (x, y) =>
            {
                Debug.Log($"位于{x},{y}处的棋子被点击了");
                mGameSystem.PlayerMove(x,y);
                mGameSystem.AIMove();
                Refresh();
            };
        }

        void Test()
        {
            mGameSystem.Board.Row(1, (x, y, chess) =>
            {
                Debug.Log("Test");
                chess.C = 'T';
                Refresh();
            });
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Test();
            }
        }

        //棋盘刷新方法
        private void Refresh()
        {
            mGameSystem.Board.ForEach((x, y, chess) =>
            {
                int index = x * 3 + y;
                mTexts[index].text = chess.C.ToString();
            });
        }

        public IArchitecture GetArchitecture()
        {
            return TicTacToe.Interface;
        }
    }
}