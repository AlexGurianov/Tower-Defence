using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]

public class SavedData
{
    public List<MonsterInfo> monsters = new List<MonsterInfo>();
    public List<TowerInfo> towers = new List<TowerInfo>();

    public SavedData()
    {

    }
}
