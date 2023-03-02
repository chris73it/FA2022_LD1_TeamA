using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapParent : MonoBehaviour
{
    public int Damage;

    public Animator Animator;

    public virtual void Initialize()
    {

    }

    private void Awake()
    {
        Initialize();
    }
}
