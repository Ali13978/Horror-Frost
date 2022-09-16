using UnityEngine;
using System.Collections;

public class KeyScript : MonoBehaviour {
	public string PlayerTag = "Player";
	public GameObject Door;
	private BoxCollider coll;
	private MeshRenderer meshR;
	private AudioSource auidio;
	public GameObject glow;
	// Use this for initialization
	void OnEnable () {
		glow.SetActive (true);
		coll = GetComponent<BoxCollider> ();
		meshR = GetComponent<MeshRenderer> ();
		auidio = GetComponent<AudioSource> ();
		coll.enabled = true;
		meshR.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.up * 100 * Time.deltaTime);
	}
	void OnTriggerEnter(Collider other){
		if(other.transform.gameObject.tag == PlayerTag){
			GetKey();
		}
	}
	void GetKey(){
		DoorScript doorScript = Door.GetComponent<DoorScript> ();
		doorScript.keySystem.isUnlock = true;
		coll.enabled = true;
		meshR.enabled = false;
		auidio.Play ();
		glow.SetActive (false);
		Invoke ("DeactivateKey", 1);
	}
	void DeactivateKey(){
		gameObject.SetActive (false);
	}
}
