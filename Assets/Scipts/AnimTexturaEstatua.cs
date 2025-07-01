using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class AnimTexturaEstatua : MonoBehaviour
{
    public Animator Esfera;
    public Animator PuertaSalida;

    public Puntos puntosScript;

    private bool SinEsfera = false;


    private void Update()
    {
        if (puntosScript.puntos == 12 && !SinEsfera)
        {
            StartCoroutine(ReproducirAnimacion());
        }

    }

    private IEnumerator ReproducirAnimacion ()
    {
        SinEsfera = true;
        Debug.Log("Puntos Suficientes");
        Esfera.Play("EsferaSolarius");
        yield return new WaitForSeconds(3);
        PuertaSalida.Play("PuertaSalidaCueva");
    }

    }