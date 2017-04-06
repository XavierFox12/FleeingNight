using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class cameraMovement : MonoBehaviour {
	/*********************************************************
	 * All of this code is courtesy of Unity from their
	 * tanks tutorial on how to move the camera with the
	 * player.
	 * ******************************************************/
	public float m_DampTime = 0.2f;
	public float m_ScreenEdgeBuffer = 4f;
	public float m_MinSize = 6.5f;
	[HideInInspector] public HashSet<Transform> m_Targets;

	private Camera m_Camera;
	private float m_ZoomSpeed;
	private Vector3 mZoomSpeed;
	private Vector3 m_MoveVelocity;
	private Vector3 m_DesiredPosition;
    public GameObject car;

    private void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>();
        m_Targets = new HashSet<Transform> { car.transform };
	}

    public void TrackPlayer(GameObject objects)
    {
            m_Targets.Add(objects.transform);

        Debug.Log(string.Join("\n", m_Targets.Select(x => x.ToString()).ToArray()));
    }

    public void UnTrackPlayer(GameObject objects)
    {
           if( m_Targets.Contains(objects.transform))
            {
                m_Targets.Remove(objects.transform);
            }
        Debug.Log(string.Join("\n", m_Targets.Select(x => x.ToString()).ToArray()));
    }

    public void TrackCar(GameObject objects)
    {
        m_Targets.Add(objects.transform);

        Debug.Log(string.Join("\n", m_Targets.Select(x => x.ToString()).ToArray()));
    }

    public void UnTrackCar(GameObject objects)
    {
        if (m_Targets.Contains(objects.transform))
        {
            m_Targets.Remove(objects.transform);
        }
        Debug.Log(string.Join("\n", m_Targets.Select(x => x.ToString()).ToArray()));
    }

    private void FixedUpdate()
	{
		Move ();
		Zoom ();
	}

	private void Move()
	{
		FindAveragePosition ();
		transform.position = Vector3.SmoothDamp (transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
	}

	private void FindAveragePosition()
	{
		Vector3 averagePos = new Vector3 ();
		int numTargets = 0;

        foreach (Transform trs in m_Targets)
        {
			if (!trs.gameObject.activeSelf)
				continue;

			averagePos += trs.position;
			numTargets++;
		}

		if (numTargets > 0)
			averagePos /= numTargets;

		averagePos.z = transform.position.z;

		m_DesiredPosition = averagePos;
	}

	private void Zoom()
	{
		float requiredSize = FindRequiredSize ();
		m_Camera.orthographicSize = Mathf.SmoothDamp (m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime);
	}

	private float FindRequiredSize ()
	{
		Vector3 desiredLocalPos = transform.InverseTransformPoint (m_DesiredPosition);
		float size = 0f;

		foreach (Transform trs in m_Targets) {
			if (!trs.gameObject.activeSelf)
				continue;

			Vector3 targetLocalPos = transform.InverseTransformPoint (trs.position);
			Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

			size = Mathf.Max (size, Mathf.Abs (desiredPosToTarget.y));
			size = Mathf.Max (size, Mathf.Abs (desiredPosToTarget.x) / m_Camera.aspect);
		}

		size += m_ScreenEdgeBuffer;
		size = Mathf.Max (size, m_MinSize);
		return size;
	}

	public void SetStartPositionAndSize()
	{
		FindAveragePosition ();
		transform.position = m_DesiredPosition;
		m_Camera.orthographicSize = FindRequiredSize ();
	}

    //private Vector3 offset;

    //void Start()
    //{
    //    offset = transform.position - car.transform.position;
    //}

    //void LateUpdate()
    //{
    //    transform.position = car.transform.position + offset;
    //}
}
