#if UNITY_EDITOR
using System;
using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
public class Door_editor : MonoBehaviour {
	[Header("Door parameters")]
	[Tooltip("Door width in Unity units. 1 unit = 1 meter. May not work with all types of doors.")]
	[Range (0.5f, 2.5f)]
	public float Width = 0.9f;
	[Tooltip("Door height in Unity units. 1 unit = 1 meter. May not work with all types of doors.")]
	[Range(0.5f, 4)]
	public float Height = 2.1f;
	[Tooltip("Door frame depth in Unity units. 1 unit = 1 meter. May not work with all types of doors.")]
	[Range (0.05f, 0.7f)]
	public float Depth = 0.2f;
	[Tooltip("May not work with all types of doors")]
	[Range(0, 1.75f)]
	public float KnobXPosition = 0.06f;
	[Tooltip("May not work with all types of doors")]
	[Range(0.7f, 1.8f)]
	public float KnobYPosition = 1.06f;
	[Tooltip("Size of the door trigger. Increase this if you want open the door from far distance")]
	[Range(0.8f, 8)]
	public float OpenDistance = 4;

	[Serializable]
	public class ObjCtrls{
		public Transform Width_L;
		public Transform Width_R;
		public Transform Knob01Placement;
		public Transform Knob02Placement;
		public Transform Lock01;
		public Transform Lock02;
		public Transform Depth01;
		public Transform Depth02;
		public Transform Depth03;
		public Transform Depth04;
		public Transform Door_top01;
		public Transform Door_top02;
		public Transform Door_top03;
		public Transform Door_top04;
		public Transform Frame_top01;
		public Transform Frame_top02;
		public Transform Coll;
		public Transform Coll02;
	}
	private Transform[] Childs;
	public ObjCtrls Controllers = new ObjCtrls();



	bool _isObjectSelected;

	void Update () {
		bool contains = Selection.Contains (gameObject);

		if (contains != _isObjectSelected) {
			_isObjectSelected = contains;
			
			if (_isObjectSelected) {
				//Debug.Log ("Children were found");
			
				Childs = GetComponentsInChildren<Transform> ();
				foreach (Transform ObjTrfm in Childs) {
				
					if (ObjTrfm.name == "Width_L" && Controllers.Width_L == null) {
						Controllers.Width_L = ObjTrfm;
					}
					if (ObjTrfm.name == "Width_R" && Controllers.Width_R == null) {
						Controllers.Width_R = ObjTrfm;
					}
					if (ObjTrfm.name == "Knob01Placement" && Controllers.Knob01Placement == null) {
						Controllers.Knob01Placement = ObjTrfm;
					}
					if (ObjTrfm.name == "Knob02Placement" && Controllers.Knob02Placement == null) {
						Controllers.Knob02Placement = ObjTrfm;
					}
					if (ObjTrfm.name == "Lock01" && Controllers.Lock01 == null) {
						Controllers.Lock01 = ObjTrfm;
					}
					if (ObjTrfm.name == "Lock02" && Controllers.Lock02 == null) {
						Controllers.Lock02 = ObjTrfm;
					}
					if (ObjTrfm.name == "Depth01" && Controllers.Depth01 == null) {
						Controllers.Depth01 = ObjTrfm;
					}
					if (ObjTrfm.name == "Depth02" && Controllers.Depth02 == null) {
						Controllers.Depth02 = ObjTrfm;
					}
					if (ObjTrfm.name == "Depth03" && Controllers.Depth03 == null) {
						Controllers.Depth03 = ObjTrfm;
					}
					if (ObjTrfm.name == "Depth04" && Controllers.Depth04 == null) {
						Controllers.Depth04 = ObjTrfm;
					}
					if (ObjTrfm.name == "Door_top01" && Controllers.Door_top01 == null) {
						Controllers.Door_top01 = ObjTrfm;
					}
					if (ObjTrfm.name == "Door_top02" && Controllers.Door_top02 == null) {
						Controllers.Door_top02 = ObjTrfm;
					}
					if (ObjTrfm.name == "Door_top03" && Controllers.Door_top03 == null) {
						Controllers.Door_top03 = ObjTrfm;
					}
					if (ObjTrfm.name == "Door_top04" && Controllers.Door_top04 == null) {
						Controllers.Door_top04 = ObjTrfm;
					}
					if (ObjTrfm.name == "Frame_top01" && Controllers.Frame_top01 == null) {
						Controllers.Frame_top01 = ObjTrfm;
					}
					if (ObjTrfm.name == "Frame_top02" && Controllers.Frame_top02 == null) {
						Controllers.Frame_top02 = ObjTrfm;
					}
					if (ObjTrfm.name == "Coll" && Controllers.Coll == null) {
						Controllers.Coll = ObjTrfm;
					}
					if (ObjTrfm.name == "Coll02" && Controllers.Coll02 == null) {
						Controllers.Coll02 = ObjTrfm;
					}
				}
			}
		}
			
	
		if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode) {
			this.enabled = false;
		} 
		else if(Selection.Contains (gameObject)) {

		}

	}

	void OnValidate () {
		if(UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode) {
			this.enabled = false;
		} 
		else {

			if(Controllers.Width_L != null){
				Controllers.Width_L.localPosition = new Vector3(Width/2, Controllers.Width_L.localPosition.y, Controllers.Width_L.localPosition.z);
			}
			else if(Controllers.Coll != null){
				Controllers.Coll.localScale = new Vector3(Controllers.Coll.localScale.x, Height, Controllers.Coll.localScale.z);
				if(Controllers.Coll02 != null){
					Controllers.Coll02.localScale = new Vector3(Controllers.Coll02.localScale.x, Height, Controllers.Coll02.localScale.z);
				}
			}
			if(Controllers.Width_R != null){
				Controllers.Width_R.localPosition = new Vector3(-Width/2, Controllers.Width_R.localPosition.y, Controllers.Width_R.localPosition.z);
			}
			if(Controllers.Coll != null && Controllers.Width_L != null && Controllers.Width_R != null){
				if(Controllers.Door_top03 != null && Controllers.Coll02 != null){
					Controllers.Coll.localScale = new Vector3(Width/2, Height, Controllers.Coll.localScale.z);
					Controllers.Coll02.localScale = new Vector3(-Width/2, Height, Controllers.Coll02.localScale.z);
				}else{
					Controllers.Coll.localScale = new Vector3(Width, Height, Controllers.Coll.localScale.z);
				}
			}
			if(GetComponent<BoxCollider>() != null){
				if(Controllers.Door_top03 != null){
					GetComponent<BoxCollider>().size = new Vector3 (Width + 0.5f, Height, Width/2 * OpenDistance * 1.4f);
				}else{
					GetComponent<BoxCollider>().size = new Vector3 (Width + 0.5f, Height, Width * OpenDistance);
				}
				GetComponent<BoxCollider>().center = new Vector3 (0, Height/2, 0);
			}

			if(Controllers.Depth01 != null){
				if(Mathf.Sign(Controllers.Depth01.localPosition.z) == -1){
					Controllers.Depth01.localPosition = new Vector3(Controllers.Depth01.localPosition.x, Controllers.Depth01.localPosition.y, -Depth);
				}else{
					Controllers.Depth01.localPosition = new Vector3(Controllers.Depth01.localPosition.x, Controllers.Depth01.localPosition.y, Depth);
				}
				if(Controllers.Depth02 != null){
					if(Mathf.Sign(Controllers.Depth02.localPosition.z) == -1){
						Controllers.Depth02.localPosition = new Vector3(Controllers.Depth02.localPosition.x, Controllers.Depth02.localPosition.y, -Depth);
					}
					else{
						Controllers.Depth02.localPosition = new Vector3(Controllers.Depth02.localPosition.x, Controllers.Depth02.localPosition.y, Depth);
					}
				}
			}

			if(Controllers.Knob01Placement != null){
				Controllers.Knob01Placement.localPosition = new Vector2(KnobXPosition, KnobYPosition);
			}
			if(Controllers.Knob01Placement != null || Controllers.Knob02Placement != null && Controllers.Lock01 != null){
				Controllers.Lock01.localPosition = new Vector3(Controllers.Lock01.localPosition.x, KnobYPosition, Controllers.Lock01.localPosition.z);
				Controllers.Lock02.position = new Vector3(Controllers.Lock02.position.x, Controllers.Lock01.position.y, Controllers.Lock02.position.z);
			}
			if(Controllers.Knob02Placement != null){
				Controllers.Knob02Placement.localPosition = new Vector2(-KnobXPosition, KnobYPosition);
			}

			if(Controllers.Door_top01 != null){
				Controllers.Door_top01.position = new Vector3(Controllers.Door_top01.position.x, Controllers.Door_top01.transform.parent.position.y + Height, Controllers.Door_top01.position.z);
			}
			if(Controllers.Door_top02 != null){
				Controllers.Door_top02.position = new Vector3(Controllers.Door_top02.position.x, Controllers.Door_top01.position.y, Controllers.Door_top02.position.z);
			}
			if(Controllers.Door_top03 != null){
				Controllers.Door_top03.position = new Vector3(Controllers.Door_top03.position.x, Controllers.Door_top01.position.y, Controllers.Door_top03.position.z);
			}
			if(Controllers.Door_top04 != null){
				Controllers.Door_top04.position = new Vector3(Controllers.Door_top04.position.x, Controllers.Door_top01.position.y, Controllers.Door_top04.position.z);
			}
			if(Controllers.Depth03 != null){
				Controllers.Depth03.localPosition = new Vector3(Controllers.Depth03.localPosition.x, Height, Controllers.Depth03.localPosition.z);
			}
			if(Controllers.Depth04 != null){
				Controllers.Depth04.localPosition = new Vector3(Controllers.Depth04.localPosition.x, Height, Controllers.Depth04.localPosition.z);
			}
			if(Controllers.Frame_top01 != null){
				Controllers.Frame_top01.position = new Vector3(Controllers.Frame_top01.position.x, Controllers.Door_top01.position.y, Controllers.Frame_top01.position.z);
			}
			if(Controllers.Frame_top02 != null){
				Controllers.Frame_top02.position = new Vector3(Controllers.Frame_top02.position.x, Controllers.Door_top01.position.y, Controllers.Frame_top02.position.z);
			}

		}
	}
}
#endif