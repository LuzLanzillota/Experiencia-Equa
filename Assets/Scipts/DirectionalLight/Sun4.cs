using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun4 : MonoBehaviour
{
    public Animator sun4;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sun4.Play("DirectionalLight4");
            Debug.Log("se oscurecio");
        }
    }
}
