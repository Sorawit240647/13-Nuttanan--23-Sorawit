using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPng : Weapon
{
    Player player;
    [SerializeField] private float speed;
    private void Start()
    {
        Damage = 1;
        speed = 20 * GetShootDirection();
    }
    public override void Move()
    {
        float newX = transform.position.x + speed * Time.fixedDeltaTime;
        float newY = transform.position.y;
        Vector2 newPosition = new Vector2(newX, newY);
        transform.position = newPosition;
    }
    private void FixedUpdate()
    {
        Move();
    }
    public override void OnHitWith(Character character)
    {

        if (character is Enemy)
            Debug.Log(" Bullet Hit!!");
        character.TakeDamage(this.Damage);
        Destroy(this);
    }
}
