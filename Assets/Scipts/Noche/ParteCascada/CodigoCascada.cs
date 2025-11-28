using System.Collections;
using UnityEngine;

public class CodigoCascada : MonoBehaviour
{
    public Animator animFlor;

    public AudioSource sonidoError;
    public AudioSource sonidoFlor;
    public AudioSource sonidoNarracionEstatua;

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

    // ⭐ NUEVO
    private bool narracionReproducida = false;
    private bool narracionTerminada = false;

    private void Start()
    {
        if (PanelEstatuaIluminada != null){

            PanelEstatuaIluminada.SetActive(false);
        }
        if (ParedMuro !=null){
            ParedMuro.SetActive(true);
        }
          if (ParedMensaje !=null)
          {
            ParedMensaje.SetActive(true);
           }
            
    }

    private void Update()
    {
        if (jugadorEnZona && Input.GetKeyDown(KeyCode.E))
        {
            // ⭐ SOLO PUEDE OCULTAR EL PANEL SI LA NARRACIÓN TERMINÓ
            if (narracionTerminada && PanelEstatuaIluminada.activeSelf)
            {
                PanelEstatuaIluminada.SetActive(false);
                return; // no continúa el resto del código
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

        if (animCabeza != null)
            animCabeza.Play("EmissionSi");

        if (animCuerpo != null)
            animCuerpo.Play("EmissionSiCuerpo");

        yield return new WaitForSeconds(1f);

        // ⭐ QUE LA NARRACIÓN SOLO SUENE UNA VEZ
        if (!narracionReproducida && sonidoNarracionEstatua != null)
        {
            PanelEstatuaIluminada.SetActive(true);

            sonidoNarracionEstatua.Play();
            narracionReproducida = true;

            // ⭐ Esperar a que termine la narración
            yield return new WaitForSeconds(sonidoNarracionEstatua.clip.length);

            narracionTerminada = true;
        }
          if (ParedMuro !=null){
            ParedMuro.SetActive(false);
        }
          if (ParedMensaje !=null)
          {
            ParedMensaje.SetActive(false);
           }
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
