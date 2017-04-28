using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]

public class TowerInfo {
    public float posx;
    public float posy;
    public float posz;

    public TowerInfo(float px, float py, float pz)
    {
        posx = px;
        posy = py;
        posz = pz;
    }
}
