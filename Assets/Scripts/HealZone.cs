using System;
using System.Collections;
using System.Collections.Generic;using UnityEditor;
using UnityEngine;

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
    [SerializeField] float time = 0f;

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
                collider.gameObject.GetComponent<PersonajeJugable>().Curar();
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        collider.gameObject.GetComponent<PersonajeJugable>().SetInZone(false);
        collider.gameObject.GetComponent<PersonajeJugable>().SetCanShoot(true);
    }
}
