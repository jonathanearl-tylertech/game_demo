using UnityEngine;
using System.Collections;

public class JellyFishTop_interaction : MonoBehaviour {

    public float bounce_force = 40f;
	private float start_y;
	private Rigidbody2D rigid_body;
    // Use this for initialization
    void Start()
    {
		start_y = this.transform.position.y;
		rigid_body = this.GetComponent<Rigidbody2D> ();
    }

	void FixedUpdate() {
		if ((this.start_y - this.transform.position.y) > 0.05f) {
			this.rigid_body.AddForce (new Vector2 (0f, 20f), ForceMode2D.Force);
		}
	}

    void OnCollisionEnter2D(Collision2D other)
    {
		Debug.Log ("COLLIDED JELLYFISH");
        if (other.gameObject.tag == "Player")
        {
			Debug.Log ("Collided with tag player");
            Rigidbody2D hero_rigid = other.gameObject.GetComponent<Rigidbody2D>();
            hero_rigid.velocity = new Vector3(hero_rigid.velocity.x, 0f, 0f);
            //hero_rigid.velocity = new Vector2(hero_rigid.velocity.x, 0f);
			// choose up or down bounce
			int direction;
			if (other.gameObject.transform.position.y > this.transform.position.y) direction = 1;
			else direction = -1;
            hero_rigid.AddForce(new Vector3(hero_rigid.velocity.x, direction * bounce_force, 0), ForceMode2D.Impulse);
        }
    }
}
