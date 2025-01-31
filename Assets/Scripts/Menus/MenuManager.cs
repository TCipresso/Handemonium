using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject DeathScreen;
    public GameObject OptionsMenu;  
    public GameObject PMenu;
    public PlayerMovement playerMovement;
    public GameObject hands;
    public GameObject player; 
    public Transform spawnPoint;
    public SpawnerManager spawnerManager;


    private bool isPaused = false;

    void Awake()
    {
        PauseMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        DeathScreen.SetActive(false);
    }

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
        if (playerMovement != null)
            playerMovement.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        if (playerMovement != null)
            playerMovement.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



    public void StartGame()
    {
        SceneManager.LoadScene(1); 
    }

    public void RestartGame()
    {
        // Reactivate the player's hands GameObject
        if (hands != null)
        {
            hands.SetActive(true);
            Debug.Log("Hands GameObject has been reactivated.");
        }

        // Re-enable the PlayerMovement script
        if (PlayerMovement.Instance != null)
        {
            PlayerMovement.Instance.enabled = true;
            Debug.Log("Player movement has been re-enabled.");
        }

        PowerUpManager.Instance.SwitchState(PowerUpManager.PowerUpState.Basic);
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;
       

        // Reset the mouse cursor for gameplay
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Cursor has been hidden and locked.");

        // Reload the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
        Debug.Log("Game has been restarted.");
        spawnerManager.InitializePools();
        PlayerStats.Instance.HP = 100f;
        PauseMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        DeathScreen.SetActive(false);
        //spawnerManager.ForceSpawn();
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
