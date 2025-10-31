using System.Collections;
using UnityEngine;

public class GemaPrincipio : MonoBehaviour
{
    public GameObject objPuntos;
    public AudioSource sonidoAgarrar; // 🔊 Sonido al recoger
    public GameObject textoGema;

    private void Start()
    {
        textoGema.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 🔢 Sumar puntos
            objPuntos.GetComponent<ContadorGemas>().puntos += 1;

            // 🔊 Reproducir sonido en la posición de la gema
            if (sonidoAgarrar != null && sonidoAgarrar.clip != null)
                AudioSource.PlayClipAtPoint(sonidoAgarrar.clip, transform.position);

            // ▶️ Iniciar corrutina para mostrar texto temporalmente
            StartCoroutine(MostrarTextoYDestruir());
        }
    }

    private IEnumerator MostrarTextoYDestruir()
    {
        // Mostrar texto
        textoGema.SetActive(true);

        // Esperar 5 segundos
        yield return new WaitForSeconds(3f);

        // Ocultar texto
        Destroy(textoGema);

        // Destruir la gema
        Destroy(gameObject);
    }
}
