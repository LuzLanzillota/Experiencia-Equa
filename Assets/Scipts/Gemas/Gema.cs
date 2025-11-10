using System.Collections;
using UnityEngine;

public class Gema : MonoBehaviour
{
    public GameObject objPuntos;
    public GameObject TengoGema;
    public AudioSource sonidoAgarrar;
    public GameObject PanelGema;
    public AudioSource panelAudio; // 👈 Ahora visible en el Inspector
    public Animator animGema; // 👈 Asigná el Animator de la gema
    public string nombreAnimGema = "GemaActiva"; // 👈 nombre del clip que se reproduce

    private Collider colGema;

    private void Start()
    {
        colGema = GetComponent<Collider>();
        colGema.enabled = false; // 🔹 no se puede agarrar aún

        if (TengoGema != null)
            TengoGema.SetActive(false);

        if (PanelGema != null)
        {
            PanelGema.SetActive(false);

            // Si no se asignó manualmente, lo busca automáticamente
            if (panelAudio == null)
                panelAudio = PanelGema.GetComponent<AudioSource>();
        }

        if (animGema != null)
            StartCoroutine(ActivarColliderDespuesAnim());
        else
            Debug.LogWarning("⚠️ Falta asignar Animator de la gema.");
    }

    private IEnumerator ActivarColliderDespuesAnim()
    {
        yield return new WaitForSeconds(GetDuracionAnim(animGema, nombreAnimGema));
        colGema.enabled = true;
    }

    private float GetDuracionAnim(Animator anim, string nombreClip)
    {
        foreach (AnimationClip clip in anim.runtimeAnimatorController.animationClips)
        {
            if (clip.name == nombreClip)
                return clip.length;
        }
        return 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (colGema.enabled && other.CompareTag("Player"))
        {
            objPuntos.GetComponent<ContadorGemas>().puntos += 1;

            if (sonidoAgarrar != null)
                sonidoAgarrar.Play();

            if (TengoGema != null)
                StartCoroutine(MostrarMensajeTemporal());

            Destroy(gameObject, sonidoAgarrar != null ? sonidoAgarrar.clip.length : 0.1f);
        }
    }

    private IEnumerator MostrarMensajeTemporal()
    {
        TengoGema.SetActive(true);
        PanelGema.SetActive(true);

        // 🎵 Reproducir audio del panel si lo tiene
        if (panelAudio != null)
            panelAudio.Play();

        yield return new WaitForSeconds(3f);
        Destroy(TengoGema);
    }
}
