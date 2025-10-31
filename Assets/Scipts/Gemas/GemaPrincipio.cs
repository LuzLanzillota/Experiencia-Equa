using System.Collections;
using UnityEngine;

public class GemaPrincipio : MonoBehaviour
{
    public GameObject objPuntos;       // 📊 Contador de gemas
    public AudioSource sonidoAgarrar;  // 🔊 Sonido al recoger
    public GameObject textoGema;       // 💬 Texto al recoger

    private void Start()
    {
        if (textoGema != null)
            textoGema.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 🔢 Sumar puntos
            objPuntos.GetComponent<ContadorGemas>().puntos += 1;

            // 🔊 Reproducir sonido
            if (sonidoAgarrar != null && sonidoAgarrar.clip != null)
                AudioSource.PlayClipAtPoint(sonidoAgarrar.clip, transform.position);

            // 💬 Mostrar texto breve
            if (textoGema != null)
                StartCoroutine(MostrarTexto());

            // 💥 Destruir la gema inmediatamente
            Destroy(gameObject);
        }
    }

    private IEnumerator MostrarTexto()
    {
        textoGema.SetActive(true);
        yield return new WaitForSeconds(2f);
        textoGema.SetActive(false);
    }
}
