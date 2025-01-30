using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; } // Singleton instance

    // Public stats
    public float HP = 100f; 
    public float moveSpeed;
    public float dashCooldown; 
    public float Def_MoveSpeed;
    public float Def_DashCD;
    public GameObject DeathScreen;
    public float damageAmount = 20f;





    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else if (Instance != this)
        {
            Destroy(gameObject); 
        }

        // Initialize from other scripts
        moveSpeed = PlayerMovement.Instance.moveSpeed;
        dashCooldown = Charge.Instance.dashCooldown;
    }

    public void PlayerDie()
    {
        Debug.Log("Player has died.");

        // Disable the PlayerMovement script
        if (PlayerMovement.Instance != null)
        {
            PlayerMovement.Instance.enabled = false;
            Debug.Log("Player movement disabled.");
        }

        // Dynamically find and disable the Hands GameObject
        GameObject hands = GameObject.Find("HandsUI");
        if (hands != null)
        {
            hands.SetActive(false);
            Debug.Log("Hands GameObject has been disabled.");
        }
        else
        {
            Debug.LogError("Hands GameObject not found. Check the name is correct.");
        }

        // Activate the death screen
        if (DeathScreen != null)
        {
            DeathScreen.SetActive(true);
            Debug.Log("Death screen activated.");
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }




    public void IncMoveSpeed(float amount)
    {
        moveSpeed += amount;
        PlayerMovement.Instance.moveSpeed = moveSpeed; 
        Debug.Log("Increased move speed: " + moveSpeed);
    }

    public void DecDashCooldown(float amount)
    {
        dashCooldown = Mathf.Max(0, dashCooldown - amount);
        Charge.Instance.dashCooldown = dashCooldown; 
        Debug.Log("Decreased dash cooldown: " + dashCooldown);
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        Debug.Log("Player took damage: " + damage + ", Remaining HP: " + HP);
        if (HP <= 0)
        {
            PlayerDie();
        }
    }

    public void ResetStats()
    {
        PlayerMovement.Instance.moveSpeed = Def_MoveSpeed;
        Charge.Instance.dashCooldown = Def_DashCD;
        Debug.Log("Stats reset to defaults, except HP.");
    }
}
