using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLauncher : MonoBehaviour
{
    public float LaunchVelocity = 5f;
    
    Text speedtext;
    bool launched = false;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        speedtext = Camera.main.GetComponentInChildren(typeof(Text)) as Text;
        if (rb != null)
        {
            rb.Sleep();
        }
    }

    // Update is called once per frame
    void Update() => Launch();

    void Launch()
    {
        if(rb == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && !launched)
        {
            rb.WakeUp();
            rb.velocity = rb.transform.right * LaunchVelocity;
            launched = true;
        }
        speedtext.text = "Speed: " + Mathf.RoundToInt(rb.velocity.magnitude).ToString() + " m/s";

    }
}
