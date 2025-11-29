using System.Collections;
using UnityEngine;

public class CodigoCascada : MonoBehaviour
{
    public Animator animFlor;

    public AudioSource sonidoError;
    public AudioSource sonidoFlor;
    public AudioSource sonidoNarracionEstatua;

    // ⭐ NUEVO: sonido de la estatua
    public AudioSource sonidoEstatua;

    public GameObject florObj;
    public GameObject PanelEstatuaIluminada;
    public GameObject ParedMuro;
    public GameObject ParedMensaje;

    public Animator animCabeza;
    public Animator animCuerpo;
    public Animator Gema;

    private bool jugadorEnZona = false;
    private LagoManager manager;

    private bool florUsada = false;

    // ⭐ Control narración
    private bool narracionReproducida = false;
    private bool narracionTerminada = false;

    // ⭐ Control sonido estatua
    private bool sonidoEstatuaReproducido = false;

    private void Start()
    {
        if (PanelEstatuaIluminada != null)
            PanelEstatuaIluminada.SetActive(false);

        if (ParedMuro != null)
            ParedMuro.SetActive(true);

        if (ParedMensaje != null)
            ParedMensaje.SetActive(true);
    }

    private void Update()
    {
        if (jugadorEnZona && Input.GetKeyDown(KeyCode.E))
        {
            if (narracionTerminada && PanelEstatuaIluminada.activeSelf)
            {
                PanelEstatuaIluminada.SetActive(false);
                return;
            }

            if (manager != null && manager.tieneFlorDelDia)
            {
                Debug.Log("🌸 Activando animación de la flor");
                StartCoroutine(ActivarFlor());

                manager.tieneFlorDelDia = false;
                florUsada = true;
            }
            else
            {
                if (!florUsada)
                {
                    Debug.Log("❌ No tiene la flor. Sonido de error.");
                    if (sonidoError != null)
                        sonidoError.Play();
                }
            }
        }
    }

    private IEnumerator ActivarFlor()
    {
        if (florObj != null)
            florObj.SetActive(true);

        yield return new WaitForEndOfFrame();

        if (animFlor != null)
            animFlor.SetTrigger("Activar");

        yield return new WaitForSeconds(2f);

        if (sonidoFlor != null)
            sonidoFlor.Play();

        yield return new WaitForSeconds(1.5f);

        // ⭐ Anima la estatua
        if (animCabeza != null)
            animCabeza.Play("EmissionSi");

        if (animCuerpo != null)
            animCuerpo.Play("EmissionSiCuerpo");

        // ⭐ Reproducir sonido de la estatua una sola vez
        if (!sonidoEstatuaReproducido && sonidoEstatua != null)
        {
            sonidoEstatua.Play();
            sonidoEstatuaReproducido = true;
        }

        yield return new WaitForSeconds(1f);

        // ⭐ NARRACIÓN ESTATUA
        if (!narracionReproducida && sonidoNarracionEstatua != null)
        {
            PanelEstatuaIluminada.SetActive(true);

            sonidoNarracionEstatua.Play();
            narracionReproducida = true;

            yield return new WaitForSeconds(sonidoNarracionEstatua.clip.length);

            narracionTerminada = true;
        }

        if (ParedMuro != null)
            ParedMuro.SetActive(false);

        if (ParedMensaje != null)
            ParedMensaje.SetActive(false);

        Gema.Play("EstaCeleste");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnZona = true;
            manager = other.GetComponent<LagoManager>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnZona = false;
            manager = null;
        }
    }
}

