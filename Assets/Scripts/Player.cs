using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myRigidBody2D;

    [SerializeField] private GameObject explosionReference;
    [SerializeField] private GameObject projectileReference;

    void Awake()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        GameManager.instance.SetPlayerAlive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsPlayerAlive() && !GameManager.instance.GamePaused())
        {
            PlayerMove();
            PlayerShoot();

            PlayerDrawVectors(); // only in Debug Mode
        }
    }

    void PlayerMove()
    {
        Vector3 position = transform.position;
        float rotation_input = Input.GetAxisRaw("Horizontal");
        
        // Rotation
        if (rotation_input > 0)
            transform.RotateAround(position, -Vector3.forward, GameManager.PLAYER_ROTATION_SPEED * Time.deltaTime);
        if (rotation_input < 0)
            transform.RotateAround(position, Vector3.forward, GameManager.PLAYER_ROTATION_SPEED * Time.deltaTime);
        else  // needed to avoid passive rotation after collision
            myRigidBody2D.angularVelocity = 0f;
        
        // Boost
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            myRigidBody2D.AddForce(GameManager.PLAYER_BOOST_SPEED * Time.deltaTime * transform.up);
        
            // we change the pitch of the boost sound based on the dot product
            // between the actual velocity of the ship and its acceleration direction
            float dot_product = Vector2.Dot(myRigidBody2D.velocity.normalized, transform.up);
            float pitch_value = (dot_product / 2) + 1;
            FindObjectOfType<AudioManager>().SetPitch(AudioManager.BOOST_SFX, pitch_value);
        }
    }

    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<AudioManager>().Play(AudioManager.SHOOT_SFX);

            GameObject newProjectile;
            // Create a new projectile...
            newProjectile = Instantiate(projectileReference);
            // ...and place it in front of the ship, with the right orientation
            newProjectile.transform.position = transform.position + (transform.up * 0.5f);
            newProjectile.transform.rotation = transform.rotation;

            // We pass orientation and direction to the new Projectile
            newProjectile.GetComponent<Projectile>().SetDirection(transform.up);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameManager.CAGE_TAG) || collision.CompareTag(GameManager.METEORITE_TAG))
        {
            GameManager.instance.SetPlayerAlive(false);

            FindObjectOfType<AudioManager>().Play(AudioManager.EXPLOSION_SFX);
            FindObjectOfType<AudioManager>().Stop(AudioManager.BOOST_SFX);

            // Ship explosion animation
            GameObject explosion;
            explosion = Instantiate(explosionReference);
            explosion.transform.position = transform.position;
            explosion.transform.localScale = new Vector3(0.05f, 0.05f, 1f);

            myRigidBody2D.velocity = Vector2.zero;
            Destroy(gameObject);
        }
    }

    void PlayerDrawVectors()
    {
        if (GameManager.instance.InDebugMode())
        {
            // Shoot/Facing direction
            Debug.DrawRay(transform.position, transform.up * 10, Color.white);
            // Velocity
            Vector3 vel = myRigidBody2D.velocity;
            Debug.DrawLine(transform.position, transform.position + vel, Color.red);
        }
    }
}
