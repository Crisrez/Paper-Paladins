using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        GameObject jugador = GameObject.Find("Player");

        Debug.Log((jugador.transform.position - this.gameObject.transform.position).magnitude);

        if (!collision.gameObject.CompareTag("Bombucha"))
        {
            Destroy(this.gameObject);

            //AnimacionImpacto(); //posible shader(?

            if (collision.gameObject.CompareTag("Personaje"))
            {
                collision.gameObject.GetComponent<PersonajeJugable>().RecibirDano(1f);
            }
        }
    }



}
