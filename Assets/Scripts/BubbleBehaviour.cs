using UnityEngine;
using System.Collections;

public class BubbleBehaviour : MonoBehaviour {
	public float sinAmp = 0.05f;
	public float sinOsc = 15f;
	public Vector3 initpos;
	public bool hasMeemo = false;
	private Vector3 bSize;

	ParticleSystem ps;
	ParticleSystem.EmissionModule em;

	private Hero_Interaction thisMeemo;



	// Use this for initialization
	void Start () {
		GlobalBehaviour globalBehaviour = GameObject.Find ("GameManager").GetComponent<GlobalBehaviour> ();
		bSize = GetComponent<Renderer> ().bounds.size;

		transform.position = new Vector3 (Random.Range (globalBehaviour.WorldMin.x, globalBehaviour.WorldMax.x),
			globalBehaviour.WorldMin.y - bSize.y / 2f, 0f);

		initpos = transform.position;

		GetComponent<Animator> ().enabled = false;

		ps = this.GetComponent<ParticleSystem> ();
		em = ps.emission;
		em.enabled = false;

	}




	// Update is called once per frame
	void Update () {
		GlobalBehaviour globalBehaviour = GameObject.Find ("GameManager").GetComponent<GlobalBehaviour> ();

		if(!hasMeemo)
			FollowSineCurve ();

		Vector3 size = GetComponent<Renderer> ().bounds.size;

		// When top of bubble touches world bound, Pop it
		if ((transform.position.y + size.y / 2f) > globalBehaviour.WorldMax.y) {
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
		float newY = transform.position.y + 0.01f;
		float newX = initpos.x + GetXValue (newY); 
		transform.position = new Vector3 (newX, newY, 0f);
	}




	// Calculate the x value for bubble movement
	private float GetXValue(float y){
		GlobalBehaviour globalBehaviour = GameObject.Find ("GameManager").GetComponent<GlobalBehaviour> ();

		float sinFreqScale = sinOsc * 2f * (Mathf.PI) / globalBehaviour.WorldMax.y;
		return sinAmp * (Mathf.Sin(y * sinFreqScale));
	}




	// When hero comes into the bubble
	void OnTriggerEnter2D(Collider2D other)
	{
		Hero_Interaction meemo = GameObject.FindGameObjectWithTag ("Meemo").GetComponent<Hero_Interaction> ();
		if (other.gameObject.name == "Meemo" && !meemo.isInBubble) {
			thisMeemo = other.GetComponent<Hero_Interaction> ();

			hasMeemo = true;
			thisMeemo.GetComponent<Rigidbody2D> ().isKinematic = false;
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
			thisMeemo.GetComponent<Rigidbody2D> ().isKinematic = true;
			hasMeemo = false;
		}
		GetComponent<Animator> ().enabled = true;
	}
}
