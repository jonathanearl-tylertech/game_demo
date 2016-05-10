using UnityEngine;
using System.Collections;

public class StarBar_interaction : MonoBehaviour {
	private CameraBehavior main_camera;
	private float width;
	private float MIN_BAR_WIDTH = 0f;
	private float bar_ratio = 1f;
	// Use this for initialization
	void Start () {
		this.main_camera = GameObject.Find ("Main Camera").GetComponent<CameraBehavior> ();
		UpdateStarBarInCamera ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	// deterimine position and size relative to the camera of the starbar
	public void UpdateStarBarInCamera() {

		float cam_height = main_camera.WorldMax.y - main_camera.WorldMin.y;
		float cam_width = main_camera.WorldMax.x - main_camera.WorldMin.x;
		// get width of bar and height of bar
		float width = cam_width / 2f * 0.5f; //width is 50% of the screen
		float height = cam_height * 0.05f; // height is 5 % of the screen
		// get position of bar
		float x = main_camera.transform.position.x;
		float y = main_camera.transform.position.y + cam_height/2f - (1f * height);

		this.transform.position = new Vector3 (x, y, 0f);
		this.transform.localScale = new Vector3 (width * this.bar_ratio, height, 0f);
	}

	public void UpdateStarBarSize(float timer_left) {
		if (timer_left < MIN_BAR_WIDTH)
			return;
		this.bar_ratio = timer_left;
	}
}
