using UnityEngine;

public class Semilla : MonoBehaviour
{
    public AudioSource sonidoAgarrar; // 🔊 Nuevo campo para el sonido

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PocionManager manager = other.GetComponent<PocionManager>();
            if (manager != null)
            {
                manager.tieneSemilla = true;
                Debug.Log("🌱 Semilla recogida");
            }

            // 🔊 Reproducir sonido antes de destruir el objeto
            if (sonidoAgarrar != null && sonidoAgarrar.clip != null)
                AudioSource.PlayClipAtPoint(sonidoAgarrar.clip, transform.position);

            // 💥 Destruir la semilla
            Destroy(gameObject);
        }
    }
}
