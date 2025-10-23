using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContadorGemas : MonoBehaviour
{
    public int puntos = 0;

    public TextMeshProUGUI textoPuntos;

    private void Update()
    {
        textoPuntos.text = puntos + "/6";
    }
}