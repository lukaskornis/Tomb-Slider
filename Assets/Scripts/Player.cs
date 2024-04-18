using UnityEngine;

public class Player : MonoBehaviour
{
	 public float moveSpeed = 20f;
    Rigidbody2D rb;
    Vector2 input;
    public GameObject landParticles;
    bool hasLanded;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
         var newInput = new Vector2( Input.GetAxisRaw( "Horizontal" ), Input.GetAxisRaw( "Vertical" ));
         // diagonal movement fix
         if (Mathf.Abs(newInput.x) > 0 && Mathf.Abs(newInput.y) > 0)
         {
	         newInput.y = 0;
         }

         if (rb.velocity.magnitude < 0.1f && !hasLanded && newInput != input && newInput != Vector2.zero)
         {
				 Instantiate(landParticles, transform.position, Quaternion.identity);
				 hasLanded = true;
         }

         if (newInput != Vector2.zero && rb.velocity.magnitude < 0.1f)
         {
				 input = newInput;
				 transform.up = -input;
				 hasLanded = false;
         }

         rb.velocity = input * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
	    if (other.gameObject.CompareTag("Coin"))
	    {
		    Destroy(other.gameObject);
	    }
    }
}