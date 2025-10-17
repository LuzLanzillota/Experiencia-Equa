using UnityEngine;
using System.Collections;

public class Llave : MonoBehaviour
{
    public GameObject panelLlave;        // panel que se muestra cuando la llave se recoge
    public bool activaDesdeInicio = true; // si la llave está activa desde el comienzo o no

    private Collider col;
    private MeshRenderer meshRend;

    private void Awake()
    {
        col = GetComponent<Collider>();
        meshRend = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        // Si querés que la llave no sea interactiva hasta que se cierre el panel de inicio,
        // seteá activaDesdeInicio = false en el Inspector.
        if (!activaDesdeInicio)
        {
            SetInteractable(false);
        }
    }

    private void SetInteractable(bool ok)
    {
        if (col != null) col.enabled = ok;
        if (meshRend != null) meshRend.enabled = ok;
    }

    private IEnumerator MostrarPanelYDestruir(GameObject llave)
    {
        if (panelLlave != null)
        {
            Debug.Log("Mostrando panel de la llave: " + panelLlave.name);

            // Oculta visualmente la llave inmediatamente (pero la mantenemos para Destroy al final)
            if (meshRend != null)
                meshRend.enabled = false;

            // Desactivamos el collider para evitar múltiples recogidas
            if (col != null)
                col.enabled = false;

            // Mostramos el panel de la llave
            panelLlave.SetActive(true);

            // Espera el tiempo que quieras ver el panel
            yield return new WaitForSeconds(2f);

            // Ocultamos el panel y destruimos la llave del mundo
            panelLlave.SetActive(false);
            Debug.Log("Panel de llave desactivado. Destruyendo la llave.");
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("No hay panelLlave asignado en el Inspector de " + name);
            Destroy(gameObject);
        }
    }

    // Método público que puede llamar Cupula para "recoger" la llave cuando se cierra el panel de inicio.
    // Se le pasa el jugador por si querés marcar algo en su manager.
    public void RecogerPorCierrePanel(GameObject jugador)
    {
        // Si hay un manager de llaves, lo marcamos
        if (jugador != null)
        {
            LlaveManager manager = jugador.GetComponent<LlaveManager>();
            if (manager != null)
            {
                manager.tieneLlave = true;
            }
        }

        // Iniciamos la rutina para mostrar el panel de la llave y destruirla
        StartCoroutine(MostrarPanelYDestruir(gameObject));
    }

    // Recogida por trigger normal (si el panel de inicio ya terminó)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Si el panel de inicio NO terminó, no permitimos agarrarla desde trigger
            if (!Cupula.panelInicioTerminado)
            {
                Debug.Log("No podés agarrar la llave hasta terminar el panel de inicio.");
                return;
            }

            // marcamos en el manager del jugador y mostramos panel
            LlaveManager manager = other.GetComponent<LlaveManager>();
            if (manager != null)
            {
                manager.tieneLlave = true;
            }
            StartCoroutine(MostrarPanelYDestruir(gameObject));
        }
    }
}
