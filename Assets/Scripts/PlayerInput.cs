using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class PlayerInput : MonoBehaviour
{
    public const KeyCode JumpKey = KeyCode.Z;
    public const KeyCode LungeKey = KeyCode.C;
    
    public static float HorizontalRaw { get; private set; }
    public static float VerticalRaw { get; private set; }
    
    public static UnityEvent OnJumpKeyDown = new UnityEvent();
    public static UnityEvent OnJumpKeyUp = new UnityEvent();
    public static UnityEvent OnLungeKeyDown = new UnityEvent();
    
    

    private void Update()
    {
        HorizontalRaw = Input.GetAxisRaw("Horizontal");
        VerticalRaw = Input.GetAxisRaw("Vertical");

        CheckJumpInputs();
        CheckLungeInputs();
    }   

    private static void CheckJumpInputs()
    {
        if (Input.GetKeyDown(JumpKey))
        {
            OnJumpKeyDown.Invoke();
        }
           
        if (Input.GetKeyUp(JumpKey))
            OnJumpKeyUp.Invoke();
    }

    private static void CheckLungeInputs()
    {
        if (Input.GetKeyDown(LungeKey))
            OnLungeKeyDown.Invoke();
    }
}
