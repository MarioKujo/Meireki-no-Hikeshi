using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    int NPCs;
    [SerializeField]
    GameObject canvasPausa;//Men� de pausa
    [SerializeField]
    GameObject canvasSeguro;//Men� que te pregunta "Seguro? S�/No"
    string NombreEscena = "";//Para introducir el nombre de la escena
    void Start()
    {
        canvasPausa.SetActive(false);//inicializa el canvas para que sea invisible
        canvasSeguro.SetActive(false);//inicializa el canvas para que sea invisible
    }
    void Update()
    {
        if (NPCs == HUD.people)
        {
            LevelComplete.nivelActual = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Level Complete");
        }
        if (Input.GetKeyDown(KeyCode.Escape))//Si se pulsa la tecla escape, se activa el men� de pausa y se para todo el juego
        {
            Pausa();//Funci�n para pausar
        }
    }
    public void Pausa()
    {
            canvasSeguro.SetActive(false);//Desactiva el seguro (por si acaso le da a No y vuelve al anterior)
            canvasPausa.SetActive(true);//Vuelve el de pausa activo
            Time.timeScale = 0;//Se detiene el tiempo
    }
    public void Reanudar()//Si se activa esta funci�n, el men� de pausa desaparece y vuelve a correr el tiempo
    {
        canvasPausa.SetActive(false);
        Time.timeScale = 1;
    }
    public void Reiniciar(string Nombre)//reinicia el nivel
    {
        SceneManager.LoadScene(Nombre);
    }
    public void Seguro(string Nombre)//Pregunta si est�s seguro
    {
        NombreEscena = Nombre;//Introduce el nombre de la escena (si pulsas el bot�n "Men� principal" es "MainMenu"
                              //y si le das a "Salir" cierra el programa)
        canvasSeguro.SetActive(true);//Activa el canvas "Seguro?"
        canvasPausa.SetActive(false);//Desactiva el canvas pausa
    }
    /*Los he tenido que hacer separados porque el "Seguro" se lo aplico a los botones "Men� principal" y "Salir", y el CargarEscena a los botones
     "S�" y "No"*/
    public void CargarEscena()
    {
        Time.timeScale = 1;
        if (NombreEscena == "MainMenu")
        {
        SceneManager.LoadScene("Title");//Si recibe "MainMenu", vuelve al t�tulo
        }
        if (NombreEscena == "Quit")
        {
            Cerrar();//Si recibe "Quit", se sale
        }
    }
    public void Cerrar()
    {
        Application.Quit();//Opci�n para salir del juego
    }
}