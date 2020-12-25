using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLauncher : MonoBehaviour
{
    public float LaunchVelocity = 5f;
    public float RotationSpeed = 2f;


    int launchstage = 0;
    bool launched = false;
    Rigidbody2D rb;
    Text speedtext;
    Transform arrow;


    void Start()
    {
        rb = GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        speedtext = Camera.main.GetComponentInChildren(typeof(Text)) as Text;
        arrow = transform.GetChild(0).transform;
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

        switch (launchstage)
        {
            default:
                transform.rotation = Quaternion.Euler(0f, 0f, (35f * Mathf.Sin(Time.time * RotationSpeed)) + 45);
                if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
                {
                    launchstage = 1;
                    
                }
                break;
            case 1:
                arrow.localScale = new Vector3(Mathf.Sin(Time.time * RotationSpeed)+1,1f,1f);
                //arrow.localPosition = new Vector3(Mathf.Sin(Time.time * RotationSpeed) +1, 0f, 0f);
                break;
        }

        if (Input.GetMouseButtonDown(0) && !launched && launchstage == 2)
        {
            rb.WakeUp();
            rb.velocity = rb.transform.right * LaunchVelocity;
            launched = true;
        }

        speedtext.text = "Speed: " + Mathf.RoundToInt(rb.velocity.magnitude).ToString() + " m/s";

    }
}
