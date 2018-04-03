using UnityEngine;
using System.Collections;

public class RTSCam : MonoBehaviour
{
    public float scrollZone = 30;
    public float scrollSpeed = 5;

    public float xMax = 8;
    public float xMin = 0;
    public float zMax = 10;
    public float zMin = 3;

    public float yMax = 8;
    public float yMin = 0; // For Limiting Movement

    private Vector3 desiredPosition;

    private void Start()
    {
        desiredPosition = transform.position; // Initial camera position
    }
    private void Update()
    {
        float x = 0, y = 0, z = 0;
        float speed = scrollSpeed * Time.deltaTime;
        if (Input.mousePosition.x < scrollZone)
            x -= speed;
        else if (Input.mousePosition.x > Screen.width - scrollZone)
            x += speed;

        if (Input.mousePosition.y < scrollZone)
            y -= speed;
        else if (Input.mousePosition.y > Screen.height - scrollZone)
            y += speed;

        z += Input.GetAxis("Mouse ScrollWheel");

        // Up, Down, Left and right camera movement completed

        Vector3 move = new Vector3(x, y, z) + desiredPosition;
        move.x = Mathf.Clamp(move.x, xMin, xMax);
        move.y = Mathf.Clamp(move.y, yMin, yMax);
        move.z = Mathf.Clamp(move.z, zMin, zMax);
        desiredPosition = move;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.2f);

    }
}
