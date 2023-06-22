using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;

    public float Health
    {
        set 
        { 
            health = value;
            if (health <= 0)
            {
                Death();
            }
        }
        get 
        { 
            return health; 
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public float health = 1;

    public void Death()
    {
        animator.SetTrigger("Death");
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject); 
    }
}
