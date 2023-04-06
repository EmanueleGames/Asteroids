using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public enum Dimension
    {
        BIG = 1,
        MEDIUM = 2,
        SMALL = 3,
    }
    private Dimension size;
    
    private float speed;
    private Vector2 direction;

    private Rigidbody2D myRigidBody2D;

    [SerializeField] private GameObject explosionReference;

    // Sets and Gets
    public void SetSpeed(float newSpeed)
        { speed = newSpeed; }
    public void SetDirection(Vector2 newDirection)
        { direction = newDirection; }
    public Dimension GetDimension()
        { return size; }
    public void SetDimension(Dimension newDimension)
        { size = newDimension; }

    void Awake()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        size = Dimension.BIG;
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2D.AddForce(direction * speed);
        myRigidBody2D.AddTorque(Random.Range(-10, 10));
    }

    // Update is called once per frame
    void Update()
    {
        MeteoriteVectors(); // Only in Debug Mode
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameManager.CAGE_TAG))
        {
            // Stop the object, avoid bounce
            myRigidBody2D.velocity = Vector2.zero;

            // If we want breaking sound when hitting cage
            //FindObjectOfType<AudioManager>().Play("explosion");

            if (size != Dimension.SMALL)
                SplitMeteorite(transform, 75);
            else
            {
                // The smallest meteorite explode when destroyed
                GameObject explosion;
                explosion = Instantiate(explosionReference);
                explosion.transform.position = transform.position;
                explosion.transform.localScale = new Vector3(0.08f, 0.08f, 1f);
            }

            // When splitting is over, destroy the meteorite
            Destroy(gameObject);
        }

        if (collision.CompareTag(GameManager.PROJECTILE_TAG))
        {
            // Stop the object, avoid bounce
            myRigidBody2D.velocity = Vector2.zero;

            // Destroy the projectile to avoid piercing
            Destroy(collision.gameObject);

            // Meteorite breaking sound when shot
            FindObjectOfType<AudioManager>().Play(AudioManager.EXPLOSION_SFX);

            // Add points to the Player Score
            GameManager.instance.AddPoints(GameManager.METEORITE_POINTS_VALUE);

            if (size != Dimension.SMALL)
                SplitMeteorite(transform, 180);
            else
            {
                // The smallest meteorite explode when destroyed
                GameObject explosion;
                explosion = Instantiate(explosionReference);
                explosion.transform.position = transform.position;
                explosion.transform.localScale = new Vector3(0.08f, 0.08f, 1f);
            }

            // When splitting is over, destroy the meteorite
            Destroy(gameObject);
        }
    }

    private void SplitMeteorite(UnityEngine.Transform transform, int maxDirectionAngle)
    {
        float randomDeviationAngle;
        Vector2 meteoriteCollisionPos = transform.position;
        // Get direction from collision point to the center of the cage
        Vector2 directionTowardsOrigin = (Vector2.zero - meteoriteCollisionPos).normalized;
        // Generate a random direction from the one towards the middle, using an angle not larger than MaxDirectionAngle
        randomDeviationAngle = Random.Range(-maxDirectionAngle, maxDirectionAngle);
        Vector2 newRandomDirection1 = Quaternion.Euler(0, 0, randomDeviationAngle) * directionTowardsOrigin;
        // Generate a random direction from the one towards the middle, using an angle not larger than MaxDirectionAngle
        randomDeviationAngle = Random.Range(-maxDirectionAngle, maxDirectionAngle);
        Vector2 newRandomDirection2 = Quaternion.Euler(0, 0, randomDeviationAngle) * directionTowardsOrigin;

        // Explosion animation before splitting
        GameObject explosion;
        explosion = Instantiate(explosionReference);
        explosion.transform.position = transform.position;

        GameObject spawnedMeteorite1, spawnedMeteorite2;
        // Create a new meteorites...
        spawnedMeteorite1 = Instantiate(gameObject);
        spawnedMeteorite2 = Instantiate(gameObject);
        // ...and place it on the spawner
        spawnedMeteorite1.transform.position = meteoriteCollisionPos;
        spawnedMeteorite2.transform.position = meteoriteCollisionPos;

        float minSpeed = 0, maxSpeed = 0;
        // We scale the new Meteorite and set speed and direction
        switch (size)
        {
            case Dimension.BIG:
                minSpeed = GameManager.METEORITE_MEDIUM_MIN_SPEED;
                maxSpeed = GameManager.METEORITE_MEDIUM_MAX_SPEED;
                spawnedMeteorite1.GetComponent<Meteorite>().transform.localScale = new Vector3(0.1f, 0.1f, 1f);
                spawnedMeteorite1.GetComponent<Meteorite>().SetDimension(Dimension.MEDIUM);
                spawnedMeteorite2.GetComponent<Meteorite>().transform.localScale = new Vector3(0.1f, 0.1f, 1f);
                spawnedMeteorite2.GetComponent<Meteorite>().SetDimension(Dimension.MEDIUM);
                // explosion animation scaling
                explosion.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
                break;
            case Dimension.MEDIUM:
                minSpeed = GameManager.METEORITE_SMALL_MIN_SPEED;
                maxSpeed = GameManager.METEORITE_SMALL_MAX_SPEED;
                spawnedMeteorite1.GetComponent<Meteorite>().transform.localScale = new Vector3(0.05f, 0.05f, 1f);
                spawnedMeteorite1.GetComponent<Meteorite>().SetDimension(Dimension.SMALL);
                spawnedMeteorite2.GetComponent<Meteorite>().transform.localScale = new Vector3(0.05f, 0.05f, 1f);
                spawnedMeteorite2.GetComponent<Meteorite>().SetDimension(Dimension.SMALL);
                // explosion animation scaling
                explosion.transform.localScale = new Vector3(0.15f, 0.15f, 1f);
                break;
            default:
                break;
        }
        spawnedMeteorite1.GetComponent<Meteorite>().SetSpeed(Random.Range(minSpeed, maxSpeed));
        spawnedMeteorite2.GetComponent<Meteorite>().SetSpeed(Random.Range(minSpeed, maxSpeed));
        spawnedMeteorite1.GetComponent<Meteorite>().SetDirection(newRandomDirection1);
        spawnedMeteorite2.GetComponent<Meteorite>().SetDirection(newRandomDirection2);
    }

    void MeteoriteVectors()
    {
        if (GameManager.instance.InDebugMode())
        {
            // Velocity
            Vector3 vel = myRigidBody2D.velocity;
            Debug.DrawLine(transform.position, transform.position + vel, Color.magenta);
        }
    }
}
