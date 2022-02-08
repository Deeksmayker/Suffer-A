using System;
using System.Collections;
using Movement;
using UnityEngine;
using UnityEngine.Events;


public class PlayerInput : MonoBehaviour
{
    private PlayerController _player;
    
    public const KeyCode JumpKey = KeyCode.Z;
    public const KeyCode LungeKey = KeyCode.C;
    
    public static float HorizontalRaw { get; private set; }
    public static float VerticalRaw { get; private set; }
    
    public static UnityEvent OnJumpKeyDown = new UnityEvent();
    public static UnityEvent OnJumpKeyUp = new UnityEvent();
    public static UnityEvent OnLungeKeyDown = new UnityEvent();

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
    }    

    private void Update()
    {
        HorizontalRaw = Input.GetAxisRaw("Horizontal");
        VerticalRaw = Input.GetAxisRaw("Vertical");

        CheckJumpInputs();
        CheckLungeInputs();
    }   

    private void CheckJumpInputs()
    {
        if (Input.GetKeyDown(JumpKey))
        {
            //OnJumpKeyDown.Invoke();
            StartCoroutine(CheckCoyote());
        }
           
        if (Input.GetKeyUp(JumpKey))
            OnJumpKeyUp.Invoke();
    }

    private static void CheckLungeInputs()
    {
        if (Input.GetKeyDown(LungeKey))
            OnLungeKeyDown.Invoke();
    }

    private IEnumerator CheckCoyote()
    {
        var duration = 0f;
        while (duration < 0.17f)
        {
            if (_player.IsGrounded)
            {
                OnJumpKeyDown.Invoke();
                yield break;
            }
            duration += Time.deltaTime;
            yield return null;
        }
    }
}
