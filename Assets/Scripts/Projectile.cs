using UnityEngine;

public class Projectile : MonoBehaviour
{
    private const float speed = 10f;
    private Vector2 direction;

    // Sets and Gets
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 tempPos = transform.position;
        tempPos += speed * Time.deltaTime * direction;
        transform.position = tempPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameManager.CAGE_TAG))
        {
            // Cage stops projectiles
            Destroy(gameObject);
        }
    }
}
