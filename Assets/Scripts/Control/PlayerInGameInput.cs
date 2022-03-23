﻿using System;
using System.Collections;
using DefaultNamespace;
using Movement;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerController))]
public class PlayerInGameInput : MonoBehaviour
{
    private PlayerController _player;
    
    public static KeyCode JumpKey = KeyCode.Z;
    public static KeyCode AttackKey = KeyCode.X;
    public static KeyCode LungeKey = KeyCode.C;
    public static KeyCode AbilityKey = KeyCode.F;


    public static float HorizontalRaw { get; private set; }
    public static float VerticalRaw { get; private set; }
    
    public static UnityEvent OnJumpKeyDown = new UnityEvent();
    public static UnityEvent OnJumpKeyUp = new UnityEvent();
    public static UnityEvent OnLungeKeyDown = new UnityEvent();
    public static UnityEvent OnAttackKeyDown = new UnityEvent();
    public static UnityEvent OnAttackKeyUp = new UnityEvent();

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
    }    

    private void Update()
    {
        SetAxisInput();
        CheckJumpInputs();
        CheckLungeInputs();
        CheckAttackInput();
    }

    private void SetAxisInput()
    {
        HorizontalRaw = Input.GetAxisRaw("Horizontal");
        VerticalRaw = Input.GetAxisRaw("Vertical");
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

    private void CheckAttackInput()
    {
        if (Input.GetKeyDown(AttackKey))
        {
            OnAttackKeyDown.Invoke();
        }

        if (Input.GetKeyUp(AttackKey))
        {
            OnAttackKeyUp.Invoke();
        }
    }

    private IEnumerator CheckCoyote()
    {
        var duration = 0f;
        while (duration < 0.17f)
        {
            if (PlayerPreferences.IsGrounded)
            {
                OnJumpKeyDown.Invoke();
                yield break;
            }
            duration += Time.deltaTime;
            yield return null;
        }
    }
}