using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Personaje
{
    private string nombre;
    private float vida;
    private float velocidad;
    private float municion;
    private float municionMax;
    private bool canRespawn;
    private Transform[] pointsRespawn = new Transform[5];

    public void Disparar()
    {
        if (vida > 0 && municion > 0)
        {
            GenerarBala();
            municion--; 
            Debug.Log("Disminui municion");
        }
    }

    public void disminuirHP()
    {
        Debug.Log("Recibi da�o");
        vida--;
        if (vida <= 0)
        {
            Muerte();
        }
    }

    public void Regenerar()
    {
        vida++;
        Debug.Log("Regenere vida");
    }

    public void Recargar()
    {
        if (municion < municionMax)
        {
            municion++;
            Debug.Log("Recargue bombucha");
        }
    }


    public void Muerte()
    {
        if (!canRespawn)
        {
            Debug.Log("Mori");
            //Destroy(this);
        }
        else
        {
            Debug.Log("Respawnee");


        }
    }

    public void GenerarBala()
    {
        Debug.Log("Dispare");
    }

}
