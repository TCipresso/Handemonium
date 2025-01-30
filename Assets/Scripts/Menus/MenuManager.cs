using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject DeathScreen;
    public GameObject OptionsMenu;  
    public GameObject PMenu;     

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;  
        isPaused = true;
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;  
        isPaused = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1); 
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);  
    }

    public void ExitGame()
    {
        Application.Quit(); 
    }

    public void ShowDeathScreen()
    {
        DeathScreen.SetActive(true);  
    }

    public void OpenOptions()
    {
        OptionsMenu.SetActive(true);  
        PMenu.SetActive(false);      
    }

    public void BackToP()
    {
        OptionsMenu.SetActive(false); 
        PMenu.SetActive(true);        
    }
}
