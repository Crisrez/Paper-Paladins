using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealZone : MonoBehaviour
{
    [SerializeField] float cooldown;
    [SerializeField] float time = 0f;

    private void FixedUpdate()
    {
        time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        time = 0f;
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
}
