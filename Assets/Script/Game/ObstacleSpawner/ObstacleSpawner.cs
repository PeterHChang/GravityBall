using UnityEngine;

namespace Obstacles
{
    public class ObstacleSpawner : MonoBehaviour
    {
        public GameObject obstaclePrefab; // Reference to the obstacle prefab
        public float spawnTime = 2f; // Time interval between spawns
        public float lifetime = 1.5f; // How long the obstacle will exist

        // Define the borders of your game area
        public float minX = -4.5f; // Minimum X boundary
        public float maxX = 4.5f;  // Maximum X boundary
        public float minY = -4.5f; // Minimum Y boundary
        public float maxY = 4.5f;  // Maximum Y boundary

        void Start()
        {
            InvokeRepeating("SpawnObstacle", 0f, spawnTime); // Start spawning obstacles
        }

        void SpawnObstacle()
        {
            // Generate a random position within the defined borders
            Vector2 randomPosition = new Vector2(
                Random.Range(minX, maxX), // Use defined min and max values
                Random.Range(minY, maxY)   // Use defined min and max values
            );

            // Log the position for debugging
            Debug.Log("Spawning obstacle at: " + randomPosition);

            // Instantiate the obstacle at the random position
            GameObject newObstacle = Instantiate(obstaclePrefab, randomPosition, Quaternion.identity);
            
            // Destroy the obstacle after a certain time
            Destroy(newObstacle, lifetime);
        }

        public void MoveObstaclesOutside()
        {
            // Find all obstacles in the scene and move them outside the borders
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach (GameObject obstacle in obstacles)
            {
                // Move the obstacle outside the visible area
                obstacle.transform.position = new Vector2(100f, 100f); // Move to a position outside the game area
            }
        }
    }
}
