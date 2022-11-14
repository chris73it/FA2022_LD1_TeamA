using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    public CharacterController Controller;
    public float MaxStamina = 1f;
    public float CurrentStamina;
    public float StaminaRegenRate = 0.05f;
    public float StaminaRegenTimer = 1f;
    public float StaminaTimer = 0f;

    private void Awake()
    {
        CurrentStamina = MaxStamina;
    }

    void Update()
    {
        StaminaTimer += Time.deltaTime;

        if (StaminaTimer >= StaminaRegenTimer)
        {
            RestoreStamina();
            StaminaTimer = 0;
        }

        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        UpdateSpeed();
        Move();

        //Animation
        if (Horizontal > 0)
        {
            GetComponent<WerewolfAnimation>().AnimationState = WerewolfAnimation.AnimationStates.WalkRight;
        }
        else if (Horizontal < 0)
        {
            GetComponent<WerewolfAnimation>().AnimationState = WerewolfAnimation.AnimationStates.WalkLeft;
        } else if (Horizontal == 0 && Vertical == 0)
        {
            GetComponent<WerewolfAnimation>().AnimationState = WerewolfAnimation.AnimationStates.Idle;
        }


    }

    public override void Move() {
        Vector3 direction = transform.right * Horizontal + transform.forward * Vertical;
        Controller.Move(direction * CurrentSpeed * Time.deltaTime);
    }

    public void RestoreStamina()
    {
        if (CurrentStamina + StaminaRegenRate > MaxStamina)
        {
            CurrentStamina = MaxStamina;
        } else
        {
            CurrentStamina += StaminaRegenRate;
        }

        PlayerUIControl.Instance.UpdateStamina(CurrentStamina, MaxStamina);
    }

    public void RestoreStamina(float amount)
    {
        if (CurrentStamina + amount > MaxStamina)
        {
            CurrentStamina = MaxStamina;
        }
        else
        {
            CurrentStamina += amount;
        }

        PlayerUIControl.Instance.UpdateStamina(CurrentStamina, MaxStamina);
    }

    public void UseStamina(float amount)
    {
        if (CurrentStamina - amount < 0)
        {
            CurrentStamina = 0;
        } 
        else
        {
            CurrentStamina -= amount;
        }

        PlayerUIControl.Instance.UpdateStamina(CurrentStamina, MaxStamina);
    }
}
