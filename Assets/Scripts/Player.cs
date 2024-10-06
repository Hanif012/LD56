using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool shootRandom = false;

    // Stats
    [SerializeField] private int health = 100;
    [SerializeField] int maxHealth = 100;
    [SerializeField] float movementSpeed = 4f;

    // Experience & level
    long xp = 0;
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
    }

    public void LevelUp()
    {
        xpFromLastLevel = xpToNextLevel;
        // xpToNextLevel = Utils.GetExperienceRequired(level) - xpToNextLevel;
        level++;

        // Todo: Update UI
        // UIExperienceBar.instance.SetLevelText(level);

    }

    public void ChangeHealth(int amount)
    {
        health += amount;

        if (health <= 0)
        {
            KillPlayer();
        }
    }
    
    void KillPlayer()
    {

        Destroy(gameObject);
    }
}
