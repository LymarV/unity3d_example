using System;
using System.Collections.Generic;

[Serializable]
public class RootConfig
{
    public SessionConfig SessionConfig;
    public PlayerConfig PlayerConfig;
    public InputConfig InputConfig;
    public List<PowerupConfig> PowerupsConfig = new List<PowerupConfig> () 
    {
        new PowerupConfig() 
        {
            name = "Jump High",
            heightAddition = 3f,
            speedAddition = -2f,
        },
    };
}