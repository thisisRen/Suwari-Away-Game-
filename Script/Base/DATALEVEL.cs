using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DATA LEVEL", menuName = "NgaPham/DATA")]

public class DATALEVEL : ScriptableObject
{
    public List<Level> listLevel;

    public void SetData()
    {
        for (int i = 0; i < listLevel.Count; i++)
        {
            PlayerPrefs.SetFloat("LevelTime" + i.ToString(), listLevel[i].second);
            PlayerPrefs.SetInt("LevelGreenEgg" + i.ToString(), listLevel[i].greenEgg);
            PlayerPrefs.SetInt("LevelGoldEgg" + i.ToString(), listLevel[i].goldEgg);
        }

    }
}

[System.Serializable]
public class Level
{
    public int ID;
    public GameObject gamePlay;
    public int second;
    public int greenEgg;
    public int goldEgg;
}

//int MaxLevel 
//float LevelTime
//int LevelGreenEgg
//int LevelGoldEgg
//int LevelPass (0/1)

//float LevelTimeUse
//int LevelGreenEggDone
//int LevelGoldEggDone

