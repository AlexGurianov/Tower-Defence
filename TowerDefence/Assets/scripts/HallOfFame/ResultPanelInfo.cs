using System.Collections;
using System.Collections.Generic;

public class ResultPanelInfo {

    public string profileName;
    public int mobsKilled;
    public float timeAlive;

    public ResultPanelInfo(string pN, int mK, float tA)
    {
        profileName = pN;
        mobsKilled = mK;
        timeAlive = tA;
    }
}
