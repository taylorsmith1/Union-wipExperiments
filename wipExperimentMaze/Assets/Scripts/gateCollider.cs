using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateCollider : MonoBehaviour {

	public Collider player;
	public static string isInGate = "0";

	void Update()
	{
		Debug.Log (isInGate);
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Gate") 
		{
			isInGate = "1";
		} 
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.tag == "Gate")
		{
			isInGate = "0";
		}
	}
}
