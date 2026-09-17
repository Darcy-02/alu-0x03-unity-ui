using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    private Camera mainCamera;
    private Vector3 cameraOffset;

    private int score = 0;
    public Text scoreText;

    public int health = 5;
    public Text healthText;
    public Text winLoseText;
    public Image winLoseBG;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        cameraOffset = mainCamera.transform.position - transform.position;

        SetScoreText();
        SetHealthText();
        winLoseText.text = "";
        winLoseBG.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, 0f, moveZ) * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
        mainCamera.transform.position = transform.position + cameraOffset;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }

        if (health <= 0)
        {
            if (!winLoseBG.gameObject.activeSelf)
            {
                winLoseText.text = "Game Over!";
                winLoseText.color = Color.white;
                winLoseBG.color = Color.red;
                winLoseBG.gameObject.SetActive(true);
                StartCoroutine(LoadScene(3));
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            score++;
            SetScoreText();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Trap"))
        {
            health--;
            SetHealthText();
        }

        if (other.CompareTag("Goal"))
        {
            // Prevent triggering twice
            if (!winLoseBG.gameObject.activeSelf)
            {
                winLoseText.text = "You Win!";
                winLoseText.color = Color.black;
                winLoseBG.color = Color.green;
                winLoseBG.gameObject.SetActive(true);

                // RESTART GAME AFTER 3 SECONDS
                StartCoroutine(LoadScene(3));
            }
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void SetHealthText()
    {
        healthText.text = "Health: " + health;
    }

    private IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}