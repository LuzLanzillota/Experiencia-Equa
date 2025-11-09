using System.Collections;
using UnityEngine;

public class Estesidestruye : MonoBehaviour
{
    public GameObject panel;
    public AudioSource audioPanel;   // 👉 arrastrá el AudioSource del panel o el que quieras reproducir
    private bool audioTermino = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panel.SetActive(true);
            audioPanel.Play();
            StartCoroutine(EsperarAudio());
        }
    }

    private IEnumerator EsperarAudio()
    {
        // ✅ Esperar a que termine el audio
        yield return new WaitWhile(() => audioPanel.isPlaying);

        audioTermino = true;
    }

    private void Update()
    {
        // ✅ Si el audio terminó y se presionó E → destruir panel
        if (audioTermino && panel.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(panel);
        }
    }
}



