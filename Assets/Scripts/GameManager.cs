using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] float temporizador;

    [SerializeField] TextMeshProUGUI textTimer;

    [SerializeField] private GameObject ammoUI;
    [SerializeField] List<GameObject> humedadUI;

    [SerializeField] PersonajeJugable player;

    private void FixedUpdate()
    {
        temporizador -= Time.deltaTime;

        textTimer.text = "" + Mathf.Floor(temporizador / 60).ToString("f0") + " : " + (temporizador % 60).ToString("f0");

        UpdateAmmoUI();

        // for (int i = 0; i < humedadUI.Count; i++)
        // {
        //     if (i < player.GetVida())
        //     {
        //         humedadUI[i+1].gameObject.SetActive(true);
        //     }
        //     else
        //     {
        //         humedadUI[i+1].gameObject.SetActive(false);
        //     }
        // }
    }
    
    private void UpdateAmmoUI() {
        float ammoCount = player.GetMunicion();
        Transform ammoUITransform = ammoUI.transform;
        for (int i = 0; i < ammoUI.transform.childCount; i++) {
            ammoUITransform.GetChild(i).gameObject.SetActive(i < ammoCount);
        }
    }



}
