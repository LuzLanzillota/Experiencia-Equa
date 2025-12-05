using System.Collections;
using UnityEngine;

public class ActivacionFinal : MonoBehaviour
{
    public Animator animGema1;
    public Animator animGema2;
    public Animator animGema3;
    public Animator animGema4;
    public Animator animGema5;
    public Animator animGema6;

    public string nombreAnimacion = "AnimGema";

    public AudioSource sonidoActivacion;

    public ParticleSystem[] particulas;

    // ⭐ Animación de la luz
    public Animator animLuz;
    public string animLuzNombre = "EncenderLuz";

    // ⭐ NUEVO: Collider mje2
    public GameObject mje2;
    public float tiempoParaActivarMje2 = 2f;

    private bool jugadorEnZona = false;
    private bool gemasActivadas = false;

    private void Start()
    {
        // asegurar que mje2 arranque desactivado
        if (mje2 != null)
            mje2.SetActive(false);
    }

    private void Update()
    {
        if (jugadorEnZona && Input.GetKeyDown(KeyCode.E))
        {
            if (!gemasActivadas)
            {
                StartCoroutine(ActivarGemas());
            }
            else
            {
                Debug.Log("🔵 Las gemas ya están activadas.");
            }
        }
    }

    private IEnumerator ActivarGemas()
    {
        gemasActivadas = true;

        Debug.Log("🔵 Activando 6 gemas sin trigger...");

        if (sonidoActivacion != null)
            sonidoActivacion.Play();

        yield return new WaitForEndOfFrame();

        animGema1.Play(nombreAnimacion);
        animGema2.Play(nombreAnimacion);
        animGema3.Play(nombreAnimacion);
        animGema4.Play(nombreAnimacion);
        animGema5.Play(nombreAnimacion);
        animGema6.Play(nombreAnimacion);

        Debug.Log("✨ Se activaron las 6 animaciones.");

        yield return new WaitForSeconds(1f);

        // ⭐ Activar partículas principales
        if (particulas != null && particulas.Length > 0)
        {
            foreach (ParticleSystem ps in particulas)
            {
                if (ps != null)
                    ps.Play();
            }
        }

        // ⭐ Pequeña espera antes de encender la luz
        yield return new WaitForSeconds(0.8f);

        // ⭐ Activar animación de luz
        if (animLuz != null)
        {
            animLuz.Play(animLuzNombre);
            Debug.Log("💡 Animación de luz activada.");
        }

        // ⭐ NUEVO: esperar unos segundos y activar mje2
        yield return new WaitForSeconds(tiempoParaActivarMje2);

        if (mje2 != null)
        {
            mje2.SetActive(true);
            Debug.Log("📘 Collider mje2 ACTIVADO.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnZona = true;
            Debug.Log("Player puede activar las gemas con E.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnZona = false;
        }
    }
}
