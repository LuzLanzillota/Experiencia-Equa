using UnityEngine;
using System.Collections;
using System.Linq;

public class BaseLlave : MonoBehaviour
{
    public Animator Portal;
    public Animator LuzPortalAnimator; // Nuevo Animator para LuzPortal-2
    public Animator AreaLuzPortal;     // ⭐ NUEVA animación AreaLuzPortal

    public GameObject llaveEnBase;
    public Animator animLlaveBase;
    public ParticleSystem particulasPortal;
    public GameObject muroBloqueador;
    public GameObject muroNota;
    public GameObject NotaGema;
    public AudioSource SonidoPortal;
    public AudioSource SonidoLlave;
    public GameObject panelInteraccionLlave;
    private FlorActivador[] floresParaActivar;
    private bool jugadorEnZona = false;
    private bool condicionesCompletas = false;
    public Animator Gema;

    void Start()
    {
        // Mezcla aleatoria de flores
        floresParaActivar = FindObjectsOfType<FlorActivador>()
            .OrderBy(f => Random.value)
            .ToArray();

        if (particulasPortal != null)
            particulasPortal.Stop();

        if (muroBloqueador != null)
        {
            NotaGema.SetActive(false);
            muroNota.SetActive(true);
            muroBloqueador.SetActive(true);
            panelInteraccionLlave.SetActive(false);
        }

        // Asegurar que AreaLuzPortal arranque apagada
        if (AreaLuzPortal != null)
            AreaLuzPortal.gameObject.SetActive(false);
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

                    llaveEnBase.SetActive(true);
                    animLlaveBase.Play("GiroLlave");
                    SonidoLlave.Play();
                    Destroy(panelInteraccionLlave);

                    StartCoroutine(AbrirPortalConDelay());
                    StartCoroutine(ActivarFloresConDelay());

                    GameObject llave = GameObject.FindWithTag("Llave");
                    if (llave != null)
                        Destroy(llave);

                    manager.tieneLlave = false;
                    condicionesCompletas = true;

                    StartCoroutine(DesactivarMuroConDelay(5f));
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

        // Activar portal
        Portal.SetTrigger("Abrir");

        // Activar LuzPortalAnimator
        if (LuzPortalAnimator != null)
        {
            LuzPortalAnimator.SetTrigger("Abrir");
            Debug.Log("✨ Animación LuzPortal-2 activada");
        }

        // ⭐ Activar AreaLuzPortal (NUEVO)
        if (AreaLuzPortal != null)
        {
            AreaLuzPortal.gameObject.SetActive(true);
            AreaLuzPortal.SetTrigger("Abrir");
            Debug.Log("🌟 Animación AreaLuzPortal activada");
        }

        // Sonido portal
        if (SonidoPortal != null)
            SonidoPortal.Play();

        // Partículas
        if (particulasPortal != null)
            particulasPortal.Play();
    }

    IEnumerator ActivarFloresConDelay()
    {
        yield return new WaitForSeconds(4f);

        foreach (FlorActivador flor in floresParaActivar)
        {
            flor.ActivarFlor();
            yield return new WaitForSeconds(0.05f);
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
