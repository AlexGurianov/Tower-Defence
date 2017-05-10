using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]

public class Profile
{
    public string userName;
    public Settings settings = new Settings();

    public Dictionary<string, SavedGame> savedGamesDictionary = new Dictionary<string, SavedGame>();
    public Dictionary<string, SavedMap> savedMapsDictionary = new Dictionary<string, SavedMap>();

    public Profile()
    {

    }
}
