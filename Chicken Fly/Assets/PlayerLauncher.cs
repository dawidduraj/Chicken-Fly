using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLauncher : MonoBehaviour
{
    public float LaunchVelocity = 5f;
    public float RotationSpeed = 2f;


    Text speedtext;
    bool rotationLock = false;
    bool launched = false;
    Rigidbody2D rb;


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
        
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && !rotationLock)
        {
            rotationLock = true;
            return;
        }
        if (!rotationLock)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, (35f * Mathf.Sin(Time.time * RotationSpeed)) + 45);
        }

        if (Input.GetMouseButtonDown(0) && !launched && rotationLock)
        {
            rb.WakeUp();
            rb.velocity = rb.transform.right * LaunchVelocity;
            launched = true;
        }

        speedtext.text = "Speed: " + Mathf.RoundToInt(rb.velocity.magnitude).ToString() + " m/s";

    }
}
