using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cupula : MonoBehaviour
{
    [Header("Referencias principales")]
    public Animator cupula;
    public GameObject panelInicio;
    public GameObject panelInteraccion;
    private bool jugadorDentro = false;
    public AudioSource Narracion;
    private GameObject currentPlayer;
    public static bool panelInicioTerminado = false;

    [Header("Configuraciones")]
    public float radioBusquedaLlaves = 3f;

    // 🔊 NUEVO -> bandera para reproducir solo una vez
    private bool audioReproducido = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            jugadorDentro = true;
            panelInteraccion.SetActive(true);
            currentPlayer = other.gameObject;
            StartCoroutine(MostrarPanelConRetraso());
        }
    }

    private IEnumerator MostrarPanelConRetraso()
    {
        cupula.Play("CupulaArriba");
        yield return new WaitForSeconds(2f);

        PanelTrigger panelTrigger = panelInteraccion.GetComponent<PanelTrigger>();
        if (panelTrigger != null)
            panelTrigger.OcultarPanel();

        if (panelInteraccion.TryGetComponent(out Collider col))
            col.enabled = false;

        panelInicio.SetActive(true);

        // 🔊 SOLO REPRODUCE EL AUDIO UNA VEZ
        if (!audioReproducido)
        {
            Narracion.Play();
            audioReproducido = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            jugadorDentro = false;
            currentPlayer = null;
            panelInicio.SetActive(false);
        }
    }

    private void Update()
    {
        if (jugadorDentro && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(panelInicio);
            panelInicioTerminado = true;
            TryCollectNearbyLlaves();
        }
    }

    private void TryCollectNearbyLlaves()
    {
        if (currentPlayer == null) return;

        Collider[] hits = Physics.OverlapSphere(currentPlayer.transform.position, radioBusquedaLlaves);
        foreach (Collider c in hits)
        {
            Llave llave = c.GetComponent<Llave>();
            if (llave != null)
            {
                llave.RecogerPorCierrePanel(currentPlayer);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (currentPlayer != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(currentPlayer.transform.position, radioBusquedaLlaves);
        }
    }
}

