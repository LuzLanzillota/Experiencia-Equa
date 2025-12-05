using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class Mensaje2Final : MonoBehaviour
{
    public Animator animMensaje;
    public GameObject mensajeObj;
    public AudioSource audioMensaje;
    public VideoPlayer videoPlayer;

    private bool audioTermino = false;
    private bool yaActivado = false;

    private void Start()
    {
        if (mensajeObj != null)
            mensajeObj.SetActive(false);

        if (videoPlayer != null)
        {
            // Cuando termine el video ? salir del juego
            videoPlayer.loopPointReached += SalirDelJuego;
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

            StartCoroutine(EsperarAudio());
        }
    }

    private IEnumerator EsperarAudio()
    {
        if (audioMensaje == null) yield break;

        yield return new WaitWhile(() => audioMensaje.isPlaying);
        audioTermino = true;
    }

    private void Update()
    {
        if (audioTermino && Input.GetKeyDown(KeyCode.E))
        {
            if (videoPlayer != null)
                videoPlayer.Play();

            Destroy(mensajeObj);
        }
    }

    // ?? ESTA FUNCIÓN SE LLAMA AUTOMÁTICAMENTE CUANDO TERMINA EL VIDEO
    private void SalirDelJuego(VideoPlayer vp)
    {
        Debug.Log("?? Video terminado. Cerrando el juego...");

        // Si estás en el editor de Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Si es una build normal
        Application.Quit();
#endif
    }
}
