using UnityEngine;

[CreateAssetMenu]
public class MovementDatabase : ScriptableObject
{
    private float horizontal = 0;
    private float vertical = 1;

    private float moving = 0;

    private float animHorizontal = 0;
    private float animVertical = 0;

    public Vector3 mousePosition = Vector2.zero;

    private float changeDirectionValue = 0.8f;

    private bool disableMove = false;

    public Vector2 GetRawInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        return new Vector2(horizontalInput, verticalInput);
    }

    public Vector2 GetInput()
    {
        return new Vector2(horizontal, vertical);
    }

    public void StopAnim()
    {

    }

    public void SetInput(Vector2 myInput)
    {
        horizontal = myInput.x;
        vertical = myInput.y;
    }

    public void SetHorizontalInput(float myInput)
    {
        horizontal = myInput;
    }

    public void SetVerticalInput(float myInput)
    {
        vertical = myInput;
    }
    
    public void DisableMove()
    {
        disableMove = true;
    }

    public void EnableMove()
    {
        disableMove = false;
    }

    public bool GetDisableEnableMove()
    {
        return disableMove;
    }

    public void SetAnim(Vector2 myInput)
    {
        animHorizontal = myInput.x;
        animVertical = myInput.y;
    }

    public void SetHorizontalAnim(float myInput)
    {
        animHorizontal = myInput;
    }

    public void SetVerticalAnim(float myInput)
    {
        animVertical = myInput;
    }

    public Vector2 GetAnimInput()
    {
        return new Vector2(animHorizontal, animVertical);
    }

    public void SetMoving(float newMoving)
    {
        moving = newMoving;
    }
    public float GetMoving()
    {
        return moving;
    }
}