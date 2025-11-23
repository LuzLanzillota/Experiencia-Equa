using UnityEngine;

public class FlorDelDia : MonoBehaviour
{
    public AudioSource sonidoAgarrar; // 🔊 Nuevo campo para el sonido

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LagoManager manager = other.GetComponent<LagoManager>();
            if (manager != null)
            {
                manager.tieneFlorDelDia = true;
                Debug.Log("🌼 Flor del Día recogida");
            }

            // 🔊 Reproducir sonido antes de destruir el objeto
            if (sonidoAgarrar != null && sonidoAgarrar.clip != null)
                AudioSource.PlayClipAtPoint(sonidoAgarrar.clip, transform.position);

            // 💥 Destruir la flor
            Destroy(gameObject);
        }
    }
}
