using System.Collections;
using UnityEngine;

public class CodigoCascada : MonoBehaviour
{
    public Animator animFlor;
    public GameObject florObj;
    public AudioSource sonidoError;
    public AudioSource sonidoFlor;      // ⭐ NUEVO: sonido que va con la animación

    private bool jugadorEnZona = false;
    private LagoManager manager;

    private bool florUsada = false; // evita que el sonido de error se repita

    private void Update()
    {
        if (jugadorEnZona && Input.GetKeyDown(KeyCode.E))
        {
            if (manager != null && manager.tieneFlorDelDia)
            {
                Debug.Log("🌸 Activando animación de la flor");
                StartCoroutine(ActivarFlor());

                manager.tieneFlorDelDia = false;
                florUsada = true;
            }
            else
            {
                if (!florUsada)
                {
                    Debug.Log("❌ No tiene la flor. Sonido de error.");
                    if (sonidoError != null)
                        sonidoError.Play();
                }
            }
        }
    }

    private IEnumerator ActivarFlor()
    {
        if (florObj != null)
            florObj.SetActive(true);

        yield return new WaitForEndOfFrame();

        if (animFlor != null)
            animFlor.SetTrigger("Activar");

        // ⭐ Espera 2.5 segundos y luego reproduce el sonido de la flor
        yield return new WaitForSeconds(2f);

        if (sonidoFlor != null)
            sonidoFlor.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnZona = true;
            manager = other.GetComponent<LagoManager>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnZona = false;
            manager = null;
        }
    }
}
