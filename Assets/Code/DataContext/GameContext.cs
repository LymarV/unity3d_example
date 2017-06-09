using System;
using UnityEngine;

public class GameContext : PropertyChangable
{
    private int _scores;
    public int Scores
    {
        get { return _scores; }
        set { _scores = value; RaisePropertyChanged("Scores"); }
    }
    
    private bool _isPaused;
    public bool IsPaused
    {
        get { return _isPaused; }
        set { _isPaused = value; RaisePropertyChanged("IsPaused"); }
    }

    private int _fishCount;
    public int FishCount 
    {
        get { return _fishCount; }
        set { _fishCount = value; RaisePropertyChanged("FishCount"); }
    }

    private float _powerbar;
    public float Powerbar
    {
        get { return _powerbar; }
        set { _powerbar = Mathf.Clamp01(value); RaisePropertyChanged("Powerbar"); }
    }
    
    public event Action ResurectRequested = delegate {};
    public event Action<bool> RestartRequested = delegate {};
    public event Action OnDie = delegate {};
    
    public void Pause()
    {
        IsPaused = true;
    }
    
    public void Resume()
    {
        IsPaused = false;
    }
    
    public void Resurrect()
    {
        ResurectRequested();
        
        Resume();
    }
    
    public void Restart()
    {
        RestartRequested(/*run=*/true);
        
        Resume();
    }

    public void Exit()
    {
        RestartRequested(/*run=*/false);
        
        Resume();
    }
    
    public void Die()
    {
        OnDie();
    }
}