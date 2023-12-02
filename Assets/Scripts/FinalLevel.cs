using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalLevel : MonoBehaviour
{
    public static int levelsCompleted;
    [SerializeField]
    GameObject Tecla;
    [SerializeField]
    string level;
    bool Infront;
    private void Start()
    {
        Tecla.SetActive(false);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Tecla.SetActive(false);
        Infront = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Tecla.SetActive(true);
            Infront = true;
        }
    }
    private void Update()
    {
        if (Infront)
        {
            if (Input.GetKeyDown(KeyCode.W) && levelsCompleted == 3)
            {
                SceneManager.LoadScene(level);
            }
        }

    }
}
