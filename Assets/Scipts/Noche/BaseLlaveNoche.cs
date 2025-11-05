using UnityEngine;
using System.Collections;

public class BaseLlaveNoche : MonoBehaviour
{
    public GameObject llaveEnBase; // La llave que se activa y gira
    public Animator animLlaveBase; // Animator de esa llave
    public AudioSource SonidoLlave;
    public GameObject panelInteraccionLlave;

    public ParticleSystem niebla; // 🌫️ Nuevo: sistema de partículas de niebla
    public Animator Gema;
    public GameObject muroBloqueador;
    public GameObject muroNota;
    public GameObject NotaGema;

    private bool jugadorEnZona = false;
    private bool condicionesCompletas = false;

    void Start()
    {
        if (muroBloqueador != null)
        {
            NotaGema.SetActive(false);
            muroNota.SetActive(true);
            muroBloqueador.SetActive(true);
            panelInteraccionLlave.SetActive(false);
        }
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
                    Debug.Log("🔑 Colocando la llave en la base...");

                    // Mostrar la llave en la base
                    llaveEnBase.SetActive(true);

                    // Reproducir la animación de giro y sonido
                    animLlaveBase.Play("GiroLlave");
                    SonidoLlave.Play();
                    Destroy(panelInteraccionLlave);

                    // ⏳ Iniciar la secuencia de desactivar la niebla
                    StartCoroutine(DetenerNieblaConDelay(3f));

                    // Quitar la llave del inventario
                    manager.tieneLlave = false;
                    condicionesCompletas = true;

                    // 🔓 Desactivar muro después de un pequeño delay
                    StartCoroutine(DesactivarMuroConDelay(5f));
                }
                else
                {
                    Debug.Log("Necesitás la llave para activar la base.");
                }
            }
        }
    }

    IEnumerator DetenerNieblaConDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (niebla != null)
        {
            niebla.Stop();
            Debug.Log("🌫️ Niebla detenida.");
        }
    }

    IEnumerator DesactivarMuroConDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (condicionesCompletas && muroBloqueador != null)
        {
            muroBloqueador.SetActive(false);
            muroNota.SetActive(false);
            NotaGema.SetActive(true);
            Gema.Play("Esta");
            Debug.Log("✅ Muro desactivado, podés avanzar.");
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

