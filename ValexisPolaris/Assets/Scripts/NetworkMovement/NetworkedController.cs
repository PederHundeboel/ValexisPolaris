using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

//A unity MLAPI ready conversion of the Existing movement controller :)

public abstract class NetworkedController : NetworkBehaviour
{

	//Getters;
	public abstract Vector3 GetVelocity();
	public abstract Vector3 GetMovementVelocity();
	public abstract bool IsGrounded();

	//Events;
	public delegate void VectorEvent(Vector3 v);
	public VectorEvent OnJump;
	public VectorEvent OnLand;

}
