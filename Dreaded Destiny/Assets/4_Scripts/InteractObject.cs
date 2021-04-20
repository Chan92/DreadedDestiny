using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
	public enum ObjectType {
		LivingCreature,
		HardObject,
		SoftObject
	}

	public ObjectType objectType;

	private void OnMouseOver() {
		if(Input.GetMouseButtonDown(0)) {
			Grab();
			//print("catch/grab");
		} else if(Input.GetMouseButtonDown(1)) {
			Break();
			//print("kill/break");
		}
	}

	private void Grab() {
		BugHunt.instance.OnInteract(true);
		Dissapear();

		switch(objectType) {
			case ObjectType.LivingCreature:
				//collectcount+1
				break;
			case ObjectType.HardObject:
				//object follow mouse
				print("Not yet inplemented");
				break;
			default:
				print("Not yet inplemented");
				break;
		}
	}

	private void Break() {
		BugHunt.instance.OnInteract(false);
		Dissapear();		

		switch(objectType) {
			case ObjectType.LivingCreature:
				//killcount+1
				break;
			case ObjectType.HardObject:
				//object changes sprite
				print("Not yet inplemented");
				break;
			default:
				print("Not yet inplemented");
				break;
		}
	}

	public void Dissapear() {
		gameObject.SetActive(false);
	}

	private void OnBecameInvisible() {
		BugHunt.instance.Dissapear();
		Dissapear();
	}
}
