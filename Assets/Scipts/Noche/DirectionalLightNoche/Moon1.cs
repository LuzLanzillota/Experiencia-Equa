using UnityEngine;

public class Moon : MonoBehaviour
{
    public Animator moonAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            moonAnimator.Play("DirectionalLightNoche1");
            Debug.Log("Moon1 → Activó DirectionalLightNoche1");

            Destroy(gameObject); // elimina el collider después de usarse
        }
    }
}
