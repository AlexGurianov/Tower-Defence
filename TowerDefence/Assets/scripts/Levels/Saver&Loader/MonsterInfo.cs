using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]

public class MonsterInfo {
    public float posx;
    public float posy;
    public float posz;
    public float maxEnergy;
    public float energy;

    public MonsterInfo(float px, float py, float pz, float maxe, float e)
    {
        posx = px;
        posy = py;
        posz = pz;
        maxEnergy = maxe;
        energy = e;
    }
}
