using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cupula : MonoBehaviour
{
    [Header("Referencias principales")]
    public Animator cupula;
    public GameObject panelInicio;
    public GameObject panelInteraccion; // Empty con collider y el script PanelTrigger
    private bool jugadorDentro = false;
    public AudioSource Narracion;
    private GameObject currentPlayer;
    public static bool panelInicioTerminado = false;

    [Header("Configuraciones")]
    [Tooltip("Radio para buscar llaves cerca del jugador al cerrar el panel")]
    public float radioBusquedaLlaves = 3f;

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
        Debug.Log("Cupula moviéndose...");
        yield return new WaitForSeconds(2f);

        // 🔹 Pedimos al PanelTrigger que oculte el mensaje
        PanelTrigger panelTrigger = panelInteraccion.GetComponent<PanelTrigger>();
        if (panelTrigger != null)
        {
            panelTrigger.OcultarPanel();
            Debug.Log("Panel de interacción ocultado por la cúpula.");
        }

        // 🔹 Desactiva el collider del panelInteraccion (para que no se reactive)
        if (panelInteraccion.TryGetComponent(out Collider col))
        {
            col.enabled = false;
            Debug.Log("Collider del panel de interacción desactivado.");
        }

        // 🔹 Muestra el panel de inicio
        panelInicio.SetActive(true);
        Narracion.Play();
        Debug.Log("Panel de inicio activado.");
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
            Debug.Log("Panel de inicio cerrado. Llave(s) disponibles o se recogerán si están cerca.");
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
