using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour

{
    public bool isPaddle1;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaddle1)
        {
            transform.Translate(0f, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0f);
        }
        else
        {
            transform.Translate(0f, Input.GetAxis("Vertical2") * speed * Time.deltaTime, 0f);
        }


        //Fixes the paddles going out of bounds
      if(transform.position.y < -4)
        {
            
            if (isPaddle1)
            {
                this.transform.position = new Vector3(-9f, -3.99f, 0f);
            }
            else
            {
                this.transform.position = new Vector3(9f, -3.99f, 0f);
            }
            
        }

        if (transform.position.y > 4)
        {
            if (isPaddle1)
            {
                this.transform.position = new Vector3(-9f, 3.99f, 0f);
            }
            else
            {
                this.transform.position = new Vector3(9f, 3.99f, 0f);
            }
        }
            
    }
}
