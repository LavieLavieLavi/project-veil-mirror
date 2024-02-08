using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class GameOverMenu : NetworkBehaviour
{
    public GameObject gameOverMenu;
    public bool isGameOver;
    [SerializeField] private string SceneTitle;

    // Start is called before the first frame update
    void Start()
    {
        gameOverMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // You can add any network-related logic here if needed
    }

    public void gameOver()
    {
        Time.timeScale = 1f;
        gameOverMenu.SetActive(true);
    }

    public void GotoMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneTitle);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
