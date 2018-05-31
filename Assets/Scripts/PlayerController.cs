using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour {

    private static int TOTAL_PRIZES = 0;

    public float speed;
    //public Text countText;
    public Text winText;

    //public TextMeshPro countText;
    public TextMeshProUGUI countText;

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

        Vector3 keyboardMovement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Get Oculus touch controllers' thumbstick axis movement
        Vector3 ovrMovementRight = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
        Vector3 movementRight = new Vector3(ovrMovementRight.x, 0.0f, ovrMovementRight.y);
        Vector3 ovrMovementLeft = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
        Vector3 movementLeft = new Vector3(ovrMovementLeft.x, 0.0f, ovrMovementLeft.y);

        // We are allowing keyboard and Oculus touch controllers
        rb.AddForce(keyboardMovement * speed);
        rb.AddForce(movementLeft * speed);
        rb.AddForce(movementRight * speed);
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
        if (count >= TOTAL_PRIZES)
        {
            winText.text = "Great news! You win!";
        }
    }
}
