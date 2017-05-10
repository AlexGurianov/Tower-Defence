using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]

public class Settings
{
    public float musicLevel = 0.5f;
    public float gameSoundLevel = 0.5f;
    public float navigationSoundLevel = 0.5f;

    public Settings()
    {

    }

    public Settings(float mL, float gSL, float nSL)
    {
        musicLevel = mL;
        gameSoundLevel = gSL;
        navigationSoundLevel = nSL;
    }
}