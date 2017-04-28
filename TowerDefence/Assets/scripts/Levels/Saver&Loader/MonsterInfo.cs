using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]

public class MonsterInfo {
    public float posx;
    public float posy;
    public float posz;
    public float energy;

    public MonsterInfo(float px, float py, float pz, float e)
    {
        posx = px;
        posy = py;
        posz = pz;
        energy = e;
    }
}
