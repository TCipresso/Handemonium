using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; } // Singleton instance

    public enum PowerUpState
    {
        Basic,
        Dual,
        Power,
        Pull
    }

    public GameObject basicGun; 
    public GameObject dualGun;
    public GameObject powerGun;
    public GameObject pullGun;


    private PowerUpState currentState;

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

        SwitchState(PowerUpState.Basic);
    }

    // Method to switch states
    public void SwitchState(PowerUpState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case PowerUpState.Basic:
                basicGun.SetActive(true);
                dualGun.SetActive(false);
                powerGun.SetActive(false);
                pullGun.SetActive(false);
                break;
            case PowerUpState.Dual:
                basicGun.SetActive(false);
                dualGun.SetActive(true);
                powerGun.SetActive(false);
                pullGun.SetActive(false);
                break;
            case PowerUpState.Power:
                basicGun.SetActive(false);
                dualGun.SetActive(false);
                powerGun.SetActive(true);
                pullGun.SetActive(false);
                break;
            case PowerUpState.Pull:
                basicGun.SetActive(false);
                dualGun.SetActive(false);
                powerGun.SetActive(false);
                pullGun.SetActive(true);
                break;
        }
    }


    // Temporary state change methods
    public void Zoom(float newSpeed, float newFireRate, float duration)
    {
        StartCoroutine(ChangeSpeedAndFireRateTemporarily(newSpeed, newFireRate, duration));
    }

    public void Power(float duration)
    {
        StartCoroutine(TemporaryStateChange(PowerUpState.Power, duration));
    }

    public void DualWield(float duration)
    {
        StartCoroutine(TemporaryStateChange(PowerUpState.Dual, duration));
    }

    public void ActivatePull(float duration)
    {
        StartCoroutine(TemporaryStateChange(PowerUpState.Pull, duration));
    }

    public void Heal(float amount)
    {
        PlayerStats.Instance.HP += amount; // Add the healing amount to the current HP
        PlayerStats.Instance.HP = Mathf.Min(PlayerStats.Instance.HP, 100f); // Cap HP at 100 to prevent it from going over

        Debug.Log("Player healed by " + amount + ". Current HP: " + PlayerStats.Instance.HP);
    }

    private IEnumerator ChangeSpeedAndFireRateTemporarily(float newSpeed, float newFireRate, float duration)
    {
        float originalSpeed = PlayerMovement.Instance.maxSpeed;
        float originalFireRate = Weapon.Instance.fireRate;
        PlayerMovement.Instance.maxSpeed = newSpeed;
        Weapon.Instance.fireRate = newFireRate;

        yield return new WaitForSeconds(duration);

        PlayerMovement.Instance.maxSpeed = originalSpeed;
        Weapon.Instance.fireRate = originalFireRate;
    }

    private IEnumerator TemporaryStateChange(PowerUpState newState, float duration)
    {
        SwitchState(newState);
        yield return new WaitForSeconds(duration);
        SwitchState(PowerUpState.Basic);
    }
}
