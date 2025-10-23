using UnityEngine;

public class InicioManager : MonoBehaviour
{
    public GameObject panelInicio;     // Asigná el panel desde el inspector
    public MonoBehaviour scriptMovimiento; // Asigná acá el script que controla el movimiento del jugador

    private void Start()
    {
        // Mostrar panel al iniciar
        panelInicio.SetActive(true);

        // Desactivar movimiento del jugador
        if (scriptMovimiento != null)
            scriptMovimiento.enabled = false;

        // Mostrar cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Esta función la vas a asignar al botón "X" del panel
    public void CerrarPanel()
    {
        // Ocultar panel
        panelInicio.SetActive(false);

        // Reactivar movimiento del jugador
        if (scriptMovimiento != null)
            scriptMovimiento.enabled = true;

        // Ocultar cursor para volver al control del jugador
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
