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
    public Animator animHongosGrandes16;
    public Animator animHongosGrandes17;
    public Animator animHongosGrandes18;
    public Animator animHongosGrandes19;
    public Animator animHongosGrandes20;
    public Animator animHongosGrandes21;
    public Animator animHongosGrandes22;
    public Animator animHongosGrandes23;
    public Animator animHongosGrandes24;
    public Animator animHongosGrandes25;
    public Animator animHongosGrandes26;
    public Animator animHongosGrandes27;
    public Animator animHongosGrandes28;
    public Animator animHongosGrandes29;
    public Animator animHongosGrandes30;
    public Animator animHongosGrandes31;
    public Animator animHongosGrandes32;
    public Animator animHongosGrandes33;
    public Animator animHongosGrandes34;
    public Animator animHongosGrandes35;
    public Animator animHongosGrandes36;
    public Animator animHongosGrandes37;
    public Animator animHongosGrandes38;
    public Animator animHongosGrandes39;
    public Animator animHongosGrandes40;
    public Animator animHongosGrandes41;
    public Animator animHongosGrandes42;
    public Animator animHongosGrandes43;
    public Animator animHongosGrandes44;
    public Animator animHongosGrandes45;
    public Animator animHongosGrandes46;
    public Animator animHongosGrandes47;
    public Animator animHongosGrandes48;
    public Animator animHongosGrandes49;
    public Animator animHongosGrandes50;







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
            animHongosGrandes16.Play("Crece");
            animHongosGrandes17.Play("Crece");
            animHongosGrandes18.Play("Crece");
            animHongosGrandes19.Play("Crece");
            animHongosGrandes20.Play("Crece");
            animHongosGrandes21.Play("Crece");
            animHongosGrandes22.Play("Crece");
            animHongosGrandes23.Play("Crece");
            animHongosGrandes24.Play("Crece");
            animHongosGrandes25.Play("Crece");
            animHongosGrandes26.Play("Crece");
            animHongosGrandes27.Play("Crece");
            animHongosGrandes28.Play("Crece");
            animHongosGrandes29.Play("Crece");
            animHongosGrandes30.Play("Crece");
            animHongosGrandes31.Play("Crece");
            animHongosGrandes32.Play("Crece");
            animHongosGrandes33.Play("Crece");
            animHongosGrandes34.Play("Crece");
            animHongosGrandes35.Play("Crece");
            animHongosGrandes36.Play("Crece");
            animHongosGrandes37.Play("Crece");
            animHongosGrandes38.Play("Crece");
            animHongosGrandes39.Play("Crece");
            animHongosGrandes40.Play("Crece");
            animHongosGrandes41.Play("Crece");
            animHongosGrandes42.Play("Crece");
            animHongosGrandes43.Play("Crece");
            animHongosGrandes44.Play("Crece");
            animHongosGrandes45.Play("Crece");
            animHongosGrandes46.Play("Crece");
            animHongosGrandes47.Play("Crece");
            animHongosGrandes48.Play("Crece");
            animHongosGrandes49.Play("Crece");
            animHongosGrandes50.Play("Crece");

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