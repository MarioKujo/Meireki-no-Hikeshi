using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelComplete : MonoBehaviour
{
    public static bool[] Completed = new bool[3];
    public static string nivelActual;
    [SerializeField]
    Text completed;
    [SerializeField]
    Text finalLevel;
    private void Awake()
    {
        finalLevel.gameObject.SetActive(false);
        switch (nivelActual)
        {
            case "Level 1":
                completed.text = "Level 1 completed!";
                if (!Completed[0])
                {
                    FinalLevel.levelsCompleted++;
                    Completed[0] = true;
                    Debug.Log("++");
                }
                break;
            case "Level 2":
                completed.text = "Level 2 completed!";
                if (!Completed[1])
                {
                    FinalLevel.levelsCompleted++;
                    Completed[1] = true;
                    Debug.Log("++");
                }
                break;
            case "Level 3":
                completed.text = "Level 3 completed!";
                if (!Completed[2])
                {
                    FinalLevel.levelsCompleted++;
                    Completed[2] = true;
                    Debug.Log("++");
                }
                break;
        }
        if (FinalLevel.levelsCompleted == 3)
        {
            finalLevel.gameObject.SetActive(true);
            finalLevel.text = "The final level has been unlocked!";
        }
    }
    public void NextLevel()
    {
        SceneManager.LoadScene("Level Selector");
    }
}
