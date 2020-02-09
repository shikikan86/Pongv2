using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(new Vector3(100f, 100f, 0f) * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(100f, 100f, 0f) * Time.deltaTime);
    }

    //Makes powerup disappear. I should verify that the ball is the object hitting it but
    //I ignore it because the ball is the only moving thing for now.
    void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
    }
}
