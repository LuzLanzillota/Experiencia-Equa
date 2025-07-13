using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun3 : MonoBehaviour
{
    public Animator sun3;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sun3.Play("DirectionalLight3");
            Debug.Log("se oscurecio");
        }
    }
}
