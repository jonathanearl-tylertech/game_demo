using UnityEngine;
using UnityEngine.SceneManagement;
// for SceneManager
using System.Collections;

public class GlobalGameManager : MonoBehaviour {
	private string mCurrentLevel = "MenuLevel";  //  
	#region support food object spawning
	private GameObject food_item;
    private int score = 0;
	#endregion

	private AudioSource audioSource;

	#region World Bound support
	private Bounds mWorldBound;  // this is the world bound
	private Vector2 mWorldMin;	// Better support 2D interactions
	private Vector2 mWorldMax;
	private Vector2 mWorldCenter;
	private Camera mMainCamera;
	#endregion

	// Use this for initialization
	void Start () {
		#region world bound support

		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = Resources.Load("Audio/level1") as AudioClip;
		audioSource.loop = true;
		audioSource.volume = 0.5f;
		audioSource.Play();

		UpdateWorldWindowBound();
		#endregion

		#region support food spawning
		if (this.food_item == null)
			this.food_item = Resources.Load ("Prefabs/Food") as GameObject;
		#endregion
		DontDestroyOnLoad(this);

	}


    #region score support
    public void AddScore()
    {
        ++this.score;
    }

    public void MinusScore()
    {
		if (this.score > 0) --this.score;
    }

    public int GetScore()
    {
        return this.score;
    }

    public void SetScore(int score)
    {
        this.score = score;
    }
    #endregion

    public void CreateNewFoodItem()
    {
        GameObject e = Instantiate(this.food_item) as GameObject;
    }

    #region support world bound support
    public enum WorldBoundStatus {
		CollideTop,
		CollideLeft,
		CollideRight,
		CollideBottom,
		Outside,
		Inside
	};

	public void UpdateWorldWindowBound()
	{
        // get the main 
        mMainCamera = Camera.main;
        mWorldBound = new Bounds(Vector3.zero, Vector3.one);
        float maxY = mMainCamera.orthographicSize;
		float maxX = mMainCamera.orthographicSize * mMainCamera.aspect;
		float sizeX = 2 * maxX;
		float sizeY = 2 * maxY;
		float sizeZ = Mathf.Abs(mMainCamera.farClipPlane - mMainCamera.nearClipPlane);

		// Make sure z-component is always zero
		Vector3 c = mMainCamera.transform.position;
		c.z = 0.0f;
		mWorldBound.center = c;
		mWorldBound.size = new Vector3(sizeX, sizeY, sizeZ);

		mWorldCenter = new Vector2(c.x, c.y);
		mWorldMin = new Vector2(mWorldBound.min.x, mWorldBound.min.y);
		mWorldMax = new Vector2(mWorldBound.max.x, mWorldBound.max.y);

	}

	public Vector2 WorldCenter { get { return mWorldCenter; } }
	public Vector2 WorldMin { get { return mWorldMin; }} 
	public Vector2 WorldMax { get { return mWorldMax; }}

	public WorldBoundStatus ObjectCollideWorldBound(Bounds objBound)
	{
		WorldBoundStatus status = WorldBoundStatus.Inside;

		if (mWorldBound.Intersects(objBound)) {
			if (objBound.max.x > mWorldBound.max.x)
				status = WorldBoundStatus.CollideRight;
			else if (objBound.min.x < mWorldBound.min.x)
				status = WorldBoundStatus.CollideLeft;
			else if (objBound.max.y > mWorldBound.max.y)
				status = WorldBoundStatus.CollideTop;
			else if (objBound.min.y < mWorldBound.min.y)
				status = WorldBoundStatus.CollideBottom;
			else if ( (objBound.min.z < mWorldBound.min.z) || (objBound.max.z > mWorldBound.max.z))
				status = WorldBoundStatus.Outside;
		} else 
			status = WorldBoundStatus.Outside;
		
		return status;
	}
	#endregion

	// 
	public void SetCurrentLevel(string level) {
		mCurrentLevel = level;
    }

	public void PrintCurrentLevel()
	{
		Debug.Log("Current Level is: " + mCurrentLevel);
	}
}
