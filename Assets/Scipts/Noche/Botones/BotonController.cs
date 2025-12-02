using UnityEngine;
using System.Collections;

public class BotonController : MonoBehaviour
{
    [Header("Configuración general")]
    private string nombreBotonCorrecto = "BotonNaranja"; // Tag del botón correcto

    [Header("Referencias externas")]
    public ParticleSystem luciernagas;

    public Animator Gema;
    private Animator animator;

    public AudioSource sonidoError;
    public AudioSource sonidoLuciernagas;
    public AudioSource sonidoCorrecto;
   
    public GameObject ColiderGema;
    public GameObject ColiderPared;
    public GameObject ColiderParedNota;
    public GameObject ColiderInteracion;

    [Header("Tiempos")]
    public float delayAparicionGema = 2f; // ⏱ tiempo que tarda en aparecer la gema después de las luciérnagas

    
    private bool jugadorCerca = false;
    private bool luciernagasActivadas = false;

    private void Start()
    {
        animator = GetComponent<Animator>();

        // 🔇 Luciérnagas y sonido comienzan desactivados
        if (luciernagas != null && luciernagas.isPlaying)
            luciernagas.Stop();

        if (sonidoLuciernagas != null && sonidoLuciernagas.isPlaying)
            sonidoLuciernagas.Stop();

        ColiderGema.SetActive(false);
        ColiderPared.SetActive(true);
        ColiderInteracion.SetActive(true);
        ColiderParedNota.SetActive(true);

    }

    private void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            if (animator != null)
                animator.Play("BotonMov");

            // ✅ Verifica si el botón tiene el tag correcto
            if (CompareTag(nombreBotonCorrecto))
            {
                if (!luciernagasActivadas)
                {
                    // 🔥 Activa luciérnagas y sonido
                    if (luciernagas != null && !luciernagas.isPlaying)
                        luciernagas.Play();

                    if (sonidoLuciernagas != null && !sonidoLuciernagas.isPlaying)
                        sonidoLuciernagas.Play();

                    if (sonidoCorrecto != null)
                        sonidoCorrecto.Play();

                    luciernagasActivadas = true;

                    // ⏱ Espera unos segundos antes de mostrar la gema
                    StartCoroutine(AparecerGemaConDelay());
                }
            }
            else
            {
                if (sonidoError != null)
                    sonidoError.Play();
            }
        }
    }

    private IEnumerator AparecerGemaConDelay()
    {
        yield return new WaitForSeconds(delayAparicionGema);

        // 👇 Oculta el panel ANTES de desactivar los colliders
        PanelTrigger panelTrigger = ColiderInteracion.GetComponent<PanelTrigger>();
        if (panelTrigger != null)
            panelTrigger.OcultarPanel();

        // 🔓 Activa colider y animación de la gema
        ColiderPared.SetActive(false);
        ColiderInteracion.SetActive(false);
        ColiderParedNota.SetActive(false);
        ColiderGema.SetActive(true);

        if (Gema != null)
            Gema.Play("Esta");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            jugadorCerca = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            jugadorCerca = false;
    }
}
