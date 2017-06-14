
using UnityEngine;
using UnityEngine.UI;

public class InterestPoint
{
	public string text;
	public GameObject point;
	public GameObject label;
	public LineRenderer lineRenderer;
	public RectTransform rectTransform;
	Vector3 circlePoint;

	public Text textObject;

	public InterestPoint(string s, GameObject go, GameObject l)
	{
		text = s;
		point = go;
		label = l;
		rectTransform = label.GetComponent<RectTransform>();
		rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;

		textObject = label.GetComponent<Text>();
		textObject.text = text;
		textObject.horizontalOverflow = HorizontalWrapMode.Overflow;
		textObject.verticalOverflow = VerticalWrapMode.Overflow;
		if (point.GetComponent<LineRenderer>() == null)
		{
			lineRenderer = point.AddComponent<LineRenderer>();
		}
		else
		{
			lineRenderer = point.GetComponent<LineRenderer>();
		}
		lineRenderer.SetVertexCount(2);
		lineRenderer.useWorldSpace = false;
		lineRenderer.SetPosition(0, Vector3.zero);
		lineRenderer.SetWidth(0.1f, 0.1f);
	}

	public InterestPoint(InterestPointConstructor psc)
	{
		text = psc.text;
		point = psc.gameObject;
		label = psc.label;
		rectTransform = label.GetComponent<RectTransform>();
		rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;

		textObject = label.GetComponent<Text>();
		textObject.text = text;
		textObject.horizontalOverflow = HorizontalWrapMode.Overflow;
		textObject.verticalOverflow = VerticalWrapMode.Overflow;
		if (point.GetComponent<LineRenderer>() == null)
		{
			lineRenderer = point.AddComponent<LineRenderer>();
		}
		else
		{
			lineRenderer = point.GetComponent<LineRenderer>();
		}
		lineRenderer.SetVertexCount(2);
		lineRenderer.useWorldSpace = false;
		lineRenderer.SetPosition(0, Vector3.zero);
		lineRenderer.SetWidth(0.1f, 0.1f);
	}

	public Vector3 CirclePosition(Camera cam, GameObject c, float r)
	{
		Vector3 fwd = cam.transform.forward;
		Vector3 v = point.transform.position - c.transform.position;
		v -= Vector3.Project(v, fwd);
		v = (v.normalized * r) + c.transform.position;
		lineRenderer.SetPosition(1, point.transform.InverseTransformPoint(v));
		circlePoint = v;
		return v;
	}

	public static void MoveLabel(InterestPoint p, Vector3 v, Vector3 off)
	{
		p.rectTransform.anchorMin = v;
		p.rectTransform.anchorMax = v;

		if (off.x > 0)
		{
			if (off.y > 0)
			{
				p.textObject.alignment = TextAnchor.LowerLeft;
			}
			else
			{
				p.textObject.alignment = TextAnchor.UpperLeft;
			}
		}
		else
		{
			if (off.y > 0)
			{
				p.textObject.alignment = TextAnchor.LowerRight;
			}
			else
			{
				p.textObject.alignment = TextAnchor.UpperRight;
			}
		}

	}

	public Vector2 viewportPosition
	{
		get { return Camera.main.WorldToViewportPoint(point.transform.position); }
	}
	public Vector3 target
	{
		get { return point.transform.position; }
	}
	public Vector3 circlePosition
	{
		get { return circlePoint; }
	}
}