
using UnityEngine;

public class InterestPointGroup
{
	GameObject c;
	float radius;
	public float radiusScale;
	public InterestPoint[] points;
	public Camera camera;

	public InterestPointGroup(GameObject go, float rScale, params InterestPoint[] ps)
	{
		radiusScale = rScale;
		centralObject = go;
		points = ps;
		camera = Camera.main;
		OnMove();
	}
	public InterestPointGroup(GameObject go, float rScale, params InterestPointConstructor[] psc)
	{
		radiusScale = rScale;
		centralObject = go;
		camera = Camera.main;

		points = new InterestPoint[psc.Length];
		for (int i = 0; i < psc.Length; i++)
		{
			points[i] = new InterestPoint(psc[i]);
		}
		OnMove();
	}

	public void OnMove()
	{
		Vector3 v;
		Vector3 cScreenSpace = camera.WorldToViewportPoint(c.transform.position);
		foreach (InterestPoint p in points)
		{
			v = p.CirclePosition(camera, c, radius);
			v = camera.WorldToViewportPoint(v);
			InterestPoint.MoveLabel(p, v, v - cScreenSpace);
		}
	}

	public GameObject centralObject
	{
		set { c = value; radius = radiusScale * c.GetComponent<MeshFilter>().mesh.bounds.extents.magnitude; }
		get { return c; }
	}
}