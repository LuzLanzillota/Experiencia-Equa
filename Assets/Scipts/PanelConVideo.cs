using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class PanelConVideo : MonoBehaviour
{
    [Header("Referencias")]
    public Animator panel;
    public VideoPlayer video3Gemas;
    public VideoPlayer video6Gemas;

    public AudioSource narracion;
    public int indiceSiguienteEscena;
    public int indiceEscenaGemasCompletas;

    public ContadorGemas contadorGemas;
    public GameObject panelSinGemas;

    private bool jugadorDentro = false;
    private bool panelActivo = false;
    private bool audioEnReproduccion = false;
    private bool esperandoTecla = false;
    private bool narracionYaSonada = false;


    void Start()
    {
        if (video3Gemas != null)
            video3Gemas.gameObject.SetActive(false);

        if (video6Gemas != null)
            video6Gemas.gameObject.SetActive(false);

        if (panelSinGemas != null)
            panelSinGemas.SetActive(false);
    }

    void Update()
    {
        if (!jugadorDentro || contadorGemas == null) return;

        int puntos = contadorGemas.puntos;

        // --- Mostrar panel cuando tiene 3 o 6 gemas ---
        if (puntos == 3 || puntos >= 6)
        {
            if (!panelActivo)
            {
                panelActivo = true;
                panelSinGemas.SetActive(false);
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

            panelSinGemas.SetActive(true);
        }

        // --- Acción E para reproducir el video ---
        if (panelActivo && !audioEnReproduccion && esperandoTecla && Input.GetKeyDown(KeyCode.E))
        {
            panelActivo = false;
            esperandoTecla = false;

            // 🔥 NUEVO → Ocultar el panel SIN animación, así no pide 2 veces la E
            panel.gameObject.SetActive(false);

            // Detener narración ANTES del video
            if (narracion != null && narracion.isPlaying)
                narracion.Stop();

            if (puntos == 3)
                StartCoroutine(ReproducirVideo3());
            else if (puntos >= 6)
                StartCoroutine(ReproducirVideo6());
        }
    }

    // ------------------------------------------------------
    // VIDEO 3 GEMAS
    // ------------------------------------------------------
    private IEnumerator ReproducirVideo3()
    {
        video3Gemas.gameObject.SetActive(true);

        video3Gemas.Prepare();
        yield return new WaitUntil(() => video3Gemas.isPrepared);

        video3Gemas.Play();
        yield return new WaitUntil(() => !video3Gemas.isPlaying);

        SceneManager.LoadScene(indiceSiguienteEscena);
    }

    // ------------------------------------------------------
    // VIDEO 6 GEMAS
    // ------------------------------------------------------
    private IEnumerator ReproducirVideo6()
    {
        video6Gemas.gameObject.SetActive(true);

        video6Gemas.Prepare();
        yield return new WaitUntil(() => video6Gemas.isPrepared);

        video6Gemas.Play();
        yield return new WaitUntil(() => !video6Gemas.isPlaying);

        SceneManager.LoadScene(indiceEscenaGemasCompletas);
    }

    // ------------------------------------------------------
    // PANEL + NARRACIÓN
    // ------------------------------------------------------
    private void AbrirPanel()
    {
        panel.gameObject.SetActive(true);
        panel.Play("FinalDia");

        if (narracion != null && !narracionYaSonada)
        {
            narracion.Play();
            audioEnReproduccion = true;
            narracionYaSonada = true;      
            StartCoroutine(EsperarFinNarracion());
        }
    }

    private IEnumerator EsperarFinNarracion()
    {
        yield return new WaitUntil(() => !narracion.isPlaying);

        audioEnReproduccion = false;

        // recién aquí puede tocar la E
        esperandoTecla = true;
    }

    private void CerrarPanel()
    {
        panel.Play("FinalDiaNoEsta");
        esperandoTecla = false;
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
            panelSinGemas.SetActive(false);
            panelActivo = false;

            // si sale, detener narración
            if (narracion != null && narracion.isPlaying)
                narracion.Stop();
        }
    }
}

