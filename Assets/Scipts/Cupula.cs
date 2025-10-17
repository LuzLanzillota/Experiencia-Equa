using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cupula : MonoBehaviour
{
    public Animator cupula;
    public GameObject panelInicio;
    public GameObject panelInteraccion;
    private bool jugadorDentro = false;
    private GameObject currentPlayer;
    public static bool panelInicioTerminado = false;

    // radio en el que buscamos llaves alrededor del jugador al cerrar el panel
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
        panelInicio.SetActive(true);
        Destroy(panelInteraccion);
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
            // Cerrás el panel de inicio
            panelInicio.SetActive(false);
            panelInicioTerminado = true;
            Debug.Log("Panel de inicio cerrado. Llave(s) disponibles o se recogerán si están cerca.");

            // Intentamos recoger llaves cercanas (si hay)
            TryCollectNearbyLlaves();
        }
    }

    private void TryCollectNearbyLlaves()
    {
        if (currentPlayer == null) return;

        // Buscamos colliders alrededor del jugador (ajustá radioBusquedaLlaves en el Inspector)
        Collider[] hits = Physics.OverlapSphere(currentPlayer.transform.position, radioBusquedaLlaves);
        foreach (Collider c in hits)
        {
            Llave llave = c.GetComponent<Llave>();
            if (llave != null)
            {
                // Llamamos al método público de la llave para que muestre su panel y se destruya
                llave.RecogerPorCierrePanel(currentPlayer);
            }
        }
    }

    // Visualización del gizmo del radio en el editor (opcional)
    private void OnDrawGizmosSelected()
    {
        if (currentPlayer != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(currentPlayer.transform.position, radioBusquedaLlaves);
        }
    }
}
