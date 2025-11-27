using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class PanelConVideo : MonoBehaviour
{
    [Header("Referencias")]
    public Animator panel;
    public VideoPlayer videoPlayer;
    public AudioSource narracion; // 👈 NUEVO: narración del panel
    public int indiceSiguienteEscena;
    public ContadorGemas contadorGemas;
    public GameObject panelSinGemas;

    [Header("Configuración de Escenas")]
    public int indiceEscenaGemasCompletas = 3;

    private bool jugadorDentro = false;
    private bool panelActivo = false;
    private bool audioEnReproduccion = false; // 👈 NUEVO

    void Start()
    {
        if (videoPlayer != null)
            videoPlayer.gameObject.SetActive(false);
        if (panelSinGemas != null)
            panelSinGemas.SetActive(false);
    }

    void Update()
    {
        if (!jugadorDentro || contadorGemas == null) return;

        int puntosActuales = contadorGemas.puntos;

        // --- 6 Gemas → Cargar escena ---
        if (puntosActuales >= 6)
        {
            CerrarPanel();
            if (panelSinGemas != null) panelSinGemas.SetActive(false);
            SceneManager.LoadScene(indiceEscenaGemasCompletas);
            return;
        }

        // --- 3 Gemas → Mostrar panel ---
        if (puntosActuales == 3)
        {
            if (!panelActivo)
            {
                panelActivo = true;
                if (panelSinGemas != null) panelSinGemas.SetActive(false);
                AbrirPanel();
            }
        }
        else
        {
            if (panelActivo)
            {
                CerrarPanel();
                panelActivo = false;
            }

            if (panelSinGemas != null)
                panelSinGemas.SetActive(true);
        }

        // --- Acción E ---
        if (panelActivo && !audioEnReproduccion && Input.GetKeyDown(KeyCode.E))
        {
            panelActivo = false;
            CerrarPanel();
            StartCoroutine(ReproducirVideoYCambiarEscena());
        }
    }

    private IEnumerator ReproducirVideoYCambiarEscena()
    {
        if (videoPlayer != null)
        {
            CerrarPanel();
            if (panelSinGemas != null) panelSinGemas.SetActive(false);

            videoPlayer.gameObject.SetActive(true);

            videoPlayer.Prepare();
            yield return new WaitUntil(() => videoPlayer.isPrepared);

            videoPlayer.Play();
            yield return new WaitUntil(() => !videoPlayer.isPlaying);

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

            CerrarPanel();
            if (panelSinGemas != null) panelSinGemas.SetActive(false);

            panelActivo = false;
        }
    }

    // ------------------------------------------------------
    //     CONTROL DEL PANEL + INICIO DE LA NARRACIÓN
    // ------------------------------------------------------
    private void AbrirPanel()
    {
        if (panel != null)
            panel.Play("FinalDia");

        if (narracion != null)
        {
            narracion.Play();
            audioEnReproduccion = true;
            StartCoroutine(EsperarAudioNarracion());
        }
    }

    private IEnumerator EsperarAudioNarracion()
    {
        // Bloquea interacción hasta que termine el audio
        yield return new WaitUntil(() => !narracion.isPlaying);

        audioEnReproduccion = false;
    }

    private void CerrarPanel()
    {
        if (panel != null)
            panel.Play("FinalDiaNoEsta");
    }
}