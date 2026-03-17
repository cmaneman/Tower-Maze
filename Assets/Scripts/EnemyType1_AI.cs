using Unity.VisualScripting;
using UnityEngine;

public class EnemyType1_AI : MonoBehaviour
{
    EnemyState enemyState = EnemyState.Idle; //When level ends, set
    [SerializeField] Transform PointA;
    [SerializeField] Transform PointB;
    [SerializeField] Transform PointC;
    [SerializeField] Transform PointD;
    void Start()
    {
        SetToPatrol(); //If Idle, set to patrol at the start of the level
    }

    // Update is called once per frame
    void Update()
    {
        //Add functions that tell the enemy what to do in each state...Switch statement
        switch(enemyState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Chase:
                Chase();
                break;
            case EnemyState.Reset:
                Reset();
                break;
            default:
                Idle();
                break;
        }
    }

    void SetToPatrol()
    {
        enemyState = EnemyState.Patrol;

    }
    void Idle()
    {
        Debug.Log("Enemy is Idle");
        //Add code to make the enemy stand still, does not move or attack
    }
    void Patrol()
    {
        Debug.Log("Enemy is Patrolling");
        //Add code to move between PointA and PointB, then switch to PointC and PointD, then back to PointA and PointB, etc.
    }
    void Chase()
    {
        Debug.Log("Enemy is Chasing Player");
        //Add code to chase the player until it touches the player, then switch to attack
    }
    void KillPlayer() //Attack
    {        Debug.Log("Enemy Killed Player");
        //Add code to attack the player, when the enemy touches the player, the player dies and the level restarts. Then switches to idle or patrol
    }
    void Reset()
    {
        Debug.Log("Enemy Reseted");
        //When the level ends, set all enemies to this state, which will stop them from moving and attacking...sets them to idle then patrol when the next level starts
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Add code to switch states when the enemy touches the player, or when the level ends, etc.
        if(other.CompareTag("Player"))
        { 
            KillPlayer();
        }
    }

    //Idle: Enemy stands still, does not move or attack
    //Patrol: Enemy moves back and forth between two points, does not attack
    //Chase: Enemy chases the player until it touches the player, then switches to attack
    //Attack: Enemy attacks the player, when the enemy touches the player, the player dies and the level restarts. Then switches to idle or patrol
    //Reset: When the level ends, set all enemies to this state, which will stop them from moving and attacking...sets them to idle then patrol when the next level starts
}
