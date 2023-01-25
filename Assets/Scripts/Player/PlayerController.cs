using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
	public Rigidbody playerRb;
	
	public GameObject playerCamera; //TODO: make rotation happen in camera object so i can add phys movement back
	
	public float moveSpeed;
	public float rotateSpeed;
	public float jumpForce;
	
	private Controls m_Controls;
	private Vector2 m_Rotation;
	
	
	public bool canPlayerJump;
	

	public GameObject debugLine;
	
	
	
	
	public void Awake()
	{
		m_Controls = new Controls();
	
	}
	
	public void OnEnable()
	{
		m_Controls.Enable();
	}
	
	public void OnDisable()
	{
		m_Controls.Disable();
	}
	
	public void OnGUI()
	{
	}
	
	public void FixedUpdate()
	{
		var look = m_Controls.gameplay.look.ReadValue<Vector2>();
		var move = m_Controls.gameplay.move.ReadValue<Vector2>();
	
		
		Look(look);
		
		//FindJumpableSurface();
		
		if(m_Controls.gameplay.Jump.triggered && canPlayerJump)
		{
			Jump();
		}
		
		
		Move(move);
	}

	
	private void Move(Vector2 direction)
	{
		if (direction.sqrMagnitude < 0.01)
			return;
		var scaledMoveSpeed = moveSpeed * Time.deltaTime;
		// For simplicity's sake, we just keep movement in a single plane here. Rotate
		// direction according to world Y rotation of player.
		var move = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
		//transform.position += move * scaledMoveSpeed;
		playerRb.AddForce(move	*scaledMoveSpeed, ForceMode.Impulse);
	}
	
	private void Look(Vector2 rotate)
	{
		
		// update camera position
		playerCamera.transform.position = transform.position;
		
		if (rotate.sqrMagnitude < 0.01)
			return;
		var scaledRotationSpeed =  rotateSpeed * Time.deltaTime;
		m_Rotation.y += rotate.x * scaledRotationSpeed;
		m_Rotation.x = Mathf.Clamp(m_Rotation.x - rotate.y * scaledRotationSpeed, -89, 89);
		playerCamera.transform.localEulerAngles = m_Rotation;
		
	}
	
	//TODO: this can be triggered multiple times
	private void Jump()
	{
		RaycastHit hit;
		if (Physics.SphereCast(transform.position, .499f, Vector3.down, out hit, .5f))
		{
			float vertDeviation = Vector3.Angle(hit.normal, Vector3.up);
			Debug.Log("Deviation: " + vertDeviation);
			Instantiate(debugLine, hit.point, Quaternion.FromToRotation(Vector3.up,hit.normal));
			playerRb.AddForce(hit.normal.normalized * jumpForce	, ForceMode.Impulse);
			
		}
		
	}
	
	private void ReloadSettings(){
		m_Controls = new Controls();
	}
}


























































































