using UnityEngine;

public class InteraccionPiedra : MonoBehaviour
{
    public GameObject objPuntos; // Objeto que tiene el script "Puntos"
    public Animator animator;    // Animator de esta piedra
    private bool puedeInteractuar = false;
    private bool animacionComenzada = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puedeInteractuar = true;
            Debug.Log("Presiona E para interactuar con la piedra");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puedeInteractuar = false;
        }
    }

    private void Update()
    {
        if (puedeInteractuar && !animacionComenzada && Input.GetKeyDown(KeyCode.E))
        {
            animacionComenzada = true;
            animator.SetTrigger("Interactua"); // activa la animación
        }
    }

    // Este método será llamado desde un evento de animación al final del clip
    public void AlTerminarAnimacion()
    {
        objPuntos.GetComponent<Puntos>().puntos += 1;
        Debug.Log("Punto sumado tras terminar animación");
    }
}
