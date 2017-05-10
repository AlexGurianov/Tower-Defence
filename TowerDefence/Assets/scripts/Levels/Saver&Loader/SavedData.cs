using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]

public class SavedGame
{
    public List<MonsterInfo> monsters = new List<MonsterInfo>();
    public List<TowerInfo> towers = new List<TowerInfo>();

    public bool isSceneDefault = false;

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