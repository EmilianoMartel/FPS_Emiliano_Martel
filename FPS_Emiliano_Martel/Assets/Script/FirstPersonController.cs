using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private Transform look;
	[Header("Player Movement")]
	[Tooltip("Move speed of the character in m/s")]
    [SerializeField] private float moveSpeed = 4.0f;
	[Tooltip("Sprint speed of the character in m/s")]
    [SerializeField] private float sprintSpeed = 6.0f;
	[Tooltip("Rotation speed of the character")]
    [SerializeField] private float rotationSpeed = 1.0f;

	[Space(10)]
	[Tooltip("The height the player can jump")]
    [SerializeField] private float jumpHeight = 1.2f;
	[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
	[SerializeField] private float gravity = -15.0f;
    [SerializeField] private float terminalVelocity = 53.0f;

	[Header("Player Grounded")]
	[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    [SerializeField] private bool grounded = true;
	[Tooltip("Offset to mark feet position")]
    [SerializeField] private float groundedOffset = 0.85f;
	[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    [SerializeField] private float groundedRadius = 0.5f;
	[Tooltip("What layers the character uses as ground")]
    [SerializeField] private LayerMask groundLayers;

	[Header("Camera Limits")]
	[SerializeField] private float minCameraAngle = -90F;
    [SerializeField] private float maxCameraAngle = 90F;

	private CharacterController controller;

	private Quaternion _characterTargetRot;
	private Quaternion _cameraTargetRot;

	private float _verticalVelocity;

	private bool _sprint;
	public bool sprint { set { _sprint = value; } }
	private bool _jump;
	public bool jump { set { _jump = value; } }

	private Vector2 _direction;
	public Vector2 direction{ set { _direction = value; } }

	private Vector2 _lookRotation;
	public Vector2 lookRotation { set { _lookRotation = value; } }

	public Action<bool> shootEvent;
	public Action reloadEvent;
	public Action interactEvent;

    private void Awake()
    {
		if (!look)
		{
            Debug.LogError($"{name}: Look is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
		if (groundLayers.value == 0)
		{
            Debug.LogError($"{name}: Select a LayerMask.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    private void Start()
	{
		controller = GetComponent<CharacterController>();
		_characterTargetRot = transform.localRotation;
		_cameraTargetRot = look.localRotation;
		UnityEngine.Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update()
	{
		GroundedCheck();
		JumpAndGravity();
		LookRotation();
		Move();
    }

	private void GroundedCheck()
	{
		Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
		grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
	}

	private void JumpAndGravity()
	{
		if (grounded && _jump)
		{
			// the square root of H * -2 * G = how much velocity needed to reach desired height
			_verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
		}
		else
		{
			// if we are not grounded, do not jump
			_jump = false;
		}

		// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
		if (_verticalVelocity < terminalVelocity)
			_verticalVelocity += gravity * Time.deltaTime;
	}

    private void Move()
	{
        Vector3 moveValue = new Vector3(_direction.x, 0, _direction.y);

        moveValue = moveValue.x * transform.right + moveValue.z * transform.forward;

        // set target speed based on move speed, sprint speed and if sprint is pressed
        float targetSpeed = _sprint ? sprintSpeed : moveSpeed;

		// move the player
		controller.Move(moveValue.normalized * (targetSpeed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
	}

	private void LookRotation()
	{
		float yRot = _lookRotation.x * rotationSpeed;
		float xRot = _lookRotation.y * rotationSpeed;

		_characterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
		_cameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

		_cameraTargetRot = ClampRotationAroundXAxis(_cameraTargetRot);

		transform.localRotation = _characterTargetRot;
		look.localRotation = _cameraTargetRot;
	}

	private Quaternion ClampRotationAroundXAxis(Quaternion q)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
		angleX = Mathf.Clamp(angleX, minCameraAngle, maxCameraAngle);

		q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

		return q;
	}
}
