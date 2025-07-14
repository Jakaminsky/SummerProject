using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BasicAI : MonoBehaviour
{
    private GameObject player;
    EnemyStats stats;
    private NavMeshAgent agent;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = stats.enemySpeed;
    }

    private void Update()
    {
        moveTowardPlayer();
    }

    private void moveTowardPlayer()
    {
        if(player != null)
        {
            agent.SetDestination(player.transform.position);
        }
    }
}
