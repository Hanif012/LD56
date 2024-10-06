using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemySO enemyData;

    private int health;
    private float movementSpeed;
    private int damage;

    void Start()
    {
        // Fetch Stats from the EnemySO
        health = enemyData.stats.health;
        movementSpeed = enemyData.stats.speed;
        damage = enemyData.stats.damage;
    }
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 target = gameObject.GetComponent<Player>().transform.position;
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * movementSpeed * Time.deltaTime;
    }

    public void ChangeHealth(int amount)
    {
        health += amount;

        if (health <= 0)
        {
            KillEnemy();
        }
    }

    public void KillEnemy()
    {
        // Trigger the event when the enemy is destroyed;

        Destroy(gameObject);
    }
}