using UnityEngine;
using System.Collections;

public class BotonController : MonoBehaviour
{
    [Header("Configuración general")]
    public string nombreBotonCorrecto = "BotonNaranja"; // Tag del botón correcto

    [Header("Referencias externas")]
    public ParticleSystem luciernagas;
    public AudioSource sonidoError;
    public AudioSource sonidoLuciernagas;
    public AudioSource sonidoCorrecto;
    public Animator Gema;
    public GameObject ColiderGema;

    private Animator animator;
    private bool jugadorCerca = false;
    private bool apagandoLuciernagas = false;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (luciernagas != null && !luciernagas.isPlaying)
            luciernagas.Play();

        if (sonidoLuciernagas != null && !sonidoLuciernagas.isPlaying)
            sonidoLuciernagas.Play();
        ColiderGema.SetActive(false);
    }

    private void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            if (animator != null)
                animator.Play("BotonMov");

            // ✅ Compara por TAG
            if (CompareTag(nombreBotonCorrecto))
            {
                if (!apagandoLuciernagas && luciernagas != null)
                    StartCoroutine(ApagarLuciernagasGradualmente(8f));

                if (sonidoLuciernagas != null && sonidoLuciernagas.isPlaying)
                    sonidoLuciernagas.Stop();

                if (sonidoCorrecto != null)
                    sonidoCorrecto.Play();
            }
            else
            {
                if (sonidoError != null)
                    sonidoError.Play();
            }
        }
    }

    private IEnumerator ApagarLuciernagasGradualmente(float duracion)
    {
        apagandoLuciernagas = true;
        var emission = luciernagas.emission;
        float inicioRate = emission.rateOverTime.constant;
        float tiempo = 0f;
        ColiderGema.SetActive(true);
        Gema.Play("Esta");

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracion;
            emission.rateOverTime = Mathf.Lerp(inicioRate, 0, t);
            yield return null;
        }

        luciernagas.Stop();
        apagandoLuciernagas = false;
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







