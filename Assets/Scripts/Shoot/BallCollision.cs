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
        if (!collision.gameObject.CompareTag("Bombucha"))
        {
            Destroy(this.gameObject);

            //AnimDisparar();

            if (collision.gameObject.CompareTag("Personaje"))
            {
                collision.gameObject.GetComponent<PersonajeJugable>().RecibirDano();
            }
        }
    }



}
