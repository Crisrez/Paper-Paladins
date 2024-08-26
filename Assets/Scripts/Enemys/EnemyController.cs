using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform zoneHeal1;
    [SerializeField] Transform zoneHeal2;
    [SerializeField] Transform zoneRecharge1;
    [SerializeField] Transform zoneRecharge2;

    [SerializeField] NavMeshAgent agent;

    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject spawnBall;

    [SerializeField] PersonajeJugable me;

    [SerializeField] GameObject player;

    [SerializeField] private Transform objetive;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        me = GetComponent<PersonajeJugable>();
        agent.speed = me.GetVelocidad();
        agent.destination = player.transform.position;

    }

    public void BotDispara()
    {
        gameObject.GetComponent<PersonajeJugable>().Disparar(ballPrefab, spawnBall);
    }

    private void Update()
    {
        agent.destination = objetive.position;
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Personaje"))
        {
            objetive = collider.transform;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Personaje"))
        {
            objetive = null;
        }
    }



}
