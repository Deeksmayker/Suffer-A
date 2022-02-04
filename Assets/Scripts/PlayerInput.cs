using System;
using UnityEngine;
using UnityEngine.Events;


public class PlayerInput : MonoBehaviour
{
    public static float HorizontalRaw { get; private set; }
    public static float VerticalRaw { get; private set; }
    public static UnityEvent OnJump = new UnityEvent();

    private void Update()
    {
        HorizontalRaw = Input.GetAxisRaw("Horizontal");
        VerticalRaw = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        
    }
}
