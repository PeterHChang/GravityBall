using UnityEngine;
using UnityEngine.SceneManagement;
using Obstacles; // Ensure this line is included

namespace Ball
{
    public class BallController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Vector2 startTouchPosition, endTouchPosition;
        private float minSwipeDistance = 50f; // Minimum distance to consider a valid swipe
        private int health = 3; // Total health (3 hearts)
        private ObstacleSpawner obstacleSpawner; // Reference to the ObstacleSpawner

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            UpdateHealthUI(); // Update UI at the start
            obstacleSpawner = FindObjectOfType<ObstacleSpawner>(); // Find the ObstacleSpawner in the scene
        }

        void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startTouchPosition = touch.position;
                        break;

                    case TouchPhase.Ended:
                        endTouchPosition = touch.position;
                        HandleSwipe();
                        break;
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                startTouchPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                endTouchPosition = Input.mousePosition;
                HandleSwipe();
            }
        }

        private void HandleSwipe()
        {
            Vector2 swipeDirection = endTouchPosition - startTouchPosition;

            if (swipeDirection.magnitude >= minSwipeDistance)
            {
                swipeDirection.Normalize();
                ChangeGravityBasedOnSwipe(swipeDirection);
            }
        }

        private void ChangeGravityBasedOnSwipe(Vector2 swipeDirection)
        {
            if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
            {
                ChangeGravity(swipeDirection.x > 0 ? new Vector2(1, 0) : new Vector2(-1, 0));
            }
            else
            {
                ChangeGravity(swipeDirection.y > 0 ? new Vector2(0, 1) : new Vector2(0, -1));
            }
        }

        private void ChangeGravity(Vector2 newGravityDirection)
        {
            Physics2D.gravity = newGravityDirection * 9.81f;
            rb.velocity = newGravityDirection * 5f;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                TakeDamage(1);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }

        private void TakeDamage(int damage)
        {
            health -= damage;
            UpdateHealthUI();

            if (health <= 0)
            {
                GameOver();
            }
        }

        private void UpdateHealthUI()
        {
            Debug.Log("Health: " + health + " hearts remaining.");
        }

        private void GameOver()
        {
            Debug.Log("Game Over! The ball has lost all its health.");
            obstacleSpawner.MoveObstaclesOutside(); // Notify the spawner to move obstacles
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
        }
    }
}
