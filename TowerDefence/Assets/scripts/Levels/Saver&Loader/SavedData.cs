using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]

public class SavedGame
{
    public List<MonsterInfo> monsters = new List<MonsterInfo>();
    public List<TowerInfo> towers = new List<TowerInfo>();

    public bool isSceneDefault = false;

    public int coins = 0;

    public float elapsedTime = 0;

    public int mobsKilled = 0;

    public SavedGame()
    {

    }
}

[Serializable]

public class SavedMap
{
    public List<TowerInfo> towers = new List<TowerInfo>();

    public SavedMap()
    {

    }
}