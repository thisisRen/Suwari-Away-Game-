using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public static LoadLevel Instance;

    public DATALEVEL level;
    public GameObject gamePlay;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        LoadGame();

    }
    private void Update()
    {

    }
    public void LoadGame()
    {
        gamePlay = Instantiate(level.listLevel[PlayerPrefs.GetInt("LevelChoose")].gamePlay);
    }
}
