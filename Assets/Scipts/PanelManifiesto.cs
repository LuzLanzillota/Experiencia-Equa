using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class Mensaje2Final : MonoBehaviour
{
    public Animator animMensaje;
    public GameObject mensajeObj;
    public AudioSource audioMensaje;
    public VideoPlayer videoPlayer;
    public GameObject panelFinal;

    public AudioSource audioAmbiente;   // ?? Ambiente con fade out

    private bool audioTermino = false;
    private bool yaActivado = false;
    private bool videoTermino = false;

    private void Start()
    {
        if (mensajeObj != null)
            mensajeObj.SetActive(false);

        if (panelFinal != null)
            panelFinal.SetActive(false);

        if (videoPlayer != null)
        {
            videoPlayer.playOnAwake = false;
            videoPlayer.Stop();
            videoPlayer.loopPointReached += ActivarPanelFinal;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !yaActivado)
        {
            yaActivado = true;

            mensajeObj.SetActive(true);
            animMensaje.Play("mensaje2final");

            if (audioMensaje != null)
                audioMensaje.Play();

            StartCoroutine(EsperarNarracion());
        }
    }

    private IEnumerator EsperarNarracion()
    {
        if (audioMensaje == null) yield break;

        yield return new WaitWhile(() => audioMensaje.isPlaying);
        audioTermino = true;
    }

    private void Update()
    {
        // ?? Cuando termina la narración, si se presiona E ? iniciar video
        if (!videoTermino && audioTermino && Input.GetKeyDown(KeyCode.E))
        {
            // ?? Fade out suave del audio ambiente
            if (audioAmbiente != null)
                StartCoroutine(FadeOutAudio(audioAmbiente, 0.5f));  // 1 segundo

            if (videoPlayer != null)
                videoPlayer.Play();

            Destroy(mensajeObj);
        }

        // ?? Después del video ? E sale del juego
        if (videoTermino && Input.GetKeyDown(KeyCode.E))
        {
            SalirDelJuego();
        }
    }

    private IEnumerator FadeOutAudio(AudioSource audio, float duration)
    {
        float startVolume = audio.volume;

        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            audio.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }

        audio.Stop();
        audio.volume = startVolume;  // Por si lo necesitás después reiniciar
    }

    private void ActivarPanelFinal(VideoPlayer vp)
    {
        videoTermino = true;

        if (panelFinal != null)
            panelFinal.SetActive(true);

        Debug.Log("?? Video terminado ? Panel final activado. Presiona E para salir.");
    }

    private void SalirDelJuego()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
