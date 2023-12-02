using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void CargarNivel()
    {
        SceneManager.LoadScene("Level Selector");
        
    }
    public void Mainmenu()
    {
        SceneManager.LoadScene("Title");
    }
    public void Salir()
    {
        Application.Quit();
    }
}
