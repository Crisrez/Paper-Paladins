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
        me.Disparar(ballPrefab, spawnBall);
    }

    private void Update()
    {
        agent.destination = objetive.position;
    }

    private void FixedUpdate()
    {
        if (me.GetVida() < 2f)
        {
            do
            {
                if ((gameObject.transform.position - zoneHeal1.position).magnitude < (gameObject.transform.position - zoneHeal2.position).magnitude)
                {
                    objetive = zoneHeal1;
                    return;
                }
                else
                {
                    objetive = zoneHeal2;
                    return;
                }
            } while (me.GetVida() != me.GetVidaMax());
        }

        if (me.GetMunicion() == 0)
        {
            if ((gameObject.transform.position - zoneRecharge1.position).magnitude < (gameObject.transform.position - zoneRecharge2.position).magnitude)
            {
                objetive = zoneRecharge1;
                return;
            }
            else
            {
                objetive = zoneRecharge2;
                return;
            }
        }
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
