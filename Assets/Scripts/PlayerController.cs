﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        UpdateCountText();
        winText.text = "";
    }

    // Called before applying any physics calculations
    private void FixedUpdate()
    {
        OVRInput.Update();

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        Vector3 ovrMovement = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
        rb.AddForce(ovrMovement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            UpdateCountText();
        }
    }

    private void UpdateCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 13)
        {
            winText.text = "Great news! You win!";
        }
    }
}
