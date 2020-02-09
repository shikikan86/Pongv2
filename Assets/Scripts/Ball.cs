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

    //Text fields
    public Text display1;
    public Text display2;
    public Text display3;

    //Ball's x and y vector components
    public float x;
    public float y;

    //Used later for changing colors when the ball is hit
    public Material[] material;
    public Color[] scoreColors;
    Renderer rend;

    //Audio array to play one of 4 random hit sound effects, and some other sound effects
    public AudioSource source;
    public AudioClip[] clips;
    public AudioClip zap;
    public AudioClip wilhelm;

    private TrailRenderer tr;

    public AnimationCurve curve = new AnimationCurve();
    public AnimationCurve defaultCurve = new AnimationCurve();

    // Start is called before the first frame update
    void Start()
    {
        float startx = Random.Range(0, 2) == 0 ? -1 : 1;
        float starty = Random.Range(0, 2) == 0 ? -1 : 1;

        rb = GetComponent<Rigidbody>();

        rb.velocity = new Vector3(speed * startx, speed * starty, 0f);

        p1score = 0;
        p2score = 0;

        //Where the scores will be updated on screen
        display1.text = p1score.ToString();
        display2.text = p2score.ToString();
        display3.text = "";

        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];

        source = GetComponent<AudioSource>();

        //Creates the curve for when the ball gets bigger from a power up
        tr = GetComponent<TrailRenderer>();
        curve.AddKey(0.0f, 1.0f);
        curve.AddKey(1.0f, 0.0f);

        //For returning to default curve
        defaultCurve.AddKey(0.0f, 0.54f);
        defaultCurve.AddKey(0.051f, 0.62f);
        defaultCurve.AddKey(1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        x = rb.velocity.x;
        y = rb.velocity.y;
        
        //Player 2 Scores
        if(this.transform.position.x < -20)
        {
            this.transform.position = new Vector3(0f, 0f, 0f);
            rb.velocity = new Vector3(-5f, -5f, 0f);
            p2score = p2score + 1;
            speed = 5f;
            display2.text = p2score.ToString();
            display2.color = scoreColors[p2score-1];
            Debug.Log("Player 2 scored 1 point. Total: " + p1score + " - " + p2score);
            normalSize();
            tr.Clear();
            tr.widthCurve = defaultCurve;
            source.clip = wilhelm;
            source.PlayOneShot(source.clip);
        }

        //Player 1 Scores
        if (this.transform.position.x > 20)
        {
            this.transform.position = new Vector3(0f, 0f, 0f);
            rb.velocity = new Vector3(5f, 5f, 0f);
            p1score = p1score + 1;
            speed = 5f;
            display1.text = p1score.ToString();
            display1.color = scoreColors[p1score - 1];
            Debug.Log("Player 1 scored 1 point. Total: " + p1score + " - " + p2score);
            normalSize();
            tr.Clear();
            tr.widthCurve = defaultCurve;
            source.clip = wilhelm;
            source.PlayOneShot(source.clip);
        }

      //Reset score if either player wins and hold ball in place
        if (p1score > 10)
        {
            tr.Clear();
            p1score = 0;
            p2score = 0;
            Debug.Log("Player 1 Wins!");
            rb.velocity = new Vector3(0f, 0f, 0f);
            display1.text = p1score.ToString();
            display2.text = p2score.ToString();
            display3.text = "Player 1 Wins!";
        }

        if(p2score > 10)
        {
            tr.Clear();
            p1score = 0;
            p2score = 0;
            Debug.Log("Player 2 Wins!");
            rb.velocity = new Vector3(0f, 0f, 0f);
            display1.text = p1score.ToString();
            display2.text = p2score.ToString();
            display3.text = "Player 2 Wins!";
        }



    }

    void OnCollisionEnter(Collision collision)
    {
        speed = speed + 0.5f;

        //this keeps the ball moving in the same direction on the y axis when it hits the paddle
        float yspeed;
        if (y < 0)
        {
            yspeed = -speed;
        }
        else
        {
            yspeed = speed;
        }

        if(collision.collider.name != "Paddle1" && collision.collider.name != "Paddle2")
        {
            source.clip = clips[Random.Range(0, clips.Length)];
            source.PlayOneShot(source.clip);
        }
        if (collision.collider.name == "Paddle1")
        {
            
            //Makes it so the ball moves opposite of the paddle it just hit
            rb.velocity = new Vector3(speed, yspeed, 0f);
            rend.sharedMaterial = material[1];
            //Play Random punch sound
            //source.clip = clips[Random.Range(0, clips.Length)];
            //source.PlayOneShot(source.clip);
            if(speed >= 9.0f)
            {
                source.clip = zap;
                source.PlayOneShot(source.clip);
            }
            else
            {
                source.clip = clips[Random.Range(0, clips.Length)];
                source.PlayOneShot(source.clip);
            }
        }
        if (collision.collider.name == "Paddle2")
        {
            rb.velocity = new Vector3(-speed, yspeed, 0f);
            rend.sharedMaterial = material[2];
            //Play sound
            //source.clip = clips[Random.Range(0, clips.Length)];
            //source.PlayOneShot(source.clip);
            if (speed >= 9.0f)
            {
                source.clip = zap;
                source.PlayOneShot(source.clip);
            }
            else
            {
                source.clip = clips[Random.Range(0, clips.Length)];
                source.PlayOneShot(source.clip);
            }
        }


    }

    //When the ball hits a powerup
    void OnTriggerEnter(Collider other)
    {
        float yspeed = y;
        //Powerup1 increases the speed by a lot
        if(other.name == "Powerup1")
        {
            speed = speed + 60;
            if(x > 0)
            {
                rb.velocity = new Vector3(speed, yspeed, 0f);
            }
            else
            {
                rb.velocity = new Vector3(speed*(-1), yspeed, 0f);
            }

        }
        //Powerup2 increases the size of the ball
        if (other.name == "Powerup2")
        {
            transform.localScale *= 2;
            tr.widthCurve = curve;
        }
        else return;
    }

    void normalSize()
    {
        transform.localScale = new Vector3(.5f,.5f,.5f);
    }
}
