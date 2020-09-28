using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[Header("Main Variables:")]
	Vector3 offset;
	float X, Y;
	[SerializeField]
	[Tooltip("Rotation point")]  Transform target; // Точка вращения
	[SerializeField]
	Vector2 startPosition;
	[SerializeField]
	float sensitivity = 3; // чувствительность мышки
	[SerializeField]
	float verticalLimit = 80; // ограничение вращения по Y
	[Space]

	[Header("Zoom Variables:")]
	[Space]
	[SerializeField]
	float zoomSensitivity = 0.25f; // чувствительность при увеличении, колесиком мышки
	[SerializeField]
	float zoomMax = 20; // макс. увеличение
	[SerializeField]
	float zoomMin = 5; // мин. увеличение
	[Space]

	[Header("Inertia Variables:")]
	[Space]
	float inertia;
	[SerializeField]
	float maxInetia = 1.5f;
	[SerializeField]
	float inertiaStartOn = 0.5f;
	[SerializeField]
	float inertiaDecrease = 0.2f;
	[SerializeField]
	float startInertiaDecrease = 0.5f;

	void Start()
	{
		verticalLimit = Mathf.Abs(verticalLimit);
		if (verticalLimit > 90) verticalLimit = 90;
		X = startPosition.x;
		Y = - startPosition.y;
		offset.z = zoomMin;
	}

	void Update()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0) offset.z += zoomSensitivity;
		else if (Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= zoomSensitivity;
		offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));

		if (Input.GetMouseButton(1))
		{
			inertia = 0;
			X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
			Y += Input.GetAxis("Mouse Y") * sensitivity;
			Y = Mathf.Clamp(Y, -verticalLimit, verticalLimit);
		}

		if (Input.GetMouseButtonUp(1))
		{
			if ( Math.Abs( Input.GetAxis("Mouse X")) > inertiaStartOn) inertia = Input.GetAxis("Mouse X");
			Debug.Log(Input.GetAxis("Mouse X"));
			if (Math.Abs(inertia) > maxInetia) inertia = inertia/Math.Abs(inertia)*maxInetia;
		}

		if (Math.Abs(inertia) > 0)
		{
			X = transform.localEulerAngles.y + inertia * sensitivity;
			if (Math.Abs(inertia) > startInertiaDecrease) inertia -= inertia/ Math.Abs(inertia) * inertiaDecrease * Time.deltaTime;
			else inertia -= inertia / Math.Abs(inertia) *  inertiaDecrease * inertiaDecrease * Time.deltaTime;

		}

		transform.localEulerAngles = new Vector3(-Y, X, 0);
		transform.position = transform.localRotation * offset + target.position;
	}

	public void CameraRotation()
	{
		StartCoroutine(Rotation());
	}

	IEnumerator Rotation()
	{
		yield return new WaitForSeconds(1);
	}
}
