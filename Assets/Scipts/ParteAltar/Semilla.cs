using UnityEngine;

public class Semilla : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PocionManager manager = other.GetComponent<PocionManager>();
            if (manager != null)
            {
                manager.tieneSemilla = true;
                Debug.Log("?? Semilla recogida");
            }

            Destroy(gameObject);
        }
    }
}