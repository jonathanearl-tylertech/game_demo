using UnityEngine;
using System.Collections;

<<<<<<< HEAD:Assets/Scripts/JellyScripts/HeadColliderInteraction.cs
public class HeadColliderInteraction : MonoBehaviour {
=======
public class JellyFish_interaction : MonoBehaviour {

    public float bounce_force = 40f;
	private float start_y;
	private Rigidbody2D rigid_body;

	#region support floating of jelly fish
	private bool float_up = false;
	public float float_radius_max = 0.5f;
	public float float_radius_min = 0.3f;
	public float float_radius;
	#endregion

>>>>>>> origin/master:Assets/Scripts/JellyFish_interaction.cs
	public Animator anim;
	public float bounce_force = 40f;

<<<<<<< HEAD:Assets/Scripts/JellyScripts/HeadColliderInteraction.cs
	// Use this for initialization
	void Start () {
		anim = GetComponentInParent<Animator> ();
=======
    // Use this for initialization
    void Start()
    {
		this.float_radius = Random.Range (float_radius_min, float_radius_max);
		this.start_y = this.transform.position.y;
		this.rigid_body = this.gameObject.GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
    }
		
	void FixedUpdate() {
		float distance_from_start = this.start_y - this.transform.position.y;
		if (distance_from_start > float_radius) {
			float_up = true;
		} else if (distance_from_start < -float_radius) {
			float_up = false;
		}

		if (float_up)
			FloatUp ();
	}

	void FloatUp() {
		this.rigid_body.AddForce (new Vector2 (0f, 20f), ForceMode2D.Force);
>>>>>>> origin/master:Assets/Scripts/JellyFish_interaction.cs
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			anim.SetTrigger ("trigger");
			Rigidbody2D hero_rigid = other.gameObject.GetComponent<Rigidbody2D>();
			hero_rigid.velocity = new Vector3(hero_rigid.velocity.x, 0f, 0f);
			// choose up or down bounce
			int direction;
			if (other.gameObject.transform.position.y > this.transform.position.y) direction = 1;
			else direction = -1;
			hero_rigid.AddForce(new Vector3(hero_rigid.velocity.x, direction * bounce_force, 0), ForceMode2D.Impulse);
		}
	}
}
