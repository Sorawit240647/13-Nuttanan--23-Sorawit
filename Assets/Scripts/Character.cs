using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private int health;
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }
    protected Animator anim;
    protected Rigidbody2D rb;

    public virtual void Init(int newHealth)
    {
        Health = newHealth;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public bool IsDead()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        else return false;
    }
    public void TakeDamage(int _damage)
    {
        Health -= _damage;
        IsDead();
    }
}