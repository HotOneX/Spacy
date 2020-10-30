using UnityEngine;
using System.Collections;
using System;

public class BGScroller : MonoBehaviour 
{
	public float scrollSpeed;
	public float tileSizeZ;
	public enum BDirection{ up, forwrd};

	public BDirection m_direction = BDirection.up;
	private Vector3 direction;
	private Vector3 StartPosition;

	void Start()
	{
		StartPosition = transform.position;
	}
	void Update()
	{
		if (m_direction == BDirection.up)
			direction = Vector3.up;
        else
        {
			direction = Vector3.forward;
        }
		float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ);
		transform.position = StartPosition + direction * newPosition;
	}

	//private Type m_Type = direction.up;
	//public direction type { get { return m_Type; } set { if (SetPropertyUtility.SetStruct(ref m_Type, value)) SetVerticesDirty(); } }
}
