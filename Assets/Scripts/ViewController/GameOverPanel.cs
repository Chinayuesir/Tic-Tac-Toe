using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class GameOverPanel : MonoBehaviour,IController
    {
        private Text mWinOrLoseText;
        private Text mTipText;
        
        private void Awake()
        {
            mWinOrLoseText = transform.Find("WinOrLoseText").GetComponent<Text>();
            mTipText = transform.Find("Tip").GetComponent<Text>();
            mWinOrLoseText.gameObject.SetActive(false);
            mTipText.gameObject.SetActive(false);
        }

        private void Start()
        {
            TicTacToe.GameOverEvent.Register((isPlayerWin) =>
            {
                GetComponent<Image>().enabled = true;
                if (isPlayerWin) mWinOrLoseText.text = "你赢了";
                else mWinOrLoseText.text = "你输了";
                mWinOrLoseText.gameObject.SetActive(true);
                mTipText.gameObject.SetActive(true);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            if (this.GetModel<IGameModel>().IsGameOver)
            {
                if (Input.anyKeyDown)
                {
                    SceneManager.LoadScene("Start");
                }
            }
        }

        public IArchitecture GetArchitecture()
        {
            return TicTacToe.Interface;
        }
    }
}
