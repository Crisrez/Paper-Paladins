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

    [SerializeField] private float cooldownShoot;
    [SerializeField] private float timerShoot = 5f;
    [SerializeField] private float timerInZone;

    public float GetVelocidad() { return velocidad; }

    public float GetMunicion() { return municion; }

    public float GetMunicionMax() { return municionMax; }

    public float GetVida() { return vida; }

    public float GetVidaMax() { return vidaMax; }

    public float GetTimerInZone() { return timerInZone; }

    public void SetCanShoot(bool _canShoot) {
        canShoot = _canShoot;
    }

    public void SetInZone(bool _inZone) {
        inZone = _inZone;
    }

    private void FixedUpdate() {
        if (!inZone) {
            timerShoot += Time.deltaTime;
            timerInZone = 0f;

            if (cooldownShoot < timerShoot) {
                SetCanShoot(true);
            }
            else {
                SetCanShoot(false);
            }
        }
        else {
            timerShoot = 0f;
            timerInZone += Time.deltaTime;
        }

    }

    public void Disparar(GameObject bullet, GameObject weapon) {
        if (vida > 0 && municion > 0 && canShoot) {
            timerShoot = 0f;
            GenerarBala(bullet, weapon);
            municion--;
            //Debug.Log("Disminui municion");
        }
        else {
            //Debug.Log("SIN MUNICION!!!");
        }
    }

    public void RecibirDano(float dmg) {
        if (vida == 0) return;
        
        vida -= dmg;
        if (vida <= 0) {
            Muerte();
        }
    }

    public void Curar() {
        if (vida < vidaMax) {
            vida++;
            Debug.Log("Regenere vida");
            timerInZone = 0f;
        }
    }

    public void Recargar() {
        if (municion < municionMax) {
            municion++;
            timerInZone = 0f;

            //Debug.Log("Recargue bombucha");
        }
    }


    public void Muerte() {
        if (!canRespawn) {
            //Debug.Log("Mori");
            //Destroy(this);
        }
        else {
            //Debug.Log("Respawnee");
            Respawn();
        }
    }

    public void GenerarBala(GameObject bullet, GameObject weapon) {
        var instance = GameObject.Instantiate(bullet, weapon.transform.position, weapon.transform.rotation);
        var ballInstance = instance.GetComponent<Ball>();
        ballInstance.owner = this;
        //Debug.Log("Dispare");
    }

    public void Respawn() {
        StartCoroutine(RespawnCoroutine());

        IEnumerator RespawnCoroutine() {
            yield return new WaitForSeconds(5f);
            gameObject.transform.position = RespawnPoint.GetRespawnPoint().transform.position;
        }
    }

}
