using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public float PlayerActivateDistance;
    bool active = false;

    private void Update()
    {
        RaycastHit hit;
        active = Physics.Raycast(transform.position, transform.forward, out hit, PlayerActivateDistance);

        if (Input.GetKeyDown(KeyCode.E) && active)
        {
            Debug.Log("La piedra sube");

            if (hit.transform != null && hit.transform.TryGetComponent<Animator>(out Animator anim))
            {
                anim.SetTrigger("Interactua");
            }
        }
    }
}
