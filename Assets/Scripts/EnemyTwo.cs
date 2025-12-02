using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTwo : Enemy, IShootable
{
    [SerializeField] private float attackRange;
    public Player player;

    [field: SerializeField] public GameObject Bullet { get; set; }
    [field: SerializeField] public Transform BulletSpawnPoint { get; set; }
    [field: SerializeField] public float BulletSpawnTime { get; set; }
    [field: SerializeField] public float BulletTimer { get; set; }
    void Start()
    {
        Init(1);
        attackRange = 12;
        BulletSpawnTime = 2f;
        BulletTimer = 2.0f;
    }

    void Update()
    {
        BulletTimer -= Time.deltaTime;

        Behavior();
    }
    public void Shoot()
    {
        GameObject obj = Instantiate(Bullet, BulletSpawnPoint.position, BulletSpawnPoint.rotation);
        BulletPng bulletPng = obj.GetComponent<BulletPng>();
        bulletPng.Init(1, this);
        BulletTimer = BulletSpawnTime;
    }
    public override void Behavior()
    {
        Vector3 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;

        if (distance < attackRange && BulletTimer <= 0)
        {
            Shoot();

        }
    }
}
