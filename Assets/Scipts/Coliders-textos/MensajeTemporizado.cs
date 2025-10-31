using System.Collections;
using UnityEngine;

public class MensajeTemporizado : MonoBehaviour
{
    public GameObject panel;          // Panel o texto que aparece
    public float tiempoVisible = 3f;  // Tiempo que dura visible

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (panel != null)
            {
                panel.SetActive(true);
                StartCoroutine(DestruirDespuesDeTiempo());
            }
        }
    }

    private IEnumerator DestruirDespuesDeTiempo()
    {
        yield return new WaitForSeconds(tiempoVisible);
        Destroy(panel); // 💥 Destruye el mensaje
    }
}
