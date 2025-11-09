using System.Collections;
using UnityEngine;

public class codigogemas : MonoBehaviour
{
    public GameObject panel;
    public float tiempoVisible = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panel.SetActive(true);
            StartCoroutine(DestruirDespuesDeTiempo());
        }
    }

    private IEnumerator DestruirDespuesDeTiempo()
    {
        yield return new WaitForSeconds(tiempoVisible);
        Destroy(panel);
    }
}

