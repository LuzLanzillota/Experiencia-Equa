using System.Collections;
using UnityEngine;

public class Estesidestruye : MonoBehaviour
{
    public Animator panel;
    public GameObject panelObj;
    public AudioSource audioPanel;

    private bool audioTermino = false;

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
        if (other.CompareTag("Player"))
        {
            panelObj.SetActive(true);

            panel.Play("Mensaje1Flores");

            if (audioPanel != null)
                audioPanel.Play();
            else
                Debug.LogError("❌ No hay audioPanel asignado, no puedo reproducir sonido.");

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
