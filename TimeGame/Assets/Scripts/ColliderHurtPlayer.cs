using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHurtPlayer : MonoBehaviour
{
    [SerializeField] private int damageAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControls>().TakeDamage(damageAmount);
        }
    }
}
