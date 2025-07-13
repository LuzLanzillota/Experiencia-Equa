using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cupula : MonoBehaviour
{
    public Animator cupula;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cupula.Play("CupulaArriba");
            Debug.Log("Cupula moviendose");
        }
    }
}
