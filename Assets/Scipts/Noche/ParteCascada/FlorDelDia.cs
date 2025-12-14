using System.Collections;
using UnityEngine;

public class FlorDelDia : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource sonidoAgarrar;

    [Header("Muro")]
    public Collider colliderMuro;   // ⬅️ Collider que bloquea el paso

    private bool recogida = false;

    void OnTriggerEnter(Collider other)
    {
        if (recogida) return;

        if (other.CompareTag("Player"))
        {
            LagoManager manager = other.GetComponent<LagoManager>();
            if (manager != null)
            {
                manager.tieneFlorDelDia = true;
                Debug.Log("🌼 Flor del Día recogida");
            }

            if (sonidoAgarrar != null && sonidoAgarrar.clip != null)
                AudioSource.PlayClipAtPoint(sonidoAgarrar.clip, transform.position);

            // 👉 ACTIVAR MURO
            if (colliderMuro != null)
                colliderMuro.enabled = true;

            recogida = true;
            OcultarFlorVisualmente();
        }
    }

    private void OcultarFlorVisualmente()
    {
        foreach (var r in GetComponentsInChildren<Renderer>())
            r.enabled = false;

        foreach (var c in GetComponentsInChildren<Collider>())
            c.enabled = false;
    }
}
