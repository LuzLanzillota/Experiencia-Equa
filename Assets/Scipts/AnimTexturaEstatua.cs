using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class AnimTexturaEstatua : MonoBehaviour
{
    public Animator PuertaSalida;
    public Animator CuerpoSolarius;
    public Animator CabezaSolarius;
    public Puntos puntosScript;
    public GameObject mensajeFinalCueva;

    private bool SinEsfera = false;

    private void Start()
    {
        mensajeFinalCueva.SetActive(false);
    }
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
        yield return new WaitForSeconds(3);
        CuerpoSolarius.Play("ConEmissive");
        CabezaSolarius.Play("ConEmissive");
        mensajeFinalCueva.SetActive(true);
        yield return new WaitForSeconds(7);
        PuertaSalida.Play("PuertaSalidaCueva");
    }

    }