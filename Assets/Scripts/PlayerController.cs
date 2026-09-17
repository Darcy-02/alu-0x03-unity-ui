using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

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
    public Text scoreText;

    /*[Header("Health Settings")]
    public int health = 5;

    

    public Text healthText;

    public Text winLoseText;

    public Image winLoseBG;
    */
    // Store starting values to reset on Game Over
    private int startingScore;
    private int startingHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        mainCamera = Camera.main;
        cameraOffset = mainCamera.transform.position - transform.position;

        //startingScore = score;
        //startingHealth = health;

        SetScoreText();
        //SetHealthText();
        //winLoseText.text = "";
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }
        // Game Over check
        /*if (health <= 0)
        {
            winLoseText.text = "Game Over!";
            winLoseText.color = Color.white;
            winLoseBG.color = Color.red;
            // Start coroutine to reload the scene after a delay
            StartCoroutine(LoadScene(3f));
            return;

        }
        SetHealthText();
        */
    }

    void OnTriggerEnter(Collider other)
    {
        // Coin collection
        if (other.CompareTag("Pickup"))
        {
            score++;
            SetScoreText();
            Destroy(other.gameObject);
        }

        // Trap collision
        /*if (other.CompareTag("Trap"))
        {
            health--;

        }

        // Goal reached
        if (other.CompareTag("Goal"))
        {
            winLoseText.text = "You Win!";
            winLoseText.color = Color.black;
            winLoseBG.color = Color.green;
        }
        */
    }


    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    /*
    void SetHealthText()
    {
        healthText.text = "Health: " + health;
    }

    // Coroutine to wait for a specified time before reloading the scene
    private IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds); // Wait for the specified seconds
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
    */
}