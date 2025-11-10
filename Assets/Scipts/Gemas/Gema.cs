using System.Collections;
using UnityEngine;

public class Gema : MonoBehaviour
{
    public GameObject objPuntos;
    public GameObject TengoGema;
    public AudioSource sonidoAgarrar;
    public Animator animGema; // Animator de la gema
    public string nombreAnimGema = "GemaActiva";
    public Animator animVideoGema; // 👈 Animator del objeto que tiene la animación "VideoGema"
    private Collider colGema;

    private void Start()
    {
        colGema = GetComponent<Collider>();
        colGema.enabled = false;

        if (TengoGema != null)
            TengoGema.SetActive(false);

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

            // 🔹 Activar la animación "VideoGema"
            if (animVideoGema != null)
            {
                animVideoGema.SetTrigger("VideoGema");
            }
            else
            {
                Debug.LogWarning("⚠️ Falta asignar el Animator que contiene la animación 'VideoGema'.");
            }

            Destroy(gameObject, sonidoAgarrar != null ? sonidoAgarrar.clip.length : 0.1f);
        }
    }

    private IEnumerator MostrarMensajeTemporal()
    {
        TengoGema.SetActive(true);
        yield return new WaitForSeconds(3f);
        Destroy(TengoGema);
    }
}
