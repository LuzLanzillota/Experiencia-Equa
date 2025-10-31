using UnityEngine;

public class GemaColores : MonoBehaviour
{
    public GameObject panel; // 🔹 Asigná acá el panel que querés mostrar

    private void Start()
    {
        // 🔹 El panel comienza oculto
        if (panel != null)
            panel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 🔹 Si el jugador toca la gema...
        if (other.CompareTag("Player"))
        {
            // ...se muestra el panel
            if (panel != null)
                panel.SetActive(true);

            // 🔹 Opcional: destruir la gema después de mostrar el panel
            Destroy(gameObject);
        }
    }
}
