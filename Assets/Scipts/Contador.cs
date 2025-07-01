using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject objPuntos;

    private void OnTriggerEnter(Collider other)
    {
        objPuntos.GetComponent<Puntos>().puntos += 1;

        Debug.Log("Un punto");

        Destroy(gameObject);
    }

}
