using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace Game
{
    //框架管理位置
    public class TicTacToe : Architecture<TicTacToe>
    {
        public static string MaxDifficulty = "MaxDifficulty";
        public static EasyEvent<bool> GameOverEvent=new EasyEvent<bool>();
        public static EasyEvent GameDrawEvent = new EasyEvent();
        protected override void Init()
        {
            RegisterModel<IGameModel>(new GameModel());
            
            RegisterSystem<IGameSystem>(new GameSystem());
        }
    }
}

