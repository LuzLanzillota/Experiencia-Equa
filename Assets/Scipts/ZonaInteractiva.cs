using UnityEngine;
 using TMPro;

public class ZonaInteractiva : MonoBehaviour
{
    public GameObject textoUI;
    public GameObject textoUI2;
    private bool jugadorEnZona = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textoUI.SetActive(true);
            textoUI2.SetActive(true);
            jugadorEnZona = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textoUI.SetActive(false);
            textoUI2.SetActive(false);
            jugadorEnZona = false;
        }
    }

    private void Update()
    {
        if (jugadorEnZona && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interacción activada");
            
        }
    }
}
