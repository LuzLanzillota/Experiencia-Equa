using UnityEngine;

public class HongoMuerto : MonoBehaviour
{
    public AudioSource sonidoAgarrar; // 🔊 Nuevo campo para el sonido

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PocionManager manager = other.GetComponent<PocionManager>();
            if (manager != null)
            {
                manager.tieneHongo = true;
                Debug.Log("🍄 Hongo muerto recogido");
            }

            // 🔊 Reproducir sonido antes de destruir el objeto
            if (sonidoAgarrar != null && sonidoAgarrar.clip != null)
                AudioSource.PlayClipAtPoint(sonidoAgarrar.clip, transform.position);

            // 💥 Destruir el hongo
            Destroy(gameObject);
        }
    }
}
