using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeteoriteSpawner : MonoBehaviour
{
    [SerializeField] private GameObject meteoriteReference;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform[] spawners;

    private const float PLAYER_MIN_DIST_FROM_SPAWNER = 8f;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SpawnMeteorite());
    }

    private void OnEnable()
    {
        // We subscribe the function resposable of the meteorites spawning
        // to the event that triggers when the level finishes loading
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    private void OnDisable()
    {
        // unsub from the event
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    // Update is called once per frame
    void Update()
    {
        DrawSpawners(); // only in Debug Mode
    }

    private void OnLevelFinishedLoading(Scene scene_loaded, LoadSceneMode mode)
    {
        if (scene_loaded.name == GameManager.GAMEPLAY_SCENE_TAG)
            StartCoroutine(SpawnMeteorite());
    }

    IEnumerator SpawnMeteorite(){

        float minDelay = GameManager.MIN_SPAWN_DELAY;
        float maxDelay = GameManager.MAX_SPAWN_DELAY;

        float minSpeed = GameManager.METEORITE_BIG_MIN_SPEED;
        float maxSpeed = GameManager.METEORITE_BIG_MAX_SPEED;

        float distanceFromPlayer;
        int randomSpawnerIndex;
        float randomAngle;

        while (true) {

            // Wait for a random Delay in the interval
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            if (GameManager.instance.IsPlayerAlive())
            {
                // Pick a random spawner among the 8
                randomSpawnerIndex = Random.Range(0, spawners.Length);

                // Check if player is on top of spawner
                distanceFromPlayer = (spawners[randomSpawnerIndex].transform.position - playerTransform.position).magnitude;
                if (distanceFromPlayer < PLAYER_MIN_DIST_FROM_SPAWNER)
                {
                    if (GameManager.instance.InDebugMode())
                    {
                        Debug.Log("<color=red>ERROR</color> spawning from position " + (randomSpawnerIndex + 1) + ", player too close: " + distanceFromPlayer + " units");
                    }
                }
                else
                {
                    // Pick a random direction pointing away from the cage (0-130 degrees)
                    randomAngle = Random.Range(0, 130);

                    if (GameManager.instance.InDebugMode())
                    {
                        Debug.Log("<color=green>SUCCESS</color> spawning from position: " + (randomSpawnerIndex + 1) + ", and angle: " + randomAngle);
                    }

                    GameObject spawnedMeteorite;
                    // Create a new meteorite...
                    spawnedMeteorite = Instantiate(meteoriteReference);
                    // ...and place it on the spawner
                    spawnedMeteorite.transform.position = spawners[randomSpawnerIndex].position;

                    // Using randomAngle, we create a starting direction for the new Meteorite
                    Vector2 spawnerRightDir = spawners[randomSpawnerIndex].transform.right;
                    Vector2 meteoriteDir = Quaternion.Euler(0, 0, randomAngle) * spawnerRightDir;

                    // We pass speed and direction to the new Meteorite
                    spawnedMeteorite.GetComponent<Meteorite>().SetSpeed(Random.Range(minSpeed, maxSpeed));
                    spawnedMeteorite.GetComponent<Meteorite>().SetDirection(meteoriteDir);
                }
            }
        } //while
    }

    void DrawSpawners()
    {
        if (GameManager.instance.InDebugMode())
        {
            for (int i = 0; i < spawners.Length; i++)
            {
                Vector3 spawnerPosition = spawners[i].transform.position;
                Vector3 rightDirection = spawners[i].transform.right;
                Vector3 maxAngleDirection = Quaternion.Euler(0, 0, 135) * rightDirection;
                Debug.DrawLine(spawnerPosition, spawnerPosition + rightDirection * 5, Color.yellow);
                Debug.DrawLine(spawnerPosition, spawnerPosition + maxAngleDirection * 5, Color.yellow);
                
            }
        }
    }
}
