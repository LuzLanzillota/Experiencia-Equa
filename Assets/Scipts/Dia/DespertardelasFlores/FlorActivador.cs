// ----------------------------
// 3. FlorActivador.cs
// ----------------------------
using UnityEngine;

public class FlorActivador : MonoBehaviour
{
    private Animation anim;

    [SerializeField] private string nombreAnimacion; // 👈 este campo debe aparecer en el Inspector

    void Start()
    {
        anim = GetComponent<Animation>();
    }

    public void ActivarFlor()
    {
        if (anim != null && anim[nombreAnimacion] != null)
        {
            anim.Play(nombreAnimacion);
        }
        else
        {
            Debug.LogWarning($"La animación '{nombreAnimacion}' no se encontró en {gameObject.name}");
        }
    }
}