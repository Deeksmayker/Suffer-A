using System;
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
    public static KeyCode HealKey = KeyCode.A;


    public static float HorizontalRaw { get; private set; }
    public static float VerticalRaw { get; private set; }
    public static float Horizontal;
    public static float Vertical;
    
    public static UnityEvent OnJumpKeyDown = new UnityEvent();
    public static UnityEvent OnJumpKeyUp = new UnityEvent();
    public static UnityEvent OnLungeKeyDown = new UnityEvent();
    public static UnityEvent OnAttackKeyDown = new UnityEvent();
    public static UnityEvent OnAttackKeyUp = new UnityEvent();
    public static UnityEvent OnAbility = new UnityEvent();
    public static UnityEvent OnHorizontalAbility = new UnityEvent();
    public static UnityEvent OnUpAbility = new UnityEvent();
    public static UnityEvent OnDownAbility = new UnityEvent();
    public static UnityEvent OnHealDown = new UnityEvent();
    public static UnityEvent OnHealUp = new UnityEvent();

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
    }    

    private void Update()
    {
        CheckAttackInput();
        if (!PlayerPreferences.ControlEnabled)
            return;

        SetAxisInput();
        CheckJumpInputs();
        CheckLungeInputs();
        CheckAbilityInput();
        CheckHealInput();
    }

    private void SetAxisInput()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
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

    private void CheckAbilityInput()
    {
        if (!Input.GetKeyDown(AbilityKey) || PlayerPreferences.CurrentBlood < PlayerPreferences.BloodSpend) 
            return;
        
        if (VerticalRaw == 1)
            OnUpAbility.Invoke();
        else if (VerticalRaw == -1)
            OnDownAbility.Invoke();
        else
            OnHorizontalAbility.Invoke();
    }

    private void CheckHealInput()
    {
        if (PlayerPreferences.CurrentBlood < PlayerPreferences.BloodSpend)
            return;
        if (Input.GetKeyDown(HealKey))
            OnHealDown.Invoke();
        if (Input.GetKeyUp(HealKey))
            OnHealUp.Invoke();
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
