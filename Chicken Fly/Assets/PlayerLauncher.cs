using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLauncher : MonoBehaviour
{
    public float LaunchVelocity = 5f;
    public float RotationSpeed = 2f;

    float startingPosition;
    [HideInInspector]
    public bool launched = false;
    int launchstage = 0;
    Rigidbody2D rb;
    Text speedtext;
    Transform arrow;


    void Start()
    {
        startingPosition = transform.position.x;
        rb = GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        speedtext = Camera.main.GetComponentInChildren(typeof(Text)) as Text;
        arrow = transform.GetChild(0).transform;
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update() 
    {
        Launch();
        Land();
    }

    void Land()
    {

    }

    void Launch()
    {
        if(rb == null)
        {
            return;
        }
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && launchstage != 2)
        {
            launchstage ++;

        }
        if (!launched)
        {
            switch (launchstage)
            {
                default:
                    transform.rotation = Quaternion.Euler(0f, 0f, (35f * Mathf.Sin(Time.time * RotationSpeed)) + 45);
                    break;
                case 1:
                    arrow.localScale = new Vector3(Mathf.Sin(Time.time * RotationSpeed) + 2f, 1f, 1f);
                    //TODO: Fix sizing
                    //arrow.localPosition = new Vector3(Mathf.Sin(Time.time * RotationSpeed) +1, 0f, 0f);
                    break;
                case 2:
                    rb.isKinematic = false;
                    rb.velocity = transform.right * (LaunchVelocity * arrow.transform.localScale.x);
                    launched = true;
                    arrow.gameObject.SetActive(false);
                    break;

            }
        }
        speedtext.text = "Speed: " + Mathf.RoundToInt(rb.velocity.magnitude).ToString() + " m/s Distance: " + Mathf.RoundToInt(transform.position.x - startingPosition).ToString() + " m";

    }
    void LateUpdate()
    {
        if (launched)
        {
            Camera.main.transform.parent = transform;
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition,new Vector3(0,0,-10f),Time.deltaTime * 2f);
            Camera.main.transform.rotation = Quaternion.identity;
        }
    }

}

