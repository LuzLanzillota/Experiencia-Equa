using UnityEngine;

public class ParedesPJ : MonoBehaviour
{
    [Header("Muros que bloquean al Player")]
    public GameObject[] muros;

    [Header("Tag de los mensajes")]
    public string tagMensaje = "Mensaje";

    private bool murosActivos = false;

    void Start()
    {
        DesactivarMuros();
    }

    void Update()
    {
        GameObject mensajeActivo = GameObject.FindWithTag(tagMensaje);

        if (mensajeActivo != null && !murosActivos)
        {
            ActivarMuros();
        }
        else if (mensajeActivo == null && murosActivos)
        {
            DesactivarMuros();
        }
    }

    void ActivarMuros()
    {
        foreach (GameObject muro in muros)
        {
            if (muro != null)
                muro.SetActive(true);
        }

        murosActivos = true;
    }

    void DesactivarMuros()
    {
        foreach (GameObject muro in muros)
        {
            if (muro != null)
                muro.SetActive(false);
        }

        murosActivos = false;
    }
}
