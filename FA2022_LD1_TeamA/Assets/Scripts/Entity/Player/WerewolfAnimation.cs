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
                    break;

                case (AnimationStates.WalkLeft):
                    GameManager.ChosenPlayerCharacter.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
                    break;

                case (AnimationStates.WalkRight):
                    GameManager.ChosenPlayerCharacter.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
                    break;
               
                default:
                    break;
            }
        }
    }

    void Awake()
    {
        AnimationState = AnimationStates.Idle;
    }


}
