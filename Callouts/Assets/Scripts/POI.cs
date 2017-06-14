using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POI : MonoBehaviour {

	public GameObject centralObject;
	public InterestPointConstructor[] points;
	public InterestPointGroup pointGroup;

	// Use this for initialization
	void Start () {
		pointGroup = new InterestPointGroup(centralObject, 5, points);
	}
	
	// Update is called once per frame
	void Update () {
		pointGroup.OnMove();
	}
}
