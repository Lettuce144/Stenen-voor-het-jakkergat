using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{

	/*	private Vector2 _delta;

		private bool _isMoving;
		private bool _isRotating;
		private bool _isBusy;

		private CinemachineVirtualCamera _camera;
		private CinemachineOrbitalTransposer _transposer;


		[SerializeField] 
		private float movementSpeed = 10.0f;
		[SerializeField] 
		private float zoomSensitivty = 10.0f;


		private void Start()
		{
			_camera = GetComponent<CinemachineVirtualCamera>();
			_transposer = _camera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
		}

		public void OnLook(InputAction.CallbackContext context)
		{
			_delta = context.ReadValue<Vector2>();
		}

		public void OnMove(InputAction.CallbackContext context)
		{
			_isMoving = context.started || context.performed;
		}

		public void OnZoom(InputAction.CallbackContext context)
		{
			// Oh god this is bad, very very bad code
			// Too bad tho.
			float z = context.ReadValue<float>();
			if (z > 0)
			{ 
				_transposer.m_FollowOffset.z += zoomSensitivty;
				_transposer.m_FollowOffset.y += zoomSensitivty / _transposer.m_FollowOffset.y;
			}
			else if (z < 0)
			{ 
				_transposer.m_FollowOffset.z -= zoomSensitivty;
				_transposer.m_FollowOffset.y -= zoomSensitivty / _transposer.m_FollowOffset.y;
			}
		}

		private void LateUpdate()
		{
			*//*if (_isMoving)
			{
				Vector3 position = transform.right * (_delta.x * -movementSpeed);
				position += transform.up * (_delta.y * -movementSpeed);
				transform.position += position * Time.deltaTime;
			}*//*

		}*/

	[SerializeField]
	private Transform _target;

	[SerializeField]
	private float _sensitivity = 10f;

	private Vector2 _rotation = Vector2.zero;

	[SerializeField]
	// Also counts as the starting distance from the center
	private float _distance = 10f;

	private InputAction lookAction;
	private bool shouldRotate;

	public void Start()
	{
		// Assuming you have set up the Input Actions in the Input System
		var map = new InputActionMap("CameraControls");
		lookAction = map.AddAction("Look", binding: "<Mouse>/delta");
		lookAction.performed += ctx => RotateCamera(ctx.ReadValue<Vector2>());
		clickAction = map.AddAction("Click", binding: "<Mouse>/leftButton");
		map.Enable();
	}

	public void RotateCamera(Vector2 input)
	{
		if (shouldRotate)
		{
			_rotation.y += input.x * _sensitivity * Time.deltaTime;
			_rotation.x = Mathf.Clamp(_rotation.x - input.y * _sensitivity * Time.deltaTime, -89f, 89f);
			Quaternion rotationQuat = Quaternion.Euler(_rotation.x, _rotation.y, 0f);
			transform.position = _target.position - (rotationQuat * Vector3.forward * _distance);
			transform.LookAt(_target);
		}
	}
}
