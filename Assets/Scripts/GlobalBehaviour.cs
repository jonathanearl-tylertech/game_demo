using UnityEngine;
using System.Collections;

public class GlobalBehaviour : MonoBehaviour {
	// For bubble creation
	public GameObject bubble = null;
	private float preBubbleTime = -2f;
	private const float bubbleCreateInterval = 5.0f; // in seconds


	// Use this for initialization
	void Start () {


		if (null == bubble)
			bubble = Resources.Load ("Prefabs/Bubble") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		CreateBubble ();
	}
		
	// Create bubble
	private void CreateBubble() {
		if ((Time.realtimeSinceStartup - preBubbleTime) > bubbleCreateInterval) {
			GameObject e = (GameObject) Instantiate(bubble);
			preBubbleTime = Time.realtimeSinceStartup;
		}
	}
}
