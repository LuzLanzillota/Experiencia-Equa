// ----------------------------
// 1. Llave.cs
// ----------------------------
using UnityEngine;

public class Llave : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LlaveManager manager = other.GetComponent<LlaveManager>();
            if (manager != null)
            {
                manager.tieneLlave = true;  // ← ✅ ACA se guarda que el jugador la tiene
            }

            Destroy(gameObject); // Desaparece la llave del mundo
        }
    }
}