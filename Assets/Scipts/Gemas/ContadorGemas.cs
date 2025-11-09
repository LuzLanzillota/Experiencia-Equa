using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ContadorGemas : MonoBehaviour
{
    public int numeroEscena;        // ✅ Lo asignás en Inspector
    public TextMeshProUGUI textoPuntos;

    public GameObject PanelAzul;
    public GameObject PanelVioleta;
    public GameObject PanelRosa;
    public GameObject PanelAmarilla;
    public GameObject PanelNaranja;
    public GameObject PanelCeleste;

    private int escenaAnterior;
    public int puntos;

    private void Start()
    {
        DesactivarTodosLosPaneles();

        // ✅ Recuperar número de la escena anterior
        escenaAnterior = PlayerPrefs.GetInt("EscenaAnterior", -1);

        Debug.Log("➡️ Viene de escena: " + escenaAnterior);
        Debug.Log("📍 Escena actual número: " + numeroEscena);

        ConfigurarPuntosYPaneles();

        // ✅ Guardar escena actual para la próxima vez
        PlayerPrefs.SetInt("EscenaAnterior", numeroEscena);
    }

    private void Update()
    {
        textoPuntos.text = puntos + "/6";
    }

    private void DesactivarTodosLosPaneles()
    {
        PanelAzul.SetActive(false);
        PanelVioleta.SetActive(false);
        PanelRosa.SetActive(false);
        PanelAmarilla.SetActive(false);
        PanelNaranja.SetActive(false);
        PanelCeleste.SetActive(false);
    }

    private void ConfigurarPuntosYPaneles()
    {
        // ✅ MISMA LÓGICA, pero usando números
        // 0 = Escena0
        // 1 = Escena1
        // 2 = Escena2

        if (numeroEscena == 1)
        {
            if (escenaAnterior == 0)
            {
                puntos = 0;
            }
            else if (escenaAnterior == 2)
            {
                puntos = 3;
                PanelVioleta.SetActive(true);
                PanelNaranja.SetActive(true);
                PanelCeleste.SetActive(true);
            }
        }
        else if (numeroEscena == 2)
        {
            if (escenaAnterior == 0)
            {
                puntos = 0;
            }
            else if (escenaAnterior == 1)
            {
                puntos = 3;
                PanelAzul.SetActive(true);
                PanelAmarilla.SetActive(true);
                PanelRosa.SetActive(true);
            }
        }
        else if (numeroEscena == 0)
        {
            puntos = 0;
        }
    }
}

