using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class PanelConVideo : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject panel;
    public VideoPlayer videoPlayer;
    public int indiceSiguienteEscena; // Usamos índice en lugar de nombre
    public ContadorGemas contadorGemas;

    private bool jugadorDentro = false;
    private bool panelActivo = false;

    void Start()
    {
        if (panel != null)
            panel.SetActive(false);

        if (videoPlayer != null)
            videoPlayer.gameObject.SetActive(false);
    }

    void Update()
    {
        if (jugadorDentro && !panelActivo && (contadorGemas.puntos == 3 || contadorGemas.puntos == 6))
        {
            panel.SetActive(true);
            panelActivo = true;
        }

        if (contadorGemas.puntos == 1 || contadorGemas.puntos == 2 || contadorGemas.puntos == 4 || contadorGemas.puntos == 5)
        {
            if (panel != null)
                panel.SetActive(false);
        }

        if (panelActivo && Input.GetKeyDown(KeyCode.E))
        {
            Destroy (panel);
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
            jugadorDentro = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = false;
            if (panel != null)
                panel.SetActive(false);
        }
    }
}


