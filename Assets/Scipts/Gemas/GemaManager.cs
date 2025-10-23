using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GemaManager : MonoBehaviour
{
    public static GemaManager instance;

    [Header("UI")]
    public TextMeshProUGUI textoContador;
    public Image gemaRojaUI;
    public Image gemaAzulUI;
    public Image gemaVerdeUI;

    private int totalGemas = 0;
    private Dictionary<string, bool> gemasRecolectadas = new Dictionary<string, bool>();

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        // Inicializamos las gemas como no recolectadas
        gemasRecolectadas["rojo"] = false;
        gemasRecolectadas["azul"] = false;
        gemasRecolectadas["verde"] = false;

        ActualizarUI();
    }

    public void RecogerGema(string color)
    {
        totalGemas++;
        gemasRecolectadas[color] = true;
        ActualizarUI();
    }

    void ActualizarUI()
    {
        textoContador.text = "Gemas: " + totalGemas;

        // Activamos visualmente las gemas recolectadas
        gemaRojaUI.color = gemasRecolectadas["rojo"] ? Color.red : Color.gray;
        gemaAzulUI.color = gemasRecolectadas["azul"] ? Color.blue : Color.gray;
        gemaVerdeUI.color = gemasRecolectadas["verde"] ? Color.green : Color.gray;
    }
}
