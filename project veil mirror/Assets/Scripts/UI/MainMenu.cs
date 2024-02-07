using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string SceneTitle;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneTitle);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
