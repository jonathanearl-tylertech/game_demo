using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// for SceneManager

public class LoadSceneSupport : MonoBehaviour {

	public string LevelName = null;

    public Button mStart;
    public Button mExit;

	private Button mainMenu = null; 

	// Use this for initialization
	void Start () {
        // Workflow assume:
        //      mLevelOneButton: is dragged/placed from UI

        // add in listener
        mStart.onClick.AddListener(
                () => {                     // Lamda operator: define an annoymous function
				if(Time.timeScale == 0) Time.timeScale = 1;
                LoadScene("Jump");
                });
        mExit.onClick.AddListener(
                () => {                     // Lamda operator: define an annoymous function
                    Application.Quit();
                });
		
		mainMenu = GameObject.Find ("GameOverCanvas/GameOverPanel/MainMenu").GetComponent<Button> ();
		if (mainMenu != null) {
			mainMenu.onClick.AddListener (
				() => {                     // Lamda operator: define an annoymous function
					if (Time.timeScale == 0)
						Time.timeScale = 1;
					LoadScene ("Menu");
				});
		}
	}

    // Update is called once per frame
    void Update () {
	
	}
    
	void LoadScene(string theLevel) {
        SceneManager.LoadScene(theLevel);
	}
}