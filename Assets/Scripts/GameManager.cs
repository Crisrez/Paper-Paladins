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

    [SerializeField] List<GameObject> bombuchaUI;
    [SerializeField] List<GameObject> humedadUI;

    [SerializeField] PersonajeJugable player;

    private void FixedUpdate()
    {
        temporizador -= Time.deltaTime;

        textTimer.text = "" + Mathf.Floor(temporizador / 60).ToString("f0") + " : " + (temporizador % 60).ToString("f0");

        for (int i = 0; i < bombuchaUI.Count; i++)
        {
            if (i < player.GetMunicion())
            {
                bombuchaUI[i].gameObject.SetActive(true);
            }
            else
            {
                bombuchaUI[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < bombuchaUI.Count; i++)
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



}
