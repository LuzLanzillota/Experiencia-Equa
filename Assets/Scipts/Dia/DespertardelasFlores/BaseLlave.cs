using UnityEngine;
using System.Collections;
using System.Linq;

public class BaseLlave : MonoBehaviour
{
    public Animator Portal;
    public Animator LuzPortalAnimator; //  Nuevo Animator para LuzPortal-2
    public GameObject llaveEnBase; // La llave que se activa y gira
    public Animator animLlaveBase; // Animator de esa llave
    public ParticleSystem particulasPortal; // Sistema de partículas a activar
    public GameObject muroBloqueador; // Muro invisible que bloquea el paso
    public GameObject muroNota;
    public GameObject NotaGema;
    public AudioSource SonidoPortal;
    public AudioSource SonidoLlave;
    public GameObject panelInteraccionLlave;
    private FlorActivador[] floresParaActivar;
    private bool jugadorEnZona = false;
    private bool condicionesCompletas = false; // Bandera para saber si ya se activó todo
    public Animator Gema;
    

    void Start()
    {
        // 🌀 Mezcla aleatoriamente el orden de las flores
        floresParaActivar = FindObjectsOfType<FlorActivador>()
            .OrderBy(f => Random.value)
            .ToArray();

        // Asegurar que el sistema de partículas arranque apagado
        if (particulasPortal != null)
        {
            particulasPortal.Stop();
        }

        // Asegurar que el muro arranque activado (bloqueando el paso)
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
                    Debug.Log("Poniendo la llave en la base");

                    // Mostrar la llave en la base
                    llaveEnBase.SetActive(true);

                    // Reproducir la animación de giro
                    animLlaveBase.Play("GiroLlave");
                    SonidoLlave.Play();
                    Destroy(panelInteraccionLlave);

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

                    // Marcar condiciones como completadas
                    condicionesCompletas = true;

                    // 🔓 Desactivar el muro invisible después de un pequeño delay (para sincronizar con animaciones)
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

        // Activar animación del portal
        Portal.SetTrigger("Abrir");

        //  Activar animación de LuzPortal-2
        if (LuzPortalAnimator != null)
        {
            LuzPortalAnimator.SetTrigger("Abrir");
            Debug.Log("✨ Animación de LuzPortal activada");
        }

        // Reproducir sonido del portal
        if (SonidoPortal != null)
        {
            SonidoPortal.Play();
        }

        // Activar partículas
        if (particulasPortal != null)
        {
            particulasPortal.Play();
        }
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