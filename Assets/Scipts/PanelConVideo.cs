using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class PanelConVideo : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject panel;
    public VideoPlayer videoPlayer;
    public int indiceSiguienteEscena;
    public ContadorGemas contadorGemas;
    public GameObject panelSinGemas;
    private bool jugadorDentro = false;
    private bool panelActivo = false;

    void Start()
    {
        if (panel != null)
            panel.SetActive(false);

        if (videoPlayer != null)
            videoPlayer.gameObject.SetActive(false);

        if (panelSinGemas != null)
            panelSinGemas.SetActive(false);  // 🔹 Aseguramos que no esté visible al iniciar
    }

    void Update()
    {
        // Mostrar panel si el jugador está dentro y tiene 3 o 6 gemas
        if (jugadorDentro && !panelActivo && (contadorGemas.puntos == 3 || contadorGemas.puntos == 6))
        {
            panel.SetActive(true);
            panelActivo = true;
            panelSinGemas.SetActive(false);
        }

        // Mostrar panel de "sin gemas" solo si está dentro y no tiene las gemas necesarias
        if (jugadorDentro && (contadorGemas.puntos != 3 && contadorGemas.puntos != 6))
        {
            if (panel != null)
                panel.SetActive(false);

            if (panelSinGemas != null)
                panelSinGemas.SetActive(true);
        }
        else if (!jugadorDentro)  // Si sale del trigger, ocultamos panelSinGemas
        {
            if (panelSinGemas != null)
                panelSinGemas.SetActive(false);
        }

        // Accionar con tecla E si el panel válido está activo
        if (panelActivo && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(panel);
            panelActivo = false;
            StartCoroutine(ReproducirVideoYCambiarEscena());
        }
    }

    private IEnumerator ReproducirVideoYCambiarEscena()
    {
        if (videoPlayer != null)
        {
            videoPlayer.gameObject.SetActive(true);

            // 🔹 Esperar a que el video esté preparado
            videoPlayer.Prepare();
            yield return new WaitUntil(() => videoPlayer.isPrepared);

            // 🔹 Reproducir video
            videoPlayer.Play();
            Debug.Log("Video se reproduce");

            // 🔹 Esperar a que termine de reproducirse
            yield return new WaitUntil(() => !videoPlayer.isPlaying);

            // 🔹 Cambiar de escena
            SceneManager.LoadScene(indiceSiguienteEscena);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            jugadorDentro = true; // 🔹 Activamos que está dentro
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = false;
            if (panel != null)
                panel.SetActive(false);

            if (panelSinGemas != null)
                panelSinGemas.SetActive(false);
        }
    }
}
