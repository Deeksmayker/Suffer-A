using System;
using UnityEngine;
using UnityEngine.Events;


public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode jumpKey;
    
    public static float HorizontalRaw { get; private set; }
    public static float VerticalRaw { get; private set; }
    public static UnityEvent OnJumpKeyDown = new UnityEvent();
    public static UnityEvent OnJumpKeyUp = new UnityEvent();

    private void Update()
    {
        HorizontalRaw = Input.GetAxisRaw("Horizontal");
        VerticalRaw = Input.GetAxisRaw("Vertical");
        
        if (Input.GetKeyDown(jumpKey))
            OnJumpKeyDown?.Invoke();
        if (Input.GetKeyUp(jumpKey))
            OnJumpKeyUp?.Invoke();
    }

    private void FixedUpdate()
    {
        
    }
}
