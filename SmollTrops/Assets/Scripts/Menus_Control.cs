using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus_Control : MonoBehaviour
{ // esto está en el canvas de cada escena

    public GameObject deadMenu;
    public GameObject pauseMenu;
    public GameObject victoryMenu;
    public Player_Control _PC; //singleton del player

    void Start()
    {
        // pillo el singleton del Player
        _PC = Player_Control.instance;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (_PC.health <= 0)
        {
            Time.timeScale = 0;
            deadMenu.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                QuitPause();
            }
            else
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
        }
    }

    public void QuitPause()
    { 
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGameApp()
    {
        print("Quitting Game...");
        Application.Quit();
    }
}
