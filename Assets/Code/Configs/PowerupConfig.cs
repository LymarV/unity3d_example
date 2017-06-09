using System;
using UnityEngine;

[Serializable]
public class PowerupConfig
{
    public string name;

    [Tooltip("Duration in seconds")]
    public float duration = 5f;

    public float heightAddition = 0f;
    public float speedAddition = 0f;
}