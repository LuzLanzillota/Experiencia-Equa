using UnityEngine;

public class ResetAlIniciar : MonoBehaviour
{
    private void Start()
    {
        // 🔥 Esto solo se ejecuta al entrar al menú principal
        PlayerPrefs.SetInt("EscenaAnterior", -1);
        PlayerPrefs.Save();
    }
}

