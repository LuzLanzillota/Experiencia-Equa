using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun2 : MonoBehaviour
{
    public Animator sun2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sun2.Play("DirectionalLight2");
            Debug.Log("se oscurecio");
        }
    }
}
