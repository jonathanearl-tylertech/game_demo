using UnityEngine;
using System.Collections;

public class Hero_Interaction : MonoBehaviour {

    public float max_speed = 2f;
    public float air_speed = 0.1f;
    private Rigidbody2D rigid_body;

    bool grounded = false;
    public Transform ground_check;
    float ground_radius = 0.5f;
    public LayerMask what_is_ground;

	// Use this for initialization
	void Start () {
        this.rigid_body = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate () {
        this.grounded = Physics2D.OverlapCircle(this.ground_check.position, this.ground_radius, this.what_is_ground);
        float move = Input.GetAxis("Horizontal");
        this.rigid_body.velocity = new Vector2(move * max_speed, this.rigid_body.velocity.y);

        //if (this.rigid_body.velocity.x > max_speed && move < 0f)
        //{
        //    this.rigid_body.velocity = new Vector2(-this.rigid_body.velocity.x + move * air_speed, this.rigid_body.velocity.y);
        //}
        //else if (this.rigid_body.velocity.x < -max_speed && move > 0f)
        //{
        //    this.rigid_body.velocity = new Vector2(this.rigid_body.velocity.x + move * air_speed, this.rigid_body.velocity.y);
        //}
        //else if (this.rigid_body.velocity.x <= max_speed && this.rigid_body.velocity.x >= -max_speed)
        //{
        //}
        //this.rigid_body.velocity = new Vector2(move * max_speed, this.rigid_body.velocity.y);
		if (Input.GetKeyDown ("space") && this.grounded) {
			Jump ();
		}
		else if (Input.GetKey ("space") && !this.grounded) {
			Debug.Log ("FLY BABY FLY");
			this.rigid_body.AddForce (new Vector2 (5f, 20f), ForceMode2D.Force);
		}
    }

    void Jump ()
    {
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
    }
}
