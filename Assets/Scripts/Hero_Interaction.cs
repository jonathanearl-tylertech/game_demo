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

	private float meemoSpeed = 5f;
	public BubbleBehaviour bubble;
	public bool isInBubble;
	private bool isFacingRight;

	// Use this for initialization
	void Start () {
        this.rigid_body = this.GetComponent<Rigidbody2D>();
		isInBubble = false;
		isFacingRight = true;
    }

    void FixedUpdate () {
        

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


		/// Interaction with bubble
		if (isInBubble) {
			Debug.Log ("InBubble");

			if (Input.GetAxis ("Horizontal") != 0f) { // When meemo is controlling the horizontal direction
				

				Debug.Log("MOVING IN BUBBLE");
				float bnewY = bubble.transform.position.y + 0.01f;// bubble floats
				float bnewX = transform.position.x + Input.GetAxis ("Horizontal") * (meemoSpeed * Time.smoothDeltaTime);
				bubble.transform.position = new Vector3 (bnewX, bnewY, 0f);
				bubble.initpos.x = bnewX;

				transform.position = new Vector3 (bnewX - 0.05f, bnewY - 0.5f, transform.position.z);

			} else { // When meemo is following bubble
				Debug.Log("Not moving in bubble");
				FollowSineCurve();

				// update meemo's position to bubble'e sine curve
				transform.position = new Vector3 (bubble.transform.position.x - 0.05f,
					bubble.transform.position.y - GetComponent<Renderer> ().bounds.size.y / 2f + 0.5f, bubble.transform.position.z);
			}

		} else {
<<<<<<< HEAD
			Debug.Log ("Not in bubble!");
			this.grounded = Physics2D.OverlapCircle(this.ground_check.position, this.ground_radius, this.what_is_ground);
			float move = Input.GetAxis("Horizontal");
			this.rigid_body.velocity = new Vector2(move * max_speed, this.rigid_body.velocity.y);

			if (Input.GetKeyDown ("space") && this.grounded) {
				Jump ();
			}
			else if (Input.GetKey ("space") && !this.grounded) {
				Debug.Log ("FLY BABY FLY");
				this.rigid_body.AddForce (new Vector2 (5f, 20f), ForceMode2D.Force);
			}
=======
			this.grounded = Physics2D.OverlapCircle(this.ground_check.position, this.ground_radius, this.what_is_ground);
			float move = Input.GetAxis("Horizontal");
			this.rigid_body.velocity = new Vector2(move * max_speed, this.rigid_body.velocity.y);
>>>>>>> origin/master
		}

		/// End interaction with bubble

		if (Input.GetAxis ("Horizontal") < 0f && isFacingRight) {
			transform.localScale = new Vector3 (-.4f, .4f, 0f);
			isFacingRight = false;
		}

		if (Input.GetAxis ("Horizontal") > 0f && !isFacingRight) {
			transform.localScale = new Vector3 (.4f, .4f, 0f);
			isFacingRight = true;
		}

    }

    void Jump ()
    {
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }



	// Update position of bubble following sine curve
	private void FollowSineCurve(){
		float newY = bubble.transform.position.y + 0.01f;
		float newX = bubble.initpos.x + GetXValue (newY); 
		bubble.transform.position = new Vector3 (newX, newY, 0f);
	}




	// Calculate the x value for bubble movement
	private float GetXValue(float y){
		GlobalBehaviour globalBehaviour = GameObject.Find ("GameManager").GetComponent<GlobalBehaviour> ();

		float sinFreqScale = bubble.sinOsc * 2f * (Mathf.PI) / globalBehaviour.WorldMax.y;
		return bubble.sinAmp * (Mathf.Sin(y * sinFreqScale));
	}

}
