using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public Animator sun;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sun.Play("DirectionalLight");
            Debug.Log("se oscurecio");
        }
    }
}
