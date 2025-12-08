using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class Mensaje2Final : MonoBehaviour
{
    public Animator animMensaje;
    public GameObject mensajeObj;
    public AudioSource audioMensaje;        // ?? Narración del mensaje
    public VideoPlayer videoPlayer;
    public GameObject panelFinal;

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
            videoPlayer.playOnAwake = false;   // ?? Asegura que NO arranque solo
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

            // ?? Reproduce automáticamente la narración del mensaje
            if (audioMensaje != null)
                audioMensaje.Play();

            StartCoroutine(EsperarNarracion());
        }
    }

    // ? Espera a que termine el audio ANTES de permitir E
    private IEnumerator EsperarNarracion()
    {
        if (audioMensaje == null) yield break;

        yield return new WaitWhile(() => audioMensaje.isPlaying);
        audioTermino = true;   // ?? Ahora sí se puede presionar E para seguir
    }

    private void Update()
    {
        // ?? Solo cuando la narración terminó ? E cierra el mensaje e inicia el video
        if (!videoTermino && audioTermino && Input.GetKeyDown(KeyCode.E))
        {
            if (videoPlayer != null)
                videoPlayer.Play();

            Destroy(mensajeObj);
        }

        // ?? Una vez que el video terminó ? E sale del juego
        if (videoTermino && Input.GetKeyDown(KeyCode.E))
        {
            SalirDelJuego();
        }
    }

    // ?? Activa panel final automáticamente al terminar el video
    private void ActivarPanelFinal(VideoPlayer vp)
    {
        videoTermino = true;

        if (panelFinal != null)
            panelFinal.SetActive(true);

        Debug.Log("?? Video terminado ? Panel final activado. Presiona E para salir.");
    }

    // ?? Cerrar el juego
    private void SalirDelJuego()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
