using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public GameObject Owner;
    public float Lifetime; // maybe should based on distance the bullet went instead
    public Vector3 Direction;
    public bool Active = false;
    public float Speed;

    private void Update()
    {
        if (Active)
        {
            if (Lifetime > 0)
            {
                Lifetime -= Time.deltaTime;
            }
            else
            {
                Destroy(this.gameObject);
            }
            gameObject.GetComponent<Rigidbody>().MovePosition(gameObject.transform.position + (Direction * Time.deltaTime * Speed));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Active)
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<Health>().TakeDamage(Owner.GetComponent<Combat>().Damage);
                Destroy(this.gameObject);
            }

            
        }
    }

    public void GetDirection(Vector3 position)
    {
        Vector3 heading = position - gameObject.transform.position;
        Direction = heading / (heading.magnitude);
    }
}
