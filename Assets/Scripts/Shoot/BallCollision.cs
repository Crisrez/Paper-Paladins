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
        Destroy(this);

        DispararAnimacion();

        if (collision.gameObject.CompareTag("PJ"))
        {
            collision.disminurHP();
        }

    }



}
