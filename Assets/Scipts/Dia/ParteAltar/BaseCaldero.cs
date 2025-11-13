using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BaseCaldero : MonoBehaviour
{
    [Header("Animaciones principales")]
    public Animator animSemilla;
    public Animator animHongo;
    public Animator animAgua;
    public Animator animAgua2;
    public Animator animAgua3;

    [Header("Hongos grandes")]
    public Animator animHongosGrandes;
    public Animator animHongosGrandes2;
    public Animator animHongosGrandes3;
    public Animator animHongosGrandes4;
    public Animator animHongosGrandes5;
    public Animator animHongosGrandes6;
    public Animator animHongosGrandes7;
    public Animator animHongosGrandes8;
    public Animator animHongosGrandes9;
    public Animator animHongosGrandes10;
    public Animator animHongosGrandes11;
    public Animator animHongosGrandes12;
    public Animator animHongosGrandes13;
    public Animator animHongosGrandes14;
    public Animator animHongosGrandes15;
    public Animator animHongosGrandes16;
    public Animator animHongosGrandes17;
    public Animator animHongosGrandes18;
    public Animator animHongosGrandes19;
    public Animator animHongosGrandes20;
    public Animator animHongosGrandes21;
    public Animator animHongosGrandes22;
    public Animator animHongosGrandes23;
    public Animator animHongosGrandes24;
    public Animator animHongosGrandes25;
    public Animator animHongosGrandes26;
    public Animator animHongosGrandes27;
    public Animator animHongosGrandes28;

    [Header("Audio")]
    public AudioSource HongosCrecen;
    public AudioSource Agua;
    public AudioSource SonidoError; // ❌ Nuevo sonido de error
    public AudioSource Splash;      // 💦 Nuevo sonido de splash

    [Header("Referencias externas")]
    public GameObject paredHongos;
    public GameObject mensajeParedHongo;
    public Animator Gema;
    public GameObject ColiderGema;

    private bool jugadorEnZona = false;

    private void Start()
    {
        paredHongos.SetActive(true);
        mensajeParedHongo.SetActive(true);
        ColiderGema.SetActive(false);
    }

    void Update()
    {
        if (jugadorEnZona && Input.GetKeyDown(KeyCode.E))
        {
            GameObject jugador = GameObject.FindWithTag("Player");
            if (jugador != null)
            {
                PocionManager manager = jugador.GetComponent<PocionManager>();

                if (manager != null && manager.tieneSemilla && manager.tieneHongo)
                {
                    Debug.Log("🧪 Iniciando creación de poción");
                    StartCoroutine(CrearPocion());

                    manager.tieneSemilla = false;
                    manager.tieneHongo = false;
                }
                else
                {
                    Debug.Log("⚠ Falta algún ingrediente");

                    // 🔊 Reproduce sonido de error cuando falta uno o ambos objetos
                    if (SonidoError != null)
                        SonidoError.Play();
                }
            }
        }
    }

    IEnumerator CrearPocion()
    {
        // 🌱 Animación de la semilla
        if (animSemilla != null)
        {
            animSemilla.gameObject.SetActive(true);
            yield return new WaitForEndOfFrame();
            animSemilla.SetTrigger("Caer");
        }

        // 🍄 Animación del hongo
        if (animHongo != null)
        {
            animHongo.gameObject.SetActive(true);
            yield return new WaitForEndOfFrame();
            animHongo.SetTrigger("Caer");

            // 💦 Sonido de splash cuando ambos caen
            if (Splash != null)
                Splash.Play();

            Destroy(paredHongos);
            Destroy(mensajeParedHongo);
        }

        yield return new WaitForSeconds(1.5f);

        // 💧 Animación del agua 1
        if (animAgua != null)
        {
            animAgua.gameObject.SetActive(true);
            yield return new WaitForEndOfFrame();
            animAgua.SetTrigger("Subir");
            if (Agua != null)
                Agua.Play();
        }

        // 💧 Animación del agua 2
        if (animAgua2 != null)
        {
            animAgua2.gameObject.SetActive(true);
            yield return new WaitForEndOfFrame();
            animAgua2.SetTrigger("Subir");
        }

        // 💧 Animación del agua 3
        if (animAgua3 != null)
        {
            animAgua3.gameObject.SetActive(true);
            yield return new WaitForEndOfFrame();
            animAgua3.SetTrigger("Subir");
        }

        yield return new WaitForSeconds(3f);

        // 🍄 Crecimiento de los hongos
        if (animHongosGrandes != null)
        {
            animHongosGrandes.Play("Crece");
            animHongosGrandes2.Play("Crece");
            animHongosGrandes3.Play("Crece");
            animHongosGrandes4.Play("Crece");
            animHongosGrandes5.Play("Crece");
            animHongosGrandes6.Play("Crece");
            animHongosGrandes7.Play("Crece");
            animHongosGrandes8.Play("Crece");
            animHongosGrandes9.Play("Crece");
            animHongosGrandes10.Play("Crece");
            animHongosGrandes11.Play("Crece");
            animHongosGrandes12.Play("Crece");
            animHongosGrandes13.Play("Crece");
            animHongosGrandes14.Play("Crece");
            animHongosGrandes15.Play("Crece");
            animHongosGrandes16.Play("Crece");
            animHongosGrandes17.Play("Crece");
            animHongosGrandes18.Play("Crece");
            animHongosGrandes19.Play("Crece");
            animHongosGrandes20.Play("Crece");
            animHongosGrandes21.Play("Crece");
            animHongosGrandes22.Play("Crece");
            animHongosGrandes23.Play("Crece");
            animHongosGrandes24.Play("Crece");
            animHongosGrandes25.Play("Crece");
            animHongosGrandes26.Play("Crece");
            animHongosGrandes27.Play("Crece");
            animHongosGrandes28.Play("Crece");

            if (HongosCrecen != null)
                HongosCrecen.Play();
        }

        // 💎 Activar la animación de la gema al final
        if (Gema != null)
        {
            ColiderGema.SetActive(true);
            Gema.Play("Esta");
        }

        Debug.Log("✨ Pocion completada");
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
