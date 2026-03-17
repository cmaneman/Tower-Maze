using UnityEngine;

enum EnemyState
{
    Idle, //Enemy stands still, does not move or attack
    Patrol,//Enemy moves back and forth between multiple points, does not attack
    Chase, //Follows the player until it touches the player, then switches to attack
    Attack, //Basically like a kill switch, when the enemy touches the player, the player dies and the level restarts. Then switches to idle or patrol
    Reset //When the level ends, set all enemies to this state, which will stop them from moving and attacking...sets them to idle then patrol when the next level starts
}