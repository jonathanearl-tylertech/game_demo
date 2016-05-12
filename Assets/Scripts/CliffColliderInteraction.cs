using UnityEngine;
using System.Collections;

public class CliffColliderInteraction : MonoBehaviour {
	public GameObject level1EndPanel = null;
	// Use this for initialization
	void Start () {
		level1EndPanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			level1EndPanel.SetActive (true);
		}
	}
}
