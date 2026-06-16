using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    public GameObject player;
    Vector2 currentLocation;
    //public Vector2 patrolPoint1 = new Vector2(-20.45f, -20.87f);
    //public Vector2 patrolPoint2 = new Vector2(-20.45f, -15.87f);
    //public Vector2 patrolPoint3 = new Vector2(-25.45f, -15.87f);
    //public Vector2 patrolPoint4 = new Vector2(-25.45f, -20.87f);
    [SerializeField] private Transform patrolPoint1;
    [SerializeField] private Transform patrolPoint2;
    [SerializeField] private Transform patrolPoint3;
    [SerializeField] private Transform patrolPoint4;
    [SerializeField] private Vector2[] patrolPoints;
    public float sightRangeEnemy = 5f; // Radius of the enemy's sight range
    public float attackRangeEnemy = 2f; // Radius of the enemy's attack range
    public float enemySpeed = 5f; // Speed at which the enemy moves
    [SerializeField] private LayerMask playerLayer; // Layer to detect the player
    private int stateInt;
    [SerializeField] Collider2D sightRangeCollider = null; // Collider to represent the sight range
    [SerializeField] Collider2D attackRangeCollider = null; // Collider to represent the attack range
    private int currentPatrolIndex = 0; // Index to track the current patrol point
    private bool playerDetected = false; // Flag to track if the player is detected
    private bool playerKilled = false; // Flag to track if the player has been killed 
    private float delay = 1.0f;
    [SerializeField] LevelManagerControllerScript resetLevel;

    void Start()
    {
        enemy = this.gameObject;
        this.transform.position = patrolPoint1.position;
        currentState = EnemyState.Patrol;


        // Initialize patrol points
        patrolPoints = new Vector2[] { patrolPoint1.position, patrolPoint2.position, patrolPoint3.position, patrolPoint4.position };
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
        //Debug.Log("Enemy detected a collision with: " + other.name);
        //Debug.Log("Enemy detected a ...other...: " + other);
        //Debug.Log("Enemy sighted a ...collision...: " + sightRangeCollider);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered sight range!");
            playerDetected = true;
            currentState = EnemyState.Chase;
        }
        /*else if (other == attackRangeCollider && other.CompareTag("Player"))
        {
            Debug.Log("Player entered attack range! Killing player.");
            KillPlayer();
        }*/
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left sight range.");
            playerDetected = false;
            currentState = EnemyState.Patrol;
        }
    }

    private void Idle()
    {
        Debug.Log("Enemy is idle.");
    }

    private void Patrol()
    {
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
    }

    private void Chase()
    {
        Debug.Log("Enemy is chasing the player.");
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
            attackRangeCollider = Physics2D.OverlapCircle(transform.position, attackRangeEnemy, playerLayer);
            Debug.Log("Attack range collider: " + attackRangeCollider);
            if(attackRangeCollider == true)
            {
                Attack();
                Debug.Log("TAKE THIS!");
                
            }
            else if(attackRangeCollider == null)
            {
                Debug.Log("Not in attack range.");
            }
            else
            {
                Debug.Log("Uh oh, something went wrong.");
            }
        }
    }

    private void Attack()
    {
        Debug.Log("Enemy is attacking the player.");
        KillPlayer();
    }

    private void KillPlayer()
    {
        Debug.Log("Player has been killed.");
        // Add logic to restart the level or handle player death
        StartCoroutine(TimeBeforeDestroy(delay));
        resetLevel.RestartLevel();
    }

    private void Reset()
    {
        Debug.Log("Enemy is resetting.");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRangeEnemy);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRangeEnemy);
    }
    private IEnumerator TimeBeforeDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(player);
    }
}
