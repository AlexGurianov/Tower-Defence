using System;
using System.Collections;
using System.Collections.Generic;



[Serializable]

public class TowerInfo {
    public TowerType towerType;

    public float posx;
    public float posy;
    public float posz;

    public bool preset;

    public TowerInfo(float px, float py, float pz, TowerType type, bool prset)
    {
        towerType = type;
        posx = px;
        posy = py;
        posz = pz;
        preset = prset;
    }
}
