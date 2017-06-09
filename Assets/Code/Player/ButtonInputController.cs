using System;
using UnityEngine;

[Obsolete("Touch Controller is the main character controller")]
public class ButtonInputController: MonoBehaviour
{
    public PlayerController playerController;

    void Awake()
    {
        if (playerController == null)
        {
            playerController = (PlayerController)FindObjectOfType(typeof(PlayerController));
        }
    }
    
    public void Jump()
    {
        playerController.Jump();
    }
    
    public void JumpDown()
    {
        playerController.JumpDown();
    }
}