using UnityEngine;

public class Gema : MonoBehaviour
{
    public GameObject objPuntos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objPuntos.GetComponent<ContadorGemas>().puntos += 1;
            Destroy(gameObject);
        }
    }
}
