using System.Collections.Generic;
using UnityEngine;

public class RechargeZone : MonoBehaviour {
    public static List<RechargeZone> RechargeZones = new();
    
    [SerializeField] float cooldown;
    [SerializeField] float time = 0f;
    
    private void OnEnable() {
        RechargeZones.Add(this);
    }

    private void OnDisable() {
        RechargeZones.Remove(this);
    }
    
    public static RechargeZone GetClosestHealZone(Vector3 position) {
        RechargeZone result = null;
        float closestDistance = float.MaxValue;
        
        foreach (RechargeZone zone in RechargeZones) {
            var distance = Vector3.Distance(position, zone.transform.position);
            if (distance < closestDistance) {
                closestDistance = distance;
                result = zone;
            }
        }

        return result;
    }
    private void FixedUpdate()
    {
        time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collider)
    {
        time = 0f;
        collider.gameObject.GetComponent<PersonajeJugable>().SetInZone(true);
        collider.gameObject.GetComponent<PersonajeJugable>().SetCanShoot(false);
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Personaje"))
        {
            if (time > cooldown)
            {
                time = 0f;
                collider.gameObject.GetComponent<PersonajeJugable>().Recargar();
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        collider.gameObject.GetComponent<PersonajeJugable>().SetInZone(false);
        collider.gameObject.GetComponent<PersonajeJugable>().SetCanShoot(true);
    }
}
