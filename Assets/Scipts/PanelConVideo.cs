using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class PanelConVideo : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject panel; // Panel con la opción de activar el video (para 3 gemas)
    public VideoPlayer videoPlayer;
    public int indiceSiguienteEscena;
    public ContadorGemas contadorGemas; // Asume que tiene una propiedad 'puntos' pública
    public GameObject panelSinGemas; // Panel que se muestra si no tiene las gemas necesarias

    [Header("Configuración de Escenas")]
    public int indiceEscenaGemasCompletas = 3; // Índice de la escena a cargar con 6 gemas

    private bool jugadorDentro = false;
    private bool panelActivo = false; // Indica si el panel de interacción (con 3 gemas) está visible

    void Start()
    {
        // 🔹 Desactivar todos los paneles al inicio
        if (panel != null)
            panel.SetActive(false);
        if (videoPlayer != null)
            videoPlayer.gameObject.SetActive(false);
        if (panelSinGemas != null)
            panelSinGemas.SetActive(false);
    }

    void Update()
    {
        if (!jugadorDentro || contadorGemas == null) return; // Salir si el jugador no está o no hay contador

        int puntosActuales = contadorGemas.puntos;

        // --- Lógica de 6 Gemas: Carga directa de escena ---
        if (puntosActuales >= 6) // Usar >= 6 por si acaso
        {
            // Ocultar cualquier panel de UI antes de la carga
            panel.SetActive(false);
            panelSinGemas.SetActive(false);
            SceneManager.LoadScene(indiceEscenaGemasCompletas);
            return; // Salir de Update para no ejecutar más lógica
        }

        // --- Lógica de 3 Gemas: Activar panel de interacción ---
        if (puntosActuales == 3)
        {
            if (!panelActivo)
            {
                panel.SetActive(true);
                panelActivo = true;
                panelSinGemas.SetActive(false); // Asegurar que el otro panel esté oculto
            }
        }
        // --- Lógica de Menos de 3 Gemas: Activar panel de "sin gemas" ---
        else
        {
            panel.SetActive(false); // Asegurar que el panel de interacción esté oculto
            panelActivo = false;

            if (panelSinGemas != null)
                panelSinGemas.SetActive(true);
        }


        // --- Accionar con tecla E si el panel válido está activo (3 gemas) ---
        if (panelActivo && Input.GetKeyDown(KeyCode.E))
        {
            // Opcional: Destruir el panel para que no se vea el frame antes de que inicie el video
            // Destroy(panel); 
            panel.SetActive(false); // Mejor solo desactivarlo si lo necesitas luego

            panelActivo = false; // El panel de interacción ya no está activo
            StartCoroutine(ReproducirVideoYCambiarEscena());
        }
    }

    private IEnumerator ReproducirVideoYCambiarEscena()
    {
        if (videoPlayer != null)
        {
            // 🔹 Desactivamos todos los paneles de UI que puedan interferir
            if (panel != null) panel.SetActive(false);
            if (panelSinGemas != null) panelSinGemas.SetActive(false);

            // 🔹 Activamos el GameObject del VideoPlayer
            videoPlayer.gameObject.SetActive(true);

            // 🔹 Esperar a que el video esté preparado
            videoPlayer.Prepare();
            yield return new WaitUntil(() => videoPlayer.isPrepared);

            // 🔹 Reproducir video
            videoPlayer.Play();
            Debug.Log("Video se reproduce");

            // 🔹 Esperar a que termine de reproducirse.
            // Esto usa la propiedad isPlaying del VideoPlayer.
            yield return new WaitUntil(() => !videoPlayer.isPlaying);

            // 🔹 Cambiar de escena
            SceneManager.LoadScene(indiceSiguienteEscena);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = true;
            // La lógica de qué panel activar se maneja en Update, una vez que jugadorDentro es true
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = false;
            // 🔹 Ocultar todos los paneles al salir del trigger
            if (panel != null)
                panel.SetActive(false);
            if (panelSinGemas != null)
                panelSinGemas.SetActive(false);
            panelActivo = false;
        }
    }
}
