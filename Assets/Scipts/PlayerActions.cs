using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public float PlayerActivateDistance;
    bool active = false;

    public AudioSource Piedra1;
    public AudioSource Piedra2;
    public AudioSource Piedra3;
    public AudioSource Piedra4;

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

                // Elegir un número aleatorio entre 1 y 4
                int randomNum = Random.Range(1, 5); // El 5 no se incluye

                // Reproducir el audio correspondiente
                switch (randomNum)
                {
                    case 1:
                        Piedra1.Play();
                        break;
                    case 2:
                        Piedra2.Play();
                        break;
                    case 3:
                        Piedra3.Play();
                        break;
                    case 4:
                        Piedra4.Play();
                        break;
                    default:
                        Debug.LogWarning("Número aleatorio fuera de rango");
                        break;
                }
            }
        }
    }
}

