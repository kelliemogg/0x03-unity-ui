using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int health = 5;
    private int score = 0;
    public float speed = .1f;
    private Rigidbody rb;

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
            Debug.Log("Score: " + score);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Trap")
        {
            health -= 1;
            Debug.Log("Health: " + health);
        }
        if (other.gameObject.tag == "Goal")
        {
            Debug.Log("You win!");
        }
    }
    void Update()
    {
        if (health == 0)
        {
            Debug.Log("Game Over!");
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
