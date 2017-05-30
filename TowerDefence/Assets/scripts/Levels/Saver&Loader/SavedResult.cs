using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]

public class SavedResult {
    public float timePlayed = 0;
    public int mobsKilled = 0;

    public SavedResult(float time, int mobs)
    {
        timePlayed = time;
        mobsKilled = mobs;
    }
}
