using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfAnimation : MonoBehaviour
{
    public enum AnimationStates
    {
        Idle,
        WalkLeft,
        WalkRight,
        WalkUp,
        WalkDown,
    }

    private AnimationStates _animationState;
    public AnimationStates AnimationState
    {
        get { return _animationState; }
        set 
        { 
            _animationState = value;

            switch (_animationState)
            {
                case (AnimationStates.Idle):
                    GameManager.ChosenPlayerCharacter.GetComponent<WerewolfCombat>().Animator.SetBool("Moving", false);

                    break;

                case (AnimationStates.WalkLeft):
                    GameManager.ChosenPlayerCharacter.GetComponent<WerewolfCombat>().Animator.SetBool("Moving", true);
                    GameManager.ChosenPlayerCharacter.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
                    break;

                case (AnimationStates.WalkRight):
                    GameManager.ChosenPlayerCharacter.GetComponent<WerewolfCombat>().Animator.SetBool("Moving", true);

                    GameManager.ChosenPlayerCharacter.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
                    break;

                case (AnimationStates.WalkUp):
                    GameManager.ChosenPlayerCharacter.GetComponent<WerewolfCombat>().Animator.SetBool("Moving", true);

                    break;

                case (AnimationStates.WalkDown):
                    GameManager.ChosenPlayerCharacter.GetComponent<WerewolfCombat>().Animator.SetBool("Moving", true);

                    break;

                default:
                    GameManager.ChosenPlayerCharacter.GetComponent<WerewolfCombat>().Animator.SetBool("Moving", false);

                    break;
            }
        }
    }

    void Awake()
    {
        AnimationState = AnimationStates.Idle;
    }


}
