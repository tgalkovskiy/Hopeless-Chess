using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateAround : MonoBehaviour
{

	public Transform target;
	public Vector2 startPosition;
	private Vector3 offset;

	public float sensitivity = 3; // чувствительность мышки
	public float limit = 80; // ограничение вращения по Y
	public float zoom = 0.25f; // чувствительность при увеличении, колесиком мышки
	public float zoomMax = 20; // макс. увеличение
	public float zoomMin = 5; // мин. увеличение
	private float X, Y;

	Vector2 tempCursorPosition;

	void Start()
	{
		limit = Mathf.Abs(limit);
		if (limit > 90) limit = 90;
		X = startPosition.x;
		Y = - startPosition.y;
		offset.z = zoomMin;
	}

	void Update()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0) offset.z += zoom;
		else if (Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= zoom;
		offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));

		if (Input.GetMouseButton(1))
		{
			//Cursor.visible = false;
			//Cursor.lockState = CursorLockMode.Locked;

			X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
			Y += Input.GetAxis("Mouse Y") * sensitivity;
			Y = Mathf.Clamp(Y, -limit, limit);
		}
		else
		{
			//Cursor.lockState = CursorLockMode.None;
			//Cursor.visible = true;
		}

		transform.localEulerAngles = new Vector3(-Y, X, 0);
		transform.position = transform.localRotation * offset + target.position;
	}
}
