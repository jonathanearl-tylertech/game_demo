using UnityEngine;
using System.Collections;

public class StarBar_interaction : MonoBehaviour {
	private CameraBehavior main_camera;
	private float width;
	private float MIN_BAR_WIDTH = 0f;
	public float PERCENT_OF_CAMERA_WIDTH = 0.33f;
	public float PERCENT_OF_CAMERA_HEIGHT = 0.3f;
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
		this.width = cam_width * PERCENT_OF_CAMERA_WIDTH; 
		float height = cam_height * PERCENT_OF_CAMERA_HEIGHT; // height is 5 % of the screen
		// get position of bar
		float x = main_camera.transform.position.x;
		float y = main_camera.transform.position.y + cam_height/2f - (1f * height);
		float bar_offset = (1f - this.bar_ratio) * this.width * 1.125f;
		this.transform.position = new Vector3 (x - bar_offset, y, 0f);
		this.transform.localScale = new Vector3 (width * this.bar_ratio, height, 0f);
	}

	public void UpdateStarBarSize(float timer_left) {
		if (timer_left < MIN_BAR_WIDTH)
			return;
		this.bar_ratio = timer_left;
	}
}
