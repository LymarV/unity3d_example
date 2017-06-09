public class InputController: TouchHandler
{
    public PlayerController playerController;
    
    void Start()
    {
        if (playerController == null)
        {
            playerController = (PlayerController)FindObjectOfType(typeof(PlayerController));
        }
    }
    
    protected override void OnTap()
    {
        playerController.Jump();
    }

    protected override void OnPointerUp()
    {
        playerController.FinishJump();
    }
    
    protected override void OnSwipeDown()
    {
        playerController.JumpDown();
    }

    protected override void OnPointerDown()
    {
        playerController.PrepareForJump();
    }
}