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
        protected override void Init()
        {
            RegisterModel<IGameModel>(new GameModel());
            
            RegisterSystem<IGameSystem>(new GameSystem());
        }
    }
}

