using System.Collections;
using UnityEngine;

public class FlorDelDia : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource sonidoAgarrar;
    public AudioSource sonidoPiedras;   // ⭐ Sonido que se reproduce con la secuencia

    [Header("Secuencia de piedras")]
    public Animator[] piedras;
    public string[] nombreAnimaciones;
    public float delayDespuesDeFlor = 2f;
    public float delayEntrePiedras = 1.5f;

    private bool recogida = false;
    private bool sonidoPiedrasReproducido = false; // ⭐ Para asegurar que suene UNA SOLA VEZ

    void OnTriggerEnter(Collider other)
    {
        if (recogida) return;

        if (other.CompareTag("Player"))
        {
            LagoManager manager = other.GetComponent<LagoManager>();
            if (manager != null)
            {
                manager.tieneFlorDelDia = true;
                Debug.Log("🌼 Flor del Día recogida");
            }

            if (sonidoAgarrar != null && sonidoAgarrar.clip != null)
                AudioSource.PlayClipAtPoint(sonidoAgarrar.clip, transform.position);

            recogida = true;
            OcultarFlorVisualmente();

            StartCoroutine(SecuenciaPiedrasYDestruir());
        }
    }

    private void OcultarFlorVisualmente()
    {
        foreach (var r in GetComponentsInChildren<Renderer>())
            r.enabled = false;

        foreach (var c in GetComponentsInChildren<Collider>())
            c.enabled = false;
    }

    private IEnumerator SecuenciaPiedrasYDestruir()
    {
        yield return new WaitForSeconds(delayDespuesDeFlor);

        // ⭐ Reproducir sonido una sola vez
        if (!sonidoPiedrasReproducido && sonidoPiedras != null)
        {
            sonidoPiedras.Play();
            sonidoPiedrasReproducido = true;
        }

        // Animar piedras una por una
        for (int i = 0; i < piedras.Length; i++)
        {
            Animator anim = piedras[i];

            if (anim != null)
            {
                string nombreAnim = nombreAnimaciones.Length > i ? nombreAnimaciones[i] : "";

                if (!string.IsNullOrEmpty(nombreAnim))
                {
                    anim.Play(nombreAnim, 0, 0f);
                }
                else
                {
                    Debug.LogWarning("⚠️ No se encontró nombre de animación para la piedra #" + i);
                }
            }

            yield return new WaitForSeconds(delayEntrePiedras);
        }

        Destroy(gameObject);
    }
}
