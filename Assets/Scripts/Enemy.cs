using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemySO enemyData;
    [SerializeField] private GameObject damageObject;
    [SerializeField] private Transform target;
    [SerializeField] private float attackSpeed = 4f;
    [SerializeField] private float separationDistance = 20f;
    [SerializeField] private GameObject skin;
    [SerializeField] private GameObject debugLabel;
    private int health;
    private float movementSpeed;
    private int damage;
    private NavMeshAgent agent;
    private Coroutine animCoroutine;
    private Coroutine attackCoroutine;
    private bool onPlayerEntered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayerEntered = true;
            Debug.Log("Player hit by enemy");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayerEntered = false;
            Debug.Log("Player hit by enemy[exit]");
        }
    }
    private enum EnemyState
    {
        Idle,
        Moving,
        Attacking
    }
    private EnemyState currentState;

    void Start()
    {
        // Fetch Stats from the EnemySO
        health = enemyData.stats.health;
        movementSpeed = enemyData.stats.speed;
        damage = enemyData.stats.damage;

        // Initialize NavMeshAgent with movementSpeed
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found. Ensure there is a GameObject with the 'Player' tag in the scene.");
        }

        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = movementSpeed;
            agent.acceleration = movementSpeed * 2;

            // Adjust avoidance and radius
            agent.radius = separationDistance;
            agent.avoidancePriority = UnityEngine.Random.Range(30, 70);
        }
        else
        {
            Debug.LogError("NavMeshAgent component not found. Ensure this GameObject has a NavMeshAgent attached.");
        }

        if (skin == null)
        {
            Debug.LogError("Skin GameObject not assigned. Please assign the skin in the Inspector.");
        }
        if (debugLabel == null)
        {
            Debug.LogError("DebugLabel GameObject not assigned. Please assign the debugLabel in the Inspector.");
        }
    }

    void FixedUpdate()
    {
        if (agent != null && target != null)
        {
            Move();
            ApplySeparationForce();
        }
        DebugLabel();
    }

    void ApplySeparationForce()
    {
        if (agent == null) return;

        float separationForceStrength = 1.5f;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, separationDistance);

        foreach (var collider in hitColliders)
        {
            if (collider != null && collider.gameObject != this.gameObject && collider.CompareTag("Enemy"))
            {
                Vector3 directionAway = transform.position - collider.transform.position;
                float distance = directionAway.magnitude;
                if (distance > 0)
                {
                    Vector3 force = directionAway.normalized * (separationForceStrength / distance);
                    agent.velocity += force * Time.fixedDeltaTime;
                }
            }
        }
    }

    void Move()
    {
        if (target != null && currentState != EnemyState.Attacking)
        {
            agent.SetDestination(target.position);
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (onPlayerEntered)
            {
                if (currentState != EnemyState.Attacking)
                {
                    ChangeState(EnemyState.Attacking);
                    if (attackCoroutine != null)
                    {
                        StopCoroutine(attackCoroutine);
                    }
                    attackCoroutine = StartCoroutine(Attack());
                }
            }
            else
            {
                if (currentState != EnemyState.Moving)
                {
                    ChangeState(EnemyState.Moving);
                }
            }
        }
    }

    void shoot()
    {
        // Shoot at the player
        
    }
    private void ChangeState(EnemyState newState)
    {
        if (currentState == newState) return;

        currentState = newState;

        if (animCoroutine != null)
        {
            StopCoroutine(animCoroutine);
        }
        animCoroutine = StartCoroutine(Anim());
    }

    public IEnumerator Anim()
    {
        if (skin == null) yield break;

        switch (currentState)
        {
            case EnemyState.Moving:
                // RESET ANIMATION
                skin.transform.DOScale(new Vector3(1, 1f, 1f), 1f);
                skin.transform.DORotate(new Vector3(0, 0, 0), 1f);
                yield return new WaitForSeconds(1f);
                // ANIMATION
                skin.transform.DORotate(new Vector3(-15, 0, 0), 1f);
                skin.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 1f);
                break;
            case EnemyState.Attacking:
                skin.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 1f);
                break;
            case EnemyState.Idle:
                skin.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 1f);
                break;
        }
        yield return null;
    }

    private IEnumerator Attack()
    {
        while (currentState == EnemyState.Attacking)
        {
            Debug.Log("Attacking Player Start");
            yield return new WaitForSeconds(attackSpeed);
            if (damageObject != null)
            {
                damageObject.SetActive(true);
                damageObject.SetActive(false);
            }
            Debug.Log("Attacking Player End");
            ChangeState(EnemyState.Idle);
            yield return new WaitForSeconds(0.5f);
        }
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
        if (animCoroutine != null)
        {
            StopCoroutine(animCoroutine);
        }
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        Destroy(gameObject);
    }

    public void DebugLabel()
    {
        if (debugLabel != null)
        {
            debugLabel.GetComponent<TextMeshProUGUI>().text = $"Health: {health}\n" +
                $"Speed: {movementSpeed}\n" +
                $"Damage: {damage}\n" +
                $"Attack Speed: {attackSpeed}\n" +
                $"Enemy Class: {enemyData.stats.enemyClass}\n";
        }
    }
}