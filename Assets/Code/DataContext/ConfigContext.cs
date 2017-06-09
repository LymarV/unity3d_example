using UnityEngine;

public class ConfigContext : PropertyChangable
{
    public float RunSpeed
    {
        get { return RootConfig.PlayerConfig.runSpeed; }
        set 
        { 
            RootConfig.PlayerConfig.runSpeed = value; 
            RaisePropertyChanged("RunSpeed"); 
        }
    }

    public float JumpHeight
    {
        get { return RootConfig.PlayerConfig.jumpHeight; }
        set 
        { 
            RootConfig.PlayerConfig.jumpHeight = value; 
            RaisePropertyChanged("JumpHeight"); 
        }
    }

    public float Gravity
    {
        get { return -Physics2D.gravity.y; }
        set 
        { 
            var g = Physics2D.gravity;
            g.y = -value;
            Physics2D.gravity = g;
            RaisePropertyChanged("Gravity"); 
        }
    }

    public float PlayerCollectionRadius
    {
        get { return RootConfig.PlayerConfig.collectionRadius; }
        set 
        { 
            RootConfig.PlayerConfig.collectionRadius = value; 
            RaisePropertyChanged("PlayerCollectionRadius"); 
        }
    }

    public float TapDetectionTime
    {
        get { return RootConfig.InputConfig.TapDetectionTime; }
        set 
        { 
            RootConfig.InputConfig.TapDetectionTime = value; 
            RaisePropertyChanged("TapDetectionTime"); 
        }
    }

    private RootConfig _rootConfig;
    public RootConfig RootConfig
    {
        get { return _rootConfig ?? (_rootConfig = Locator.Find<RootConfig>()); }
    }
}