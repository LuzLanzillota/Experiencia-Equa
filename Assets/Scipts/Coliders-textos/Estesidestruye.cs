using System.Collections;
using UnityEngine;

public class Estesidestruye : MonoBehaviour
{
    public Animator panel;
    public GameObject panelObj;
    public AudioSource audioPanel;

    private bool audioTermino = false;
    private bool yaActivado = false; // ⭐ evita que vuelva a reproducirse

    private void Start()
    {
        if (panelObj != null)
            panelObj.SetActive(false);

        if (audioPanel == null)
            Debug.LogError("❌ No asignaste el AudioSource en 'audioPanel'.");

        if (panel == null)
            Debug.LogError("❌ No asignaste el Animator en 'panel'.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !yaActivado) // ⭐ Solo la primera vez
        {
            yaActivado = true;

            panelObj.SetActive(true);
            panel.Play("Mensaje1Flores");

            if (audioPanel != null)
                audioPanel.Play();

            StartCoroutine(EsperarAudio());
        }
    }

    private IEnumerator EsperarAudio()
    {
        if (audioPanel == null) yield break;

        yield return new WaitWhile(() => audioPanel.isPlaying);
        audioTermino = true;
    }

    private void Update()
    {
        if (audioTermino && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(panelObj);
        }
    }
}

