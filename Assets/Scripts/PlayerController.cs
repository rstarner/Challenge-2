using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float moveForce = 100f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Text countText;
    public Text winText;
    public bool facingRight = true;

    private int count;

    // Start is called before the first frame update
    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();

        count = 0;

        winText.text = "";

        SetCountText();
    }

    // Update is called once per frame
    void Update()
    {

    }



    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");


        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (Input.GetKey("escape"))
            Application.Quit();

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Pickup"))

            other.gameObject.SetActive(false);

        count = count + 1;

        SetCountText();

        if (other.gameObject.CompareTag("Enemy"))
            Destroy(other.gameObject);








    }



    void SetCountText()
    {

        countText.text = "Score: " + count.ToString();

        if (count >= 4)

            winText.text = "You win!";


    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }

}