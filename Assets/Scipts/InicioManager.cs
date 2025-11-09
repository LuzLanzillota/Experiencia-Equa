using UnityEngine;
using UnityEngine.UI; // Necesario para usar Button

public class InicioManager : MonoBehaviour
{
    public GameObject panelInicio;
    public AudioSource AudioInicio;
    public MonoBehaviour scriptMovimiento;
    public Button botonCerrar; // 👉 arrastrá el botón desde el Inspector

    private void Start()
    {
        panelInicio.SetActive(true);
        AudioInicio.Play();

        // 🔒 Desactivar movimiento
        if (scriptMovimiento != null)
            scriptMovimiento.enabled = false;

        // 🔒 Desactivar botón hasta que el audio termine
        botonCerrar.interactable = false;

        // ✅ Llamar función cuando termine el audio
        Invoke(nameof(ActivarBotonCerrar), AudioInicio.clip.length);

        // Mostrar cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ActivarBotonCerrar()
    {
        botonCerrar.interactable = true;
    }

    public void CerrarPanel()
    {
        if (!botonCerrar.interactable) return; // Seguridad extra

        panelInicio.SetActive(false);

        if (scriptMovimiento != null)
            scriptMovimiento.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

