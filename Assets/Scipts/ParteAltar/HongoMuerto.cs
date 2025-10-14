using UnityEngine;

public class HongoMuerto : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PocionManager manager = other.GetComponent<PocionManager>();
            if (manager != null)
            {
                manager.tieneHongo = true;
                Debug.Log("🍄 Hongo muerto recogido");
            }

            Destroy(gameObject);
        }
    }
}
