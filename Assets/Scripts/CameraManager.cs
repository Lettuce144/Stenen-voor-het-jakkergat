using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
	[SerializeField]
	private Transform _target;

	[SerializeField]
	private float _sensitivity = 10f;

	private Vector2 _rotation = Vector2.zero;

	[SerializeField]
	// Also counts as the starting distance from the center
	private float _distance = 10f;

	private InputAction lookAction;
	private InputAction mouseClickAction;
	private bool shouldRotate;

	public void Start()
	{
		// Assuming you have set up the Input Actions in the Input System
		var map = new InputActionMap("CameraControls");
		lookAction = map.AddAction("Look", binding: "<Mouse>/delta");
		lookAction.performed += ctx => RotateCamera(ctx.ReadValue<Vector2>());

		// Clicking
		mouseClickAction = map.AddAction("Click", binding: "<Mouse>/leftButton");
		mouseClickAction.performed += ctx => shouldRotate = ctx.ReadValue<float>() > 0;
		mouseClickAction.canceled += ctx => shouldRotate = false;

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
