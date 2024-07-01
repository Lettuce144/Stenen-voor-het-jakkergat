using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToHere : MonoBehaviour
{
	private Camera _MainCam;

	private void Start()
	{
		_MainCam = Camera.main;
	}

	private void LateUpdate()
	{
		transform.LookAt(_MainCam.transform);
		transform.Rotate(90, 0, 0);
	}

	private void OnMouseOver()
	{
		Debug.Log("Hovered!");
	}
}
