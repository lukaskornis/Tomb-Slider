using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public float moveSpeed = 20;
    public LayerMask wallLayer;
    Rigidbody2D rb;
    Vector2 input = Vector2.right;
    public GameObject landParticles;
    bool hasLanded;
    public RuleTile coinTile;
    public Tilemap tilemap;

    Sprite coinSprite;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coinSprite = coinTile.m_DefaultSprite;
        coinTile.m_DefaultSprite = null;
        tilemap.RefreshAllTiles();
    }

    void OnDestroy()
    {
        coinTile.m_DefaultSprite = coinSprite;
    }

    void Update()
    {
        var newInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


        // land particles
        if (rb.velocity.magnitude < 0.1f && !hasLanded)
        {
            hasLanded = true;
            var obj = Instantiate( landParticles, transform.position, Quaternion.identity);
            obj.transform.up = transform.up;
        }


        var hit = Physics2D.Raycast(transform.position, input, 1f, wallLayer);
        if (newInput != Vector2.zero && hit.collider != null && newInput != input)
        {
            input = newInput;
            transform.up = -input;
            hasLanded = false;
        }

        rb.velocity = input * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
    }
}