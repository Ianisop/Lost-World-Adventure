using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_attacker : MonoBehaviour
{
    private Collider2D enemy ;

    void Start()
    {
        enemy = GetComponentInParent<Collider2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    { 
        
        if(other.CompareTag("player"))
        {
            if(enemy.CompareTag("rat"))
                PlayerHealth.Instance.TakeDamage(1);
            else if(enemy.CompareTag("Enemy") || enemy.CompareTag("turret"))
                PlayerHealth.Instance.TakeDamage(3);
            //PlayerHealth.Instance.TakeDamage(1);
        }
    }
}
