using UnityEngine;

public class Gema : MonoBehaviour
{
    public GameObject objPuntos;
    public AudioSource sonidoAgarrar; // 🔊 Nuevo campo para el sonido

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 🔢 Suma puntos
            objPuntos.GetComponent<ContadorGemas>().puntos += 1;

            // 🔊 Reproducir sonido en la posición de la gema
            if (sonidoAgarrar != null && sonidoAgarrar.clip != null)
                AudioSource.PlayClipAtPoint(sonidoAgarrar.clip, transform.position);

            // 💥 Destruir la gema
            Destroy(gameObject);
        }
    }
}
