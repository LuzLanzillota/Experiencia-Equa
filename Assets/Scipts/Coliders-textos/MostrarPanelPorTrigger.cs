using UnityEngine;

public class MostrarPanelPorTrigger : MonoBehaviour
{
    public GameObject panel; // Asigná tu panel en el Inspector
    private bool jugadorDentro = false;

    private void Start()
    {
        // Aseguramos que el panel esté oculto al inicio
        if (panel != null)
            panel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = true;
            if (panel != null)
                panel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = false;
            if (panel != null)
                panel.SetActive(false);
        }
    }

    private void Update()
    {
        if (jugadorDentro && Input.GetKeyDown(KeyCode.E))
        {
            if (panel != null)
                panel.SetActive(false);
        }
    }
}
