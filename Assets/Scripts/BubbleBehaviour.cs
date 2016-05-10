using UnityEngine;
using System.Collections;

public class BubbleBehaviour : MonoBehaviour {
	public float sinAmp = 0.5f;
	public float sinOsc = 15f;
	public Vector3 initpos;
	public bool hasMeemo = false;
	private Vector3 bSize;
	public bool isPopped;
	public Animator anim;

	ParticleSystem ps;
	ParticleSystem.EmissionModule em;

	private Hero_Interaction thisMeemo;



	// Use this for initialization
	void Start () {
		CameraBehavior globalBehaviour = GameObject.Find ("Main Camera").GetComponent<CameraBehavior> ();
		bSize = GetComponent<Renderer> ().bounds.size;

		transform.position = new Vector3 (Random.Range (globalBehaviour.WorldMin.x, globalBehaviour.WorldMax.x),
			globalBehaviour.WorldMin.y - bSize.y / 2f, 0f);

		initpos = transform.position;

		anim = GetComponent<Animator> ();
		isPopped = false;

		ps = this.GetComponent<ParticleSystem> ();
		em = ps.emission;
		em.enabled = false;


	}




	// Update is called once per frame
	void Update () {
		CameraBehavior globalBehaviour = GameObject.Find ("Main Camera").GetComponent<CameraBehavior> ();

		if(!hasMeemo)
			FollowSineCurve ();

		Vector3 size = GetComponent<Renderer> ().bounds.size;

		// When top of bubble touches world bound, Pop it
		if ((transform.position.y + size.y / 2f) > globalBehaviour.WorldMax.y && !isPopped) {
			PopBubble ();
		}

		// When bottom of bubble touches world bound, destroy it
		if ((transform.position.y - size.y / 2f) > globalBehaviour.WorldMax.y) {
			Debug.Log ("bubble is destroyed");
			Destroy (this.gameObject);
		}
	}




	// Update position of bubble following sine curve
	public void FollowSineCurve(){
		float newY = transform.position.y + 0.03f;
		float newX = initpos.x + GetXValue (newY); 
		transform.position = new Vector3 (newX, newY, 0f);
	}




	// Calculate the x value for bubble movement
	private float GetXValue(float y){
		CameraBehavior globalBehaviour = GameObject.Find ("Main Camera").GetComponent<CameraBehavior> ();

		float sinFreqScale = sinOsc * 2f * (Mathf.PI) / globalBehaviour.WorldMax.y;
		return sinAmp * (Mathf.Sin(y * sinFreqScale));
	}




	// When hero comes into the bubble
	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log ("Touches something");
		Hero_Interaction meemo = GameObject.FindGameObjectWithTag ("Player").GetComponent<Hero_Interaction> ();
		if (other.gameObject.name == "Meemo" && !meemo.isInBubble && !isPopped) {
			Debug.Log ("Touches Meemo");
			thisMeemo = other.GetComponent<Hero_Interaction> ();

			hasMeemo = true;
			thisMeemo.GetComponent<Rigidbody2D> ().isKinematic = true;
			thisMeemo.bubble = this;
			thisMeemo.isInBubble = true;

			ps = this.GetComponent<ParticleSystem> ();
			em = ps.emission;
			em.enabled = true;
		}
	}


	void PopBubble() {
		Debug.Log ("Bubble is popping");
		if (hasMeemo) {
			thisMeemo.isInBubble = false;
			thisMeemo.GetComponent<Rigidbody2D> ().isKinematic = false;
			hasMeemo = false;
		}
		anim.SetTrigger ("trigger");
		isPopped = true;
	}
}
