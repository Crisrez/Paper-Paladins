using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour {
    NavMeshAgent agent;
    PersonajeJugable me;
    private EnemySensor sensor;

    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject spawnBall;

    [SerializeField] GameObject player;

    [SerializeField]
    private float engagementDistance = 5;
    [SerializeField]
    private float attackCooldown = 0.8f;

    private Transform agentTarget;

    private PersonajeJugable targetCharacter;


    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        me = GetComponent<PersonajeJugable>();
        sensor = GetComponentInChildren<EnemySensor>();
        
        agent.speed = me.GetVelocidad();
        agent.destination = player.transform.position;
    }

    public void BotDispara() {
        me.Disparar(ballPrefab, spawnBall);
    }

    private void FixedUpdate() {
        UpdateTarget(); 
        
        
        agent.destination = agentTarget.position;
    }

    private void UpdateTarget() {
        var position = transform.position;
        if (me.GetVida() < 2f) {
            var closestHealZone = HealZone.HealZones.GetClosest(position);

            if (closestHealZone != null) {
                agentTarget = closestHealZone.transform;
                agent.stoppingDistance = 1;
                targetCharacter = null;
                return;
            }
        }

        if (me.GetMunicion() == 0) {
            RechargeZone closestRechargeZone = RechargeZone.RechargeZones.GetClosest(position);
            if (closestRechargeZone) {
                agentTarget = closestRechargeZone.transform;
                agent.stoppingDistance = 1;
                targetCharacter = null;
                return;
            }
        }

        if (sensor.HasTargets()) {
            targetCharacter =  sensor.Targets.GetClosest(position);
            if (targetCharacter != null) {
                agentTarget = targetCharacter.transform;
                agent.stoppingDistance = engagementDistance;
                BotDispara();
                return;
            }
        }
    }
}
