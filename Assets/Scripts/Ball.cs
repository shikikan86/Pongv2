using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public float speed = 5f;

    public Rigidbody rb;

    public Vector3 v = new Vector3(0f, 0f, 0f);

    public int p1score;
    public int p2score;

    public Text display1;
    public Text display2;

    public float x;
    public float y;


    // Start is called before the first frame update
    void Start()
    {
        float startx = Random.Range(0, 2) == 0 ? -1 : 1;
        float starty = Random.Range(0, 2) == 0 ? -1 : 1;

        rb = GetComponent<Rigidbody>();

        rb.velocity = new Vector3(speed * startx, speed * starty, 0f);

        p1score = 0;
        p2score = 0;

        display1.text = p1score.ToString();
        display2.text = p2score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //speed = speed + 0.0001f;
        x = rb.velocity.x;
        y = rb.velocity.y;

        //v = (x, y, 0f);
        

        if(this.transform.position.x < -20)
        {
            this.transform.position = new Vector3(0f, 0f, 0f);
            rb.velocity = new Vector3(-5f, -5f, 0f);
            p2score = p2score + 1;
            speed = 5f;
            display2.text = p2score.ToString();
            Debug.Log("Player 2 scored 1 point. Total: " + p1score + " - " + p2score);
        }

        if (this.transform.position.x > 20)
        {
            this.transform.position = new Vector3(0f, 0f, 0f);
            rb.velocity = new Vector3(5f, 5f, 0f);
            p1score = p1score + 1;
            speed = 5f;
            display1.text = p1score.ToString();
            Debug.Log("Player 1 scored 1 point. Total: " + p1score + " - " + p2score);

        }

      
        if (p1score > 10)
        {
            p1score = 0;
            p2score = 0;
            Debug.Log("Player 1 Wins!");
            rb.velocity = new Vector3(0f, 0f, 0f);
        }

        if(p2score > 10)
        {
            p1score = 0;
            p2score = 0;
            Debug.Log("Player 2 Wins!");
            rb.velocity = new Vector3(0f, 0f, 0f);
        }



    }

    void OnCollisionEnter(Collision collision)
    {
        speed = speed + 0.5f;

        //this keeps the ball moving in the same direction on the y axis
        float yspeed;
        if (y < 0)
        {
            yspeed = -speed;
        }
        else
        {
            yspeed = speed;
        }
        if (collision.collider.name == "Paddle1")
        {
            rb.velocity = new Vector3(speed, yspeed, 0f);
        }
        if (collision.collider.name == "Paddle2")
        {
            rb.velocity = new Vector3(-speed, yspeed, 0f);
        }


    }
}
