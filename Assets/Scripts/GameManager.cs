using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

public class GameManager : MonoBehaviour
{
    [SerializeField] float timer;

    [SerializeField] TextMeshProUGUI textTimer;

    [SerializeField] private GameObject ammoUI;
    [SerializeField] List<GameObject> humedadUI;
    [SerializeField] GameObject M_Pause;

    [SerializeField] PersonajeJugable player;
    [SerializeField] InputController inputController;

    [SerializeField] bool isPaused = false;

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;

        textTimer.text = "" + Mathf.Floor(timer / 60).ToString("f0") + " : " + (timer % 60).ToString("f0");

        UpdateAmmoUI();

         for (int i = 0; i < humedadUI.Count; i++)
         {
             if (i < player.GetVida())
             {
                 humedadUI[i+1].gameObject.SetActive(true);
             }
             else
             {
                 humedadUI[i+1].gameObject.SetActive(false);
             }
         }
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
        for (int i = 0; i < ammoUI.transform.childCount; i++) {
            ammoUITransform.GetChild(i).gameObject.SetActive(i < ammoCount);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
