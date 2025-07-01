using UnityEngine;
using System.Collections;

public class BaseLlave : MonoBehaviour
{
    public Animator Portal;
    public GameObject llaveEnBase; // La llave que se activa y gira
    public Animator animLlaveBase; // Animator de esa llave

    private FlorActivador[] floresParaActivar; // Ahora es privado
    private bool jugadorEnZona = false;

    void Start()
    {
        // 🧠 Busca automáticamente todas las flores que tengan el script FlorActivador
        floresParaActivar = FindObjectsOfType<FlorActivador>();
    }

    void Update()
    {
        if (jugadorEnZona && Input.GetKeyDown(KeyCode.E))
        {
            GameObject jugador = GameObject.FindWithTag("Player");
            if (jugador != null)
            {
                LlaveManager manager = jugador.GetComponent<LlaveManager>();
                if (manager != null && manager.tieneLlave)
                {
                    Debug.Log("Poniendo la llave en la base");

                    // Mostrar la llave en la base
                    llaveEnBase.SetActive(true);

                    // Reproducir la animación de giro
                    animLlaveBase.Play("GiroLlave");

                    // Iniciar delay para abrir el portal
                    StartCoroutine(AbrirPortalConDelay());

                    // Activar flores con delay
                    StartCoroutine(ActivarFloresConDelay());

                    // Destruir la llave que estaba en el mundo (si aún existe)
                    GameObject llave = GameObject.FindWithTag("Llave");
                    if (llave != null)
                    {
                        Destroy(llave);
                    }

                    // Quitar la llave del inventario
                    manager.tieneLlave = false;
                }
                else
                {
                    Debug.Log("Necesitás la llave para activar la base.");
                }
            }
        }
    }

    IEnumerator AbrirPortalConDelay()
    {
        yield return new WaitForSeconds(3f);
        Portal.SetTrigger("Abrir");
    }

    IEnumerator ActivarFloresConDelay()
    {
        yield return new WaitForSeconds(5f);

        foreach (FlorActivador flor in floresParaActivar)
        {
            flor.ActivarFlor();
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnZona = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnZona = false;
        }
    }
}