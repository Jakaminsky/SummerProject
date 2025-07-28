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
        stats = gameObject.GetComponent<EnemyStats>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.speed = stats.enemySpeed;
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
