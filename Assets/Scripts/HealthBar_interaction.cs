using UnityEngine;
using System.Collections;

public class HealthBar_interaction : MonoBehaviour {

	GlobalBehaviour global_behaviour;
	private float width;
	// Use this for initialization
	void Start () {
		this.global_behaviour = GameObject.Find ("GameManager").GetComponent<GlobalBehaviour> ();
		this.global_behaviour.UpdateWorldWindowBound ();
		Vector2 world_min = global_behaviour.WorldMin;
		Vector2 world_max = global_behaviour.WorldMax;
		Vector2 world_center = global_behaviour.WorldCenter;
		Debug.Log ("min" + world_min + " max:" + world_max);
		float width = world_max.x - world_min.x * 0.50f; //width is 50% of the screen
		float height = world_max.y - world_min.y * 0.01f; // height is 2 % of the screen
		Debug.Log("width:" + width + " height:" + height);
		float x = world_center.x;
		float y = world_max.y - height;
		Debug.Log("x pos:" + x + " y pos:" + y);

		this.transform.position = new Vector3 (x, y, 0f);
		this.transform.localScale = new Vector3 (width, height, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
