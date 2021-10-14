using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuwariAwayGame 
{
    public delegate void UIGameManagerEvent();
    public static UIGameManagerEvent ShowUI;
    public static UIGameManagerEvent Music;

    public delegate void GamePlayManagerEvent();
    public static GamePlayManagerEvent GamePlay;

}
