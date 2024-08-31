using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] float timer;

    [SerializeField] TextMeshProUGUI textTimer;

    [SerializeField] private GameObject ammoUI;
    [SerializeField] private GameObject healthUI;
    [SerializeField] GameObject M_Pause;

    [SerializeField] PersonajeJugable player;
    [SerializeField] InputController inputController;

    [SerializeField] bool isPaused = false;

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;

        textTimer.text = "" + Mathf.Floor(timer / 60).ToString("f0") + " : " + (timer % 60).ToString("f0");

        UpdateAmmoUI();
    UpdateHealthUI();
    }

    private void Update()
    {
        if (inputController.Pausar())
        {
            ActivarMenu(M_Pause);
        }
    }

    public void ActivarMenu(GameObject menu)
    {
        if (!isPaused)
        {
            menu.SetActive(true);
            ActivarPause();
        }
        else
        {
            menu.SetActive(false);
            ActivarPause();
        }

    }

    public void ActivarPause()
    {
        if (!isPaused)
        {
            ChangeTime(0);
            isPaused = true;
        }
        else
        {
            ChangeTime(1);
            isPaused = false;
        }

    }

    public void ChangeTime(int valorTiempo)
    {
        Time.timeScale = valorTiempo;
    }

    public bool isGamePaused()
    {
        return isPaused;
    }

    public float GetTimer()
    {
        return timer;
    }


    private void UpdateAmmoUI() {
        float ammoCount = player.GetMunicion();
        Transform ammoUITransform = ammoUI.transform;
        for (int i = 0; i < ammoUITransform.childCount; i++) {
            ammoUITransform.GetChild(i).gameObject.SetActive(i < ammoCount);
        }
    }

    private void UpdateHealthUI() {
        float health = player.GetVida();
        Transform healthUITransform = healthUI.transform;
        for (int i = 0; i < healthUITransform.childCount; i++) {
            healthUITransform.GetChild(i).gameObject.SetActive(i < health);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
