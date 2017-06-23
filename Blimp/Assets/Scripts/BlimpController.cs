using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlimpController : MonoBehaviour {

    float timeCounter;

    [Tooltip("Flight speed.")]
    public float Speed = 0.2f;

    [Tooltip("Flightpath semi-major radius.")]
    public float RadiusSemiMajor = 4.0f;

    [Tooltip("Flightpath semi-minor radius.")]
    public float RadiusSemiMinor = 4.0f;

    [Tooltip("The center hub around which orbiting occurs.")]
    public GameObject Hub;

    Vector3 centerPosition;
    Vector3 lookPosition;
    Quaternion lookRotation;

	// Use this for initialization
	void Start () {
        timeCounter = 0.0f;
        getHubPosition();
    }
	
	// Update is called once per frame
	void Update () {
        positionMe();
        rotateMe();

        // its possible the center of rotation, the hub position, has moved, so check and update
        getHubPosition();
    }

    void getHubPosition()
    {
        // check to make sure the hub was wired up - return early if not
        if (Hub == null) return;

        // its possible the center of rotation, the hub position, has moved, so check and update
        centerPosition = Hub.transform.position;
    }

    // Position change to rotate around a center position
    void positionMe()
    {
        timeCounter += Time.deltaTime * Speed;
        
        float x = Mathf.Cos(timeCounter) * RadiusSemiMinor + centerPosition.x;
        float z = Mathf.Sin(timeCounter) * RadiusSemiMajor + centerPosition.z;
        float y = 0.0f + centerPosition.y;

        transform.position = new Vector3(x, y, z);
    }

    // Rotation change to face the object tagent to motion
    void rotateMe()
    {
        lookPosition = centerPosition - transform.position;
        lookRotation = Quaternion.LookRotation(lookPosition);

        transform.rotation = lookRotation;
    }
}
