using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; } // Singleton instance

    // Public stats
    public float HP = 100f; // Initialize HP with a default value, now directly accessible
    public float moveSpeed; // To reflect PlayerMovement's move speed
    public float dashCooldown; // To reflect Charge's dash cooldown
    public float Def_MoveSpeed;
    public float Def_DashCD;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optionally make the object persistent across scenes
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Ensure that there is only one instance
        }

        // Initialize from other scripts
        moveSpeed = PlayerMovement.Instance.moveSpeed;
        dashCooldown = Charge.Instance.dashCooldown;
    }

    public void PlayerDie()
    {
        Debug.Log("Player has died.");
        // Additional actions on death, like animations or game over logic
    }

    public void IncMoveSpeed(float amount)
    {
        moveSpeed += amount;
        PlayerMovement.Instance.moveSpeed = moveSpeed; // Ensure the player movement speed is updated
        Debug.Log("Increased move speed: " + moveSpeed);
    }

    public void DecDashCooldown(float amount)
    {
        dashCooldown = Mathf.Max(0, dashCooldown - amount);
        Charge.Instance.dashCooldown = dashCooldown; // Update the actual dash cooldown in Charge
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
        // Reset move speed and dash cooldown to initial values
        PlayerMovement.Instance.moveSpeed = Def_MoveSpeed;
        Charge.Instance.dashCooldown = Def_DashCD;
        Debug.Log("Stats reset to defaults, except HP.");
    }
}
