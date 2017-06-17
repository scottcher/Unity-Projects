using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlimpController : MonoBehaviour {

    float timeCounter;

    [Tooltip("Move speed.")]
    public float Speed = 0.2f;

    [Tooltip("length of the curve to move along.")]
    public float Length = 4.0f;

    [Tooltip("width of the curve to move along.")]
    public float Width = 4.0f;

    [Tooltip("The center hub around which orbiting occurs.")]
    GameObject Hub;

    Vector3 centerPosition;
    Vector3 lookPosition;
    Quaternion lookRotation;

	// Use this for initialization
	void Start () {
        timeCounter = 0.0f;
        centerPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        positionMe();
        rotateMe();
    }

    void positionHub()
    {
        // check to make sure the hub was wired up - return early if not
        if (Hub == null) return;

        // the hub is a sibling to the blimp container within the stage so get it and re-position it
        // to the centerPosition of rotation
        Hub.transform.position = centerPosition;
    }

    // Position change to rotate around a center position
    void positionMe()
    {
        timeCounter += Time.deltaTime * Speed;
        
        float x = Mathf.Cos(timeCounter) * Width + centerPosition.x;
        float z = Mathf.Sin(timeCounter) * Length + centerPosition.z;
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
