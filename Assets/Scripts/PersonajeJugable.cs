using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PersonajeJugable : MonoBehaviour
{
    [SerializeField] private float vidaMax;
    [SerializeField] private float vida;
    [SerializeField] private float velocidad;
    [SerializeField] private float municionMax;
    [SerializeField] private float municion;
    [SerializeField] private bool canRespawn;
    [SerializeField] private GameObject[] pointsRespawn = new GameObject[5];

    public float GetVelocidad(){ return velocidad; }

    public float GetMunicion(){ return municion; }

    public float GetVida() { return vida; }

    



    public void Disparar()
    {
        if (vida > 0 && municion > 0)
        {
            GenerarBala();
            municion--;
            Debug.Log("Disminui municion");
        }
    }

    public void RecibirDano()
    {
        Debug.Log("Recibi daño");
        vida--;
        if (vida <= 0)
        {
            Muerte();
        }
    }

    public void Curar()
    {
        if (vida < vidaMax)
        {
            vida++;
            Debug.Log("Regenere vida");
        }
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



    private void Start()
    {
        
    }
}
