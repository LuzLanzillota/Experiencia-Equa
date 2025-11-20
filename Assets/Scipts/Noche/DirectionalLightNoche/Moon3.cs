using UnityEngine;

public class Moon3 : MonoBehaviour
{
    public Animator moonAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            moonAnimator.Play("DirectionalLightNoche3");
            Debug.Log("Moon3 → Activó DirectionalLightNoche3");

            Destroy(gameObject); // elimina el collider después de usarse
        }
    }
}
