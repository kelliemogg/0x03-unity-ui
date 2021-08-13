using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public int health = 5;
    private int score = 0;
    public float speed = .1f;
    public Rigidbody rb;
    public Text scoreText;
    public Text healthText;
    public Text WinLoseText;
    public Image WinLoseBG;
    public GameObject readyplay;

    /// gets rb (rigidbody) for GameObject
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        // GetAxis detects the user input (WASD or arrows)
        // It then assigns the numbers based on that input
        float xDirection = Input.GetAxis("Horizontal");
        float zDirection = Input.GetAxis("Vertical");

        // Vector3 is a variable that takes in 3 inputs (x, y, z)
        Vector3 moveDirection = new Vector3(xDirection, 0.0f, zDirection);

        rb.AddForce(moveDirection * speed);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            score += 1;
            SetScoreText();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Trap")
        {
            health -= 1;
            SetHealthText();
            /// Debug.Log("Health: " + health);
        }
        if (other.gameObject.tag == "Goal")
        {
            WinLoseText.text = "You Win!";
            WinLoseText.color = Color.black;
            WinLoseBG.color = Color.green;
            /// Debug.Log("You win!");
        }
    }
    void Update()
    {
        if (health == 0)
        {
            /// Debug.Log("Game Over!");
            readyplay.SetActive(true);
            WinLoseText.text = "Game Over!";
            WinLoseText.color = Color.white;
            WinLoseBG.color = Color.red;
            StartCoroutine(LoadScene(3));
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
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
    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(3);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
