using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyType1_AI : MonoBehaviour
{
    enum EnemyState
    {
        Idle, //Enemy stands still, does not move or attack
        Patrol,//Enemy moves back and forth between multiple points, does not attack
        Chase, //Follows the player until it touches the player, then switches to attack
        Attack, //Basically like a kill switch, when the enemy touches the player, the player dies and the level restarts. Then switches to idle or patrol
        Reset //When the level ends, set all enemies to this state, which will stop them from moving and attacking...sets them to idle then patrol when the next level starts
    }
    EnemyState currentState = EnemyState.Idle;
    GameObject enemy;
    GameObject player;
    Vector2 currentLocation;
    public Vector2 patrolPoint1 = new Vector2(-20.45f, -20.87f);
    public Vector2 patrolPoint2 = new Vector2(-20.45f, -15.87f);
    public Vector2 patrolPoint3 = new Vector2(-25.45f, -15.87f);
    public Vector2 patrolPoint4 = new Vector2(-25.45f, -20.87f);
    [SerializeField]private Vector2[] patrolPoints;
    public float sightRangeEnemy = 5f; // Radius of the enemy's sight range
    public float enemySpeed = 5f; // Speed at which the enemy moves
    private LayerMask playerLayer; // Layer to detect the player
    private int stateInt;
    [SerializeField] Collider2D sightRangeCollider = null; // Collider to represent the sight range
    [SerializeField] Collider2D attackRangeCollider = null; // Collider to represent the attack range
    private int currentPatrolIndex = 0; // Index to track the current patrol point
    private bool playerDetected = false; // Flag to track if the player is detected

    void Start()
    {
        enemy = this.gameObject;
        this.transform.position = new Vector2(-20.45f, -20.87f);
        currentState = EnemyState.Patrol;


        // Initialize patrol points
        patrolPoints = new Vector2[] { patrolPoint1, patrolPoint2, patrolPoint3, patrolPoint4 };
    }

    void Update()
    {
        if (playerDetected && currentState != EnemyState.Chase)
        {
            currentState = EnemyState.Chase;
        }
    }

    void FixedUpdate()
    {
        EnemyLogic(currentState);
    }

    void EnemyLogic(EnemyState state)
    {
        stateInt = (int)state;
        switch (stateInt)
        {
            case 0:
                Idle();
                break;
            case 1:
                Patrol();
                break;
            case 2:
                Chase();
                break;
            case 3:
                Attack();
                break;
            case 4:
                Reset();
                break;
            default:
                Debug.Log("Unknown state.");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == sightRangeCollider && other.CompareTag("Player"))
        {
            Debug.Log("Player entered sight range!");
            playerDetected = true;
        }
        else if (other == attackRangeCollider && other.CompareTag("Player"))
        {
            Debug.Log("Player entered attack range! Killing player.");
            KillPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == sightRangeCollider && other.CompareTag("Player"))
        {
            Debug.Log("Player left sight range.");
            playerDetected = false;
        }
    }

    private void Idle()
    {
        Debug.Log("Enemy is idle.");
    }

    private void Patrol()
    {
        sightRangeCollider = Physics2D.OverlapCircle(transform.position, sightRangeEnemy, playerLayer);
        if (patrolPoints.Length == 0) 
        {
            Idle();
        }

        Vector2 targetPoint = patrolPoints[currentPatrolIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetPoint, enemySpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

        if(sightRangeCollider != null && sightRangeCollider.CompareTag("Player"))
        {
            
        }
    }

    private void Chase()
    {
        Debug.Log("Enemy is chasing the player.");
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
        }
    }

    private void Attack()
    {
        Debug.Log("Enemy is attacking the player.");
    }

    private void KillPlayer()
    {
        Debug.Log("Player has been killed.");
        // Add logic to restart the level or handle player death
    }

    private void Reset()
    {
        Debug.Log("Enemy is resetting.");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRangeEnemy);
    }
}
