using UnityEngine;
 using TMPro;

public class ZonaInteractiva : MonoBehaviour
{
    public GameObject textoUI;
    private bool jugadorEnZona = false;
    public Animator PuertaEntrada;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textoUI.SetActive(true);
            jugadorEnZona = true;
            PuertaEntrada.Play("PuertaEntradaCueva");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textoUI.SetActive(false);
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
