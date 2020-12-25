using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLauncher : MonoBehaviour
{
    bool launched = false;
    public float LaunchVelocity = 5f;
    public Text veltext;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.Sleep();
    }

    // Update is called once per frame
    void Update()
    {
        Launch();
    }

    void Launch()
    {
        if (Input.GetMouseButtonDown(0) && !launched)
        {
            rb.WakeUp();
            rb.velocity = new Vector2(LaunchVelocity, LaunchVelocity);
            launched = true;
        }
        veltext.text = Mathf.RoundToInt(rb.velocity.magnitude).ToString() + " m/s";

    }
}
