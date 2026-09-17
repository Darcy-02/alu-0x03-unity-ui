using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;

    private Rigidbody rb;

    [Header("Camera Settings")]
    private Camera mainCamera;
    private Vector3 cameraOffset;

    [Header("Score Settings")]
    private int score = 0;

    [Header("Health Settings")]
    public int health = 5;

    // Store starting values to reset on Game Over
    private int startingScore;
    private int startingHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        mainCamera = Camera.main;
        cameraOffset = mainCamera.transform.position - transform.position;

        startingScore = score;
        startingHealth = health;
    }

    void FixedUpdate()
    {
        // Player movement input (natural controls)
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down

        // Move only on X/Z axes
        Vector3 movement = new Vector3(moveX, 0f, moveZ) * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Camera follows player
        mainCamera.transform.position = transform.position + cameraOffset;
    }

    void Update()
    {
        // Game Over check
        if (health <= 0)
        {
            Debug.Log("Game Over!");

            // Reset health and score
            health = startingHealth;
            score = startingScore;

            // Reload current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Coin collection
        if (other.CompareTag("Pickup"))
        {
            score++;
            Debug.Log("Score: " + score);
            Destroy(other.gameObject);
        }

        // Trap collision
        if (other.CompareTag("Trap"))
        {
            health--;
            Debug.Log("Health: " + health);
        }

        // Goal reached
        if (other.CompareTag("Goal"))
        {
            Debug.Log("You win!");
        }
    }
}
