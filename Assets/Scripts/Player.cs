using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private bool shootAuto = false;
    // Stats
    [SerializeField] public int Health = 100;
    [SerializeField] public int MaxHealth = 100;
    [SerializeField] float movementSpeed = 4f;
    [SerializeField] float attackRange = 5f;
    [SerializeField] int attackDamage = 10;
    [SerializeField] float attackDelay = 2f; // Delay between attacks in seconds
    [SerializeField] private GameObject skin;

    // Experience & level
    long xpFromLastLevel = 0;
    long xpToNextLevel = 0;
    int level = 0;

    bool noNearbyEnemies = false; // shoot randomly
    public bool NoNearbyEnemies
    {
        get { return noNearbyEnemies; }
        set { noNearbyEnemies = value; }
    }

    public object UIExperienceBar { get; private set; }

    void Start()
    {
        LevelUp();
        StartCoroutine(AutoAttack()); // Start the auto-attack coroutine
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movementVector = Vector3.zero;

        // WASD to move around
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) movementVector += new Vector3(0, 0, 1);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) movementVector += new Vector3(-1, 0, 0);
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) movementVector += new Vector3(0, 0, -1);
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) movementVector += new Vector3(1, 0, 0);
        transform.position += movementVector.normalized * Time.deltaTime * movementSpeed;
        
        //Flip the player sprite180 degrees when moving left
        if (movementVector.x < 0)
        {
            skin.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (movementVector.x > 0)
        {
            skin.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void LevelUp()
    {
        xpFromLastLevel = xpToNextLevel;
        level++;

        // Todo: Update UI
        // UIExperienceBar.instance.SetLevelText(level);
    }

    public void ChangeHealth(int amount)
    {
        Health += amount;
        transform.DOShakeScale(1f, new Vector3(1.1f, 1.1f, 1.1f), 10, 10, false);
        if (Health <= 0)
        {
            KillPlayer();
        }
    }

    void KillPlayer()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.GameOver();
    }

    private IEnumerator AutoAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackDelay);
            Attack();
        }
    }

    private void Attack()
    {
        // Find all enemies within range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (var hitCollider in hitColliders)
        {
            Enemy enemy = hitCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ChangeHealth(-attackDamage);
                Debug.Log("Attacked enemy: " + enemy.name);
                return; // Attack only one enemy at a time
            }
        }
    }
}