using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    [SerializeField]
    private float baseVelocity = 10;

    public PersonajeJugable owner;
    
    private void Start() {
        GetComponent<Rigidbody>().velocity = (gameObject.transform.forward + Vector3.up).normalized * baseVelocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        //GameObject jugador = GameObject.Find("Player");
        
        //Debug.Log(new Vector3(jugador.transform.position.x - gameObject.transform.position.x, 0, jugador.transform.position.z - gameObject.transform.position.z).magnitude);

        if (!other.CompareTag("Bombucha"))
        {
            //AnimacionImpacto(); //posible shader(?

            if (other.CompareTag("Personaje")) {
                var otherCharacter = other.GetComponentInParent<PersonajeJugable>();
                if (otherCharacter != owner) {
                    otherCharacter.RecibirDano(1f);
                }
            }
            
            Destroy(gameObject);
        }
    }

}
