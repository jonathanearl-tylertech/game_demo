using UnityEngine;
using System.Collections;

public class JellyFish_interaction : MonoBehaviour {

	private float start_y;
	private Rigidbody2D rigid_body;

    // Use this for initialization
    void Start()
    {
		this.start_y = this.transform.position.y;
		this.rigid_body = this.gameObject.GetComponent<Rigidbody2D> ();
    }
		
	void FixedUpdate() {
		if ((this.start_y - this.transform.position.y) > 0.05f && this.transform.position.y + 0.05f < this.start_y) {
			this.rigid_body.AddForce (new Vector2 (0f, 20f), ForceMode2D.Force);
		}

	}
}
