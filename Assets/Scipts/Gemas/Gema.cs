using System.Collections;
using UnityEngine;

public class Gema : MonoBehaviour
{
    public GameObject objPuntos;
    public GameObject TengoGema;
    public AudioSource sonidoAgarrar;
    public GameObject PanelGema;
    public AudioSource panelAudio; 
    public Animator animGema; 
    public GameObject MuroAgarraGema;
    public GameObject PanelAgarraGema;
    public string nombreAnimGema = "Esta"; // La animación que debe terminar

    private Collider colGema;
    public bool fueAgarrada = false;

    private void Start()
    {
        colGema = GetComponent<Collider>();
        colGema.enabled = false;

        // Apagar objetos al inicio
        if (TengoGema != null)
        {
            MuroAgarraGema.SetActive(false);
            PanelAgarraGema.SetActive(false);
            TengoGema.SetActive(false);
        }

        if (PanelGema != null)
        {
            PanelGema.SetActive(false);

            if (panelAudio == null)
                panelAudio = PanelGema.GetComponent<AudioSource>();
        }

        if (animGema != null)
            StartCoroutine(ActivarColliderYMurosDespuesAnim());
        else
            Debug.LogWarning("⚠️ Falta asignar Animator de la gema.");
    }

    private IEnumerator ActivarColliderYMurosDespuesAnim()
    {
        // Esperar a que termine la animación "Esta"
        yield return new WaitForSeconds(GetDuracionAnim(animGema, nombreAnimGema));

        // Activar muros cuando la gema ya terminó su animación
        MuroAgarraGema.SetActive(true);
        PanelAgarraGema.SetActive(true);

        // Ahora sí se puede agarrar
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
            fueAgarrada = true;

            objPuntos.GetComponent<ContadorGemas>().puntos += 1;

            if (sonidoAgarrar != null)
                sonidoAgarrar.Play();

            if (TengoGema != null)
                StartCoroutine(MostrarMensajeTemporal());

            // Destruye la gema después del sonido
            Destroy(gameObject, sonidoAgarrar != null ? sonidoAgarrar.clip.length : 0.1f);
        }
    }

    private IEnumerator MostrarMensajeTemporal()
    {
        TengoGema.SetActive(true);
        PanelGema.SetActive(true);

        if (panelAudio != null)
            panelAudio.Play();

        yield return new WaitForSeconds(3f);

        Destroy(TengoGema);
    }

    // ⭐ Se ejecuta SIEMPRE cuando el objeto se destruye
    private void OnDestroy()
    {
        if (MuroAgarraGema != null)
            MuroAgarraGema.SetActive(false);

        if (PanelAgarraGema != null)
            PanelAgarraGema.SetActive(false);
    }
}


