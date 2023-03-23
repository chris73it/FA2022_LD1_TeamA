using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedcapFairyAnimationEvents : MonoBehaviour
{
    private SpriteRenderer Sprite;
    private RedcapFairyCombat Combat;
    public void Awake()
    {
        Sprite = GetComponent<SpriteRenderer>();
        Combat = transform.parent.gameObject.GetComponent<RedcapFairyCombat>();
    }

    public void DoAttack()
    {
        if (Combat.AttackDelay <= 0 && Combat.IsAttacking)
        {
            Collider[] Damaged = Physics.OverlapBox(Combat.AttackDirection + transform.position, new Vector3(1, 1, 1));
            Combat.OnDamage(Damaged);
        }
        Combat.IsAttacking = false;
        Combat.AttackCooldown = 1f;
        // Combat.SoundSource.PlayOneShot(Combat.SoundClips[0], GameManager.Instance.SoundVolume / 10f);
    }
}
