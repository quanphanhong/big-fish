using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 screenSize;
    float horizontalInput = 0f, verticalInput = 0f;
    Vector3 movingVector;
    [SerializeField] float movingSpeed = 5f;
    [SerializeField] Camera cam;



    // Start is called before the first frame update
    void Start()
    {
        screenSize = new Vector2(Screen.width, Screen.height);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse movement delta
        horizontalInput = Input.GetAxis("Mouse X");
        verticalInput = Input.GetAxis("Mouse Y");

        // Update player position
        movingVector = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(movingVector * movingSpeed * Time.deltaTime);
        
        Vector3 screenSizeInWorld = cam.ScreenToWorldPoint(screenSize);
        // Make sure player is within the screen
        Vector3 validatedPosition = new Vector3(
            Mathf.Clamp(transform.position.x, -screenSizeInWorld.x, screenSizeInWorld.x),
            Mathf.Clamp(transform.position.y, -screenSizeInWorld.y, screenSizeInWorld.y),
            0
        );
        transform.position = validatedPosition;
    }
}
