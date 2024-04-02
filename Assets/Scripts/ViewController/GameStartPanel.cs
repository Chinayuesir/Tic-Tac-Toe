using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class GameStartPanel : MonoBehaviour,IController
    {
        private Button mEasyBtn;
        private Button mNormalBtn;
        private Button mHardBtn;
        private Toggle mPlayerFirst;
        
        public Sprite mButtonBg;

        private int mDif;

        private void Awake()
        {
            mEasyBtn = transform.Find("EasyBtn").GetComponent<Button>();
            mNormalBtn = transform.Find("NormalBtn").GetComponent<Button>();
            mNormalBtn.interactable = false;
            mHardBtn = transform.Find("HardBtn").GetComponent<Button>();
            mHardBtn.interactable = false;
            mEasyBtn.onClick.AddListener(()=>StartGame(1));
            mNormalBtn.onClick.AddListener(()=>StartGame(2));
            mHardBtn.onClick.AddListener(()=>StartGame(3));

            mPlayerFirst = transform.Find("IsPlayerFirst").GetComponent<Toggle>();
            
            if (PlayerPrefs.HasKey(TicTacToe.Difficulty))
            {
                mDif=PlayerPrefs.GetInt(TicTacToe.Difficulty);
            }
            else
            {
                mDif=1;
                PlayerPrefs.SetInt(TicTacToe.Difficulty,mDif);
            }
            RefreshUI();
        }

        private void StartGame(int difficulty)
        {
            SceneManager.LoadScene("Game");
            this.GetModel<IGameModel>().IsGameOver = false;
            this.GetModel<IGameModel>().IsPlayerFirstMove = mPlayerFirst.isOn;
        }

        private void RefreshUI()
        {
            if (mDif >= 3)
            {
                mHardBtn.GetComponent<Image>().sprite = mButtonBg;
                mHardBtn.interactable = true;
                mHardBtn.transform.GetChild(0).gameObject.SetActive(true);
            }

            if (mDif >= 2)
            {
                mNormalBtn.GetComponent<Image>().sprite = mButtonBg;
                mNormalBtn.interactable = true;
                mNormalBtn.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        public IArchitecture GetArchitecture()
        {
            return TicTacToe.Interface;
        }
    }
}
