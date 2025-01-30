using UnityEngine;
using System.Collections;
using UnityEngine.UI; 


public class Charge : MonoBehaviour
{
    public static Charge Instance { get; private set; }  

    public Rigidbody rb;                  
    public Transform orientation;       
    public float dashSpeed = 10f;        
    public float dashDistance = 10f;     
    public float dashCooldown = 1.5f;     
    public LayerMask obstacleLayers;
    public Image cooldownImage;  


    private bool isDashing;              
    private bool canDash = true;         
    private Vector3 dashDirection;      
    private float dashTime;             

    private void Awake()
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

        dashCooldown = 1.5f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && !isDashing)
        {
            StartDash();
        }
    }

    private void StartDash()
    {
        canDash = false;
        isDashing = true;
        Vector3 startPosition = transform.position;
        dashDirection = orientation.forward * Input.GetAxisRaw("Vertical") + orientation.right * Input.GetAxisRaw("Horizontal");
        if (dashDirection != Vector3.zero)
        {
            dashDirection.Normalize(); 
        }
        else
        {
            dashDirection = orientation.forward; 
        }

        dashTime = dashDistance / dashSpeed;
        float actualDashDistance = CalculateDashDistance(startPosition, dashDirection, dashDistance);
        StartCoroutine(PerformDash(startPosition, dashDirection, actualDashDistance));
    }

    private float CalculateDashDistance(Vector3 startPosition, Vector3 direction, float maxDistance)
    {
        RaycastHit hit;
        if (Physics.Raycast(startPosition, direction, out hit, maxDistance, obstacleLayers))
        {
            return hit.distance - 0.01f; 
        }
        return maxDistance; 
    }

    private IEnumerator PerformDash(Vector3 startPosition, Vector3 direction, float distance)
    {
        float elapsed = 0;
        Vector3 lastSafePosition = startPosition;

        while (elapsed < dashTime)
        {
            Vector3 nextPosition = startPosition + direction * dashSpeed * elapsed;
            if (!IsPositionSafe(nextPosition, direction))
            {
                rb.MovePosition(lastSafePosition);
                break;
            }
            lastSafePosition = nextPosition;
            rb.MovePosition(nextPosition);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(startPosition + direction * distance);
        isDashing = false;
        StartCoroutine(DashCooldown());
    }

    private bool IsPositionSafe(Vector3 position, Vector3 direction)
    {
        float checkDistance = 0.1f;
        RaycastHit hit;
        if (Physics.Raycast(position, direction, out hit, checkDistance, obstacleLayers))
        {
            return false;
        }
        return true;
    }

    private IEnumerator DashCooldown()
    {
        float cooldownTimer = 0;

        while (cooldownTimer < dashCooldown)
        {
            cooldownTimer += Time.deltaTime;
            // This will start at 0 and fill up to 1 as the cooldown expires
            cooldownImage.fillAmount = cooldownTimer / dashCooldown;
            yield return null;
        }

        cooldownImage.fillAmount = 1;  // Ensure it's fully filled at the end
        canDash = true;
    }

}
