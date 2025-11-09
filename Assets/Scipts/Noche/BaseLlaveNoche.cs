using UnityEngine;
using System.Collections;
using System.Linq;

public class BaseLlaveNoche : MonoBehaviour
{
    // 🎬 Animaciones y efectos
    public Animator Portal;
    public Animator LuzPortalAnimator; // Nuevo Animator para LuzPortal-2
    public Animator animLlaveBase; // Animator de la llave en la base
    public Animator Gema;

    // 💨 Partículas
    public ParticleSystem niebla; // 🌫️ Niebla de la versión nocturna
    public ParticleSystem particulasPortal; // ✨ NUEVO: partículas del portal

    // 🎮 Objetos y referencias
    public GameObject llaveEnBase;
    public GameObject muroBloqueador;
    public GameObject muroNota;
    public GameObject NotaGema;
    public GameObject panelInteraccionLlave;

    // 🔊 Sonidos
    public AudioSource SonidoPortal;
    public AudioSource SonidoLlave;

    // ⚙️ Estados internos
    private bool jugadorEnZona = false;
    private bool condicionesCompletas = false;

    void Start()
    {
        // Asegurar que los objetos empiecen en el estado correcto
        if (muroBloqueador != null)
        {
            NotaGema.SetActive(false);
            muroNota.SetActive(true);
            muroBloqueador.SetActive(true);
            panelInteraccionLlave.SetActive(false);
        }

        if (particulasPortal != null)
            particulasPortal.Stop();

        // Asegurar que el portal no esté activo al comienzo
        if (Portal != null)
            Portal.gameObject.SetActive(false);
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

                    // Animación y sonido de la llave
                    animLlaveBase.Play("GiroLlave");
                    SonidoLlave.Play();
                    Destroy(panelInteraccionLlave);

                    // Iniciar las secuencias visuales
                    StartCoroutine(AbrirPortalConDelay());
                    StartCoroutine(DetenerNieblaConDelay(3f));
                    StartCoroutine(DesactivarMuroConDelay(5f));

                    // Quitar la llave del inventario
                    manager.tieneLlave = false;
                    condicionesCompletas = true;
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

        // Activar portal y luz
        if (Portal != null)
        {
            Portal.gameObject.SetActive(true);
            Portal.SetTrigger("Abrir");
        }

        if (LuzPortalAnimator != null)
        {
            LuzPortalAnimator.SetTrigger("Abrir");
            Debug.Log("✨ Animación de LuzPortal activada");
        }

        // ✨ Activar partículas del portal
        if (particulasPortal != null)
        {
            particulasPortal.Play();
            Debug.Log("💫 Partículas del portal activadas");
        }

        // Reproducir sonido del portal
        if (SonidoPortal != null)
        {
            SonidoPortal.Play();
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
            jugadorEnZona = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            jugadorEnZona = false;
    }
}
