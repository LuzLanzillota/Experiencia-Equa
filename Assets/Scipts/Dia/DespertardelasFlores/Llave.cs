using UnityEngine;
using System.Collections;

public class Llave : MonoBehaviour
{
    public GameObject panelLlave;         // Panel que se muestra cuando la llave se recoge
    public bool activaDesdeInicio = true; // Si la llave está activa desde el comienzo o no
    public AudioSource sonidoAgarrar;     // 🔊 Nuevo campo para el sonido de recoger la llave

    private Collider col;
    private MeshRenderer meshRend;

    private void Awake()
    {
        col = GetComponent<Collider>();
        meshRend = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        if (!activaDesdeInicio)
            SetInteractable(false);
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

            // 🔊 Reproducir sonido de recoger la llave
            if (sonidoAgarrar != null && sonidoAgarrar.clip != null)
                AudioSource.PlayClipAtPoint(sonidoAgarrar.clip, transform.position);

            // Ocultar visualmente la llave y desactivar interacción
            if (meshRend != null)
                meshRend.enabled = false;

            if (col != null)
                col.enabled = false;

            // Mostrar el panel
            panelLlave.SetActive(true);

            yield return new WaitForSeconds(2f);

            // Ocultar panel y destruir la llave
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

    // 🔸 Recogida por cierre de panel
    public void RecogerPorCierrePanel(GameObject jugador)
    {
        if (jugador != null)
        {
            LlaveManager manager = jugador.GetComponent<LlaveManager>();
            if (manager != null)
            {
                manager.tieneLlave = true;
            }
        }

        StartCoroutine(MostrarPanelYDestruir(gameObject));
    }

    // 🔸 Recogida por trigger normal
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!Cupula.panelInicioTerminado)
            {
                Debug.Log("No podés agarrar la llave hasta terminar el panel de inicio.");
                return;
            }

            LlaveManager manager = other.GetComponent<LlaveManager>();
            if (manager != null)
            {
                manager.tieneLlave = true;
            }

            StartCoroutine(MostrarPanelYDestruir(gameObject));
        }
    }
}
