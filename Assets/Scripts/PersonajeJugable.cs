using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PersonajeJugable : MonoBehaviour {
    [SerializeField] private float vidaMax;
    [SerializeField] private float vida;
    [SerializeField] private float velocidad;
    [SerializeField] private float municionMax;
    [SerializeField] private float municion;
    [SerializeField] private bool canShoot = true;
    [SerializeField] private bool canRespawn;
    [SerializeField] private bool inZone = false;
    [SerializeField] private GameObject[] pointsRespawn = new GameObject[5];
    [SerializeField] private float cooldown;
    [SerializeField] private float timer = 5f;

    public float GetVelocidad() { return velocidad; }

    public float GetMunicion() { return municion; }

    public float GetVida() { return vida; }

    public float GetVidaMax() { return vidaMax; }

    public float GetMunicionMax() { return municionMax; }

    public void SetCanShoot(bool _canShoot) {
        canShoot = _canShoot;
    }

    public void SetInZone(bool _inZone)
    {
        inZone = _inZone;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (cooldown < timer && !inZone)
        {
            SetCanShoot(true);
        }
        else
        {
            SetCanShoot(false);
        }

    }

    public void Disparar(GameObject bullet, GameObject weapon) {
        if (vida > 0 && municion > 0 && canShoot) {
            timer = 0f;
            GenerarBala(bullet, weapon);
            municion--;
            Debug.Log("Disminui municion");
        }
        else {
            Debug.Log("SIN MUNICION!!!");
        }
    }

    public void RecibirDano(float dmg) {
        Debug.Log("Recibi daño");
        vida = vida - dmg;
        if (vida <= 0) {
            Muerte();
        }
    }

    public void Curar() {
        if (vida < vidaMax) {
            vida++;
            Debug.Log("Regenere vida");
        }
    }

    public void Recargar() {
        if (municion < municionMax) {
            municion++;
            Debug.Log("Recargue bombucha");
        }
    }


    public void Muerte() {
        if (!canRespawn) {
            Debug.Log("Mori");
            //Destroy(this);
        }
        else {
            Debug.Log("Respawnee");
        }
    }

    public void GenerarBala(GameObject bullet, GameObject weapon) {
        var instance = GameObject.Instantiate(bullet, weapon.transform.position, weapon.transform.rotation);
        var ballInstance = instance.GetComponent<Ball>();
        ballInstance.owner = this;
        Debug.Log("Dispare");
    }

}
