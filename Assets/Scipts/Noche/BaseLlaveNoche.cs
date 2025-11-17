using UnityEngine;
using System.Collections;
using System.Linq;

public class BaseLlaveNoche : MonoBehaviour
{
    // 🎬 Animaciones y efectos
    public Animator Portal;
    public Animator LuzPortalAnimator; // 🔥 Animator de la luz del portal
    public Animator AreaLuzPortal;     // ⭐ NUEVA animación para AreaLuzPortal
    public Animator animLlaveBase;
    public Animator Gema;

    // 💨 Partículas
    public ParticleSystem niebla;
    public ParticleSystem particulasPortal;

    // 🎮 Objetos y referencias
    public GameObject llaveEnBase;
    public GameObject muroBloqueador;
    public GameObject muroNota;
    public GameObject NotaGema;
    public GameObject panelInteraccionLlave;
    public GameObject ColiderGema;

    // 🔊 Sonidos
    public AudioSource SonidoPortal;
    public AudioSource SonidoLlave;
    public AudioSource SonidoViento;

    // ⚙️ Estados internos
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
            ColiderGema.SetActive(false);
        }

        if (particulasPortal != null)
            particulasPortal.Stop();

        if (Portal != null)
            Portal.gameObject.SetActive(false);

        if (AreaLuzPortal != null)
            AreaLuzPortal.gameObject.SetActive(false); // Importante
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

                    llaveEnBase.SetActive(true);

                    animLlaveBase.Play("GiroLlave");
                    SonidoLlave.Play();
                    Destroy(panelInteraccionLlave);

                    StartCoroutine(AbrirPortalConDelay());
                    StartCoroutine(DetenerNieblaConDelay(3f));
                    StartCoroutine(DesactivarMuroConDelay(5f));

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

        // ➤ ACTIVAR PORTAL
        if (Portal != null)
        {
            Portal.gameObject.SetActive(true);
            Portal.SetTrigger("Abrir");
        }

        // ➤ ACTIVAR LUZ DEL PORTAL
        if (LuzPortalAnimator != null)
        {
            LuzPortalAnimator.gameObject.SetActive(true);
            LuzPortalAnimator.SetTrigger("Abrir");
        }

        // ⭐ ➤ ACTIVAR AreaLuzPortal (Nueva animación)
        if (AreaLuzPortal != null)
        {
            AreaLuzPortal.gameObject.SetActive(true);
            AreaLuzPortal.SetTrigger("Abrir");
            Debug.Log("✨ Animación AreaLuzPortal activada");
        }

        // ➤ PARTÍCULAS
        if (particulasPortal != null)
        {
            particulasPortal.Play();
        }

        // ➤ SONIDO
        if (SonidoPortal != null)
        {
            SonidoPortal.Play();
        }
    }

    IEnumerator DetenerNieblaConDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (SonidoViento != null)
            SonidoViento.Play();

        if (niebla != null)
            niebla.Stop();
    }

    IEnumerator DesactivarMuroConDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (condicionesCompletas && muroBloqueador != null)
        {
            muroBloqueador.SetActive(false);
            muroNota.SetActive(false);
            NotaGema.SetActive(true);
            ColiderGema.SetActive(true);
            Gema.Play("Esta");
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
