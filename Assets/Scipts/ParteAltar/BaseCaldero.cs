using UnityEngine;
using System.Collections;

public class BaseCaldero : MonoBehaviour
{
    public Animator animSemilla;
    public Animator animHongo;
    public Animator animAgua;
    public Animator animHongosGrandes;
    public Animator animHongosGrandes2;
    public Animator animHongosGrandes3;
    public Animator animHongosGrandes4;
    public Animator animHongosGrandes5;
    public Animator animHongosGrandes6;
    public Animator animHongosGrandes7;
    public Animator animHongosGrandes8;
    public Animator animHongosGrandes9;
    public Animator animHongosGrandes10;
    public Animator animHongosGrandes11;
    public Animator animHongosGrandes12;
    public Animator animHongosGrandes13;
    public Animator animHongosGrandes14;
    public Animator animHongosGrandes15;




    private bool jugadorEnZona = false;

    void Update()
    {
        if (jugadorEnZona && Input.GetKeyDown(KeyCode.E))
        {
            GameObject jugador = GameObject.FindWithTag("Player");
            if (jugador != null)
            {
                PocionManager manager = jugador.GetComponent<PocionManager>();

                if (manager != null && manager.tieneSemilla && manager.tieneHongo)
                {
                    Debug.Log("🧪 Iniciando creación de poción");
                    StartCoroutine(CrearPocion());

                    // Evita repetir la acción
                    manager.tieneSemilla = false;
                    manager.tieneHongo = false;
                }
                else
                {
                    Debug.Log("⚠ Falta algún ingrediente");
                }
            }
        }
    }

    IEnumerator CrearPocion()
    {
        // 1️⃣ Semilla cayendo
        if (animSemilla != null)
        {
            animSemilla.gameObject.SetActive(true); // Activa el objeto
            yield return new WaitForEndOfFrame();   // Espera un frame para asegurar la activación
            animSemilla.SetTrigger("Caer");
        }

        // 2️⃣ Hongo cayendo
        if (animHongo != null)
        {
            animHongo.gameObject.SetActive(true);
            yield return new WaitForEndOfFrame();
            animHongo.SetTrigger("Caer");
        }

        yield return new WaitForSeconds(1.5f);

        // 3️⃣ Agua subiendo
        if (animAgua != null)
        {
            animAgua.gameObject.SetActive(true);
            yield return new WaitForEndOfFrame();
            animAgua.SetTrigger("Subir");
        }

        yield return new WaitForSeconds(3f);

        // 4️⃣ Hongos grandes creciendo
        if (animHongosGrandes != null)
        {
            animHongosGrandes.Play("Crece");
            animHongosGrandes2.Play("Crece");
            animHongosGrandes3.Play("Crece");
            animHongosGrandes4.Play("Crece");
            animHongosGrandes5.Play("Crece");
            animHongosGrandes6.Play("Crece");
            animHongosGrandes7.Play("Crece");
            animHongosGrandes8.Play("Crece");
            animHongosGrandes9.Play("Crece");
            animHongosGrandes10.Play("Crece");
            animHongosGrandes11.Play("Crece");
            animHongosGrandes12.Play("Crece");
            animHongosGrandes13.Play("Crece");
            animHongosGrandes14.Play("Crece");
            animHongosGrandes15.Play("Crece");
        }

        Debug.Log("✨ Pocion completada");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            jugadorEnZona = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            jugadorEnZona = false;
    }
}