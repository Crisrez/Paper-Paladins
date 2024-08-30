using System.Collections.Generic;
using UnityEngine;

public class RechargeZone : MonoBehaviour {
    public static List<RechargeZone> RechargeZones = new();
    
    [SerializeField] float cooldown;

    [SerializeField] List<PersonajeJugable> personajesInZone = new();
    [SerializeField] PersonajeJugable? newpj, oldpj;

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
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Personaje"))
        {
            newpj = collider.gameObject.GetComponentInParent<PersonajeJugable>();

            if (!personajesInZone.Contains(newpj))
            {
                personajesInZone.Add(newpj);
                newpj.SetInZone(true);
                newpj.SetCanShoot(false);
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        foreach (PersonajeJugable pj in personajesInZone)
        {
            if (pj.GetTimerInZone() > cooldown)
            {
                pj.Recargar();
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        oldpj = collider.GetComponentInParent<PersonajeJugable>();

        oldpj.SetInZone(false);
        oldpj.SetCanShoot(true);
        personajesInZone.Remove(oldpj);
    }
}
