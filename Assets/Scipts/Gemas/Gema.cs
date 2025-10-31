using System.Collections;
using UnityEngine;

public class Gema : MonoBehaviour
{
    public GameObject objPuntos;
    public GameObject TengoGema; // Asegurate que esté en el Canvas, NO dentro de la gema
    public AudioSource sonidoAgarrar;

    private void Start()
    {
        if (TengoGema != null)
            TengoGema.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objPuntos.GetComponent<ContadorGemas>().puntos += 1;

            if (sonidoAgarrar != null)
                sonidoAgarrar.Play();

            // 🔹 Mostrar cartel manejado desde otro objeto que no se destruye
            if (TengoGema != null)
                StartCoroutine(MostrarMensajeTemporal());

            // 🔹 Destruir la gema después del sonido
            Destroy(gameObject, sonidoAgarrar != null ? sonidoAgarrar.clip.length : 0.1f);
        }
    }

    private IEnumerator MostrarMensajeTemporal()
    {
        TengoGema.SetActive(true);
        yield return new WaitForSeconds(3f);
        TengoGema.SetActive(false);
    }
}


