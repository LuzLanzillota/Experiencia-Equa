using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Jugar : MonoBehaviour
{
    public void Dia()
    {
        SceneManager.LoadScene(1);
    }
    public void Noche()
    {
        SceneManager.LoadScene(2);
    }
    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}
