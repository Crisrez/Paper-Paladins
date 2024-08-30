using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public static class BomboocherUtility {
    public static T GetClosest<T>(this List<T> collection, Vector3 position) where T : MonoBehaviour{
        T result = default(T);
        float closestDistance = float.MaxValue;
        
        foreach (T o in collection) {
            var distance = Vector3.Distance(position, o.transform.position);
            if (distance < closestDistance) {
                closestDistance = distance;
                result = o;
            }
        }

        return result;
    }
} 

public class HealZone : MonoBehaviour {
    public static List<HealZone> HealZones = new();
    
    [SerializeField] float cooldown;

    [SerializeField] List<PersonajeJugable> personajesInZone = new();
    [SerializeField] PersonajeJugable? newpj, oldpj;

    public static HealZone GetClosestHealZone(Vector3 position) {
        HealZone result = null;
        float closestDistance = float.MaxValue;
        
        foreach (HealZone zone in HealZones) {
            var distance = Vector3.Distance(position, zone.transform.position);
            if (distance < closestDistance) {
                closestDistance = distance;
                result = zone;
            }
        }

        return result;
    }
    
    private void OnEnable() {
        HealZones.Add(this);
    }

    private void OnDisable() {
        HealZones.Remove(this);
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
                pj.Curar();
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
