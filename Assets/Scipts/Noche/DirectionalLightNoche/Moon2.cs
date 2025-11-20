using UnityEngine;

public class Moon2 : MonoBehaviour
{
    public Animator moonAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            moonAnimator.Play("DirectionalLightNoche2");
            Debug.Log("Moon2 → Activó DirectionalLightNoche2");

            Destroy(gameObject); // elimina el collider después de usarse
        }
    }
}
