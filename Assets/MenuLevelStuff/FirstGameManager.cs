using UnityEngine;

// Copied from http://forum.unity3d.com/threads/58609-DontDestroyOnLoad-Leading-to-Double-Objects
// scroll down to posting from JRavey
//
public class FirstGameManager : MonoBehaviour {

	#region support the creation and accesssing of a GlobalGameManager
	// Trick is to create a GlobalGameManager, only once!
	// Since this "GlobalGameManager" will persist over all scenes, it cannot be part of a scene (everything gets destroyed!)
	// So, we create this object the first time and re-use all over
	// 
	private static GlobalGameManager sTheGameState = null;

	private static void CreateGlobalManager()
	{
		GameObject newGameState = new GameObject();
		newGameState.name = "GlobalStateManager";
		newGameState.AddComponent<GlobalGameManager>();
		sTheGameState = newGameState.GetComponent<GlobalGameManager>();
		DontDestroyOnLoad (sTheGameState.gameObject);
	}

	public static GlobalGameManager TheGameState
    {
        get
        {
            // going to assume allready created.
            // Otherwise should do:
            //
            //    if (sTheGameState == null)
            //         CreateGlobalManager()
            //
            // before the return statement
            return sTheGameState;
        }
    }
	#endregion 	

    // Called even if the script component is not enableds
	void Awake() {
		if (null == sTheGameState) { // not here yet
			CreateGlobalManager();
            Debug.Log("Creating Global Manager!");
		}
			
	}

	// from this point on, ALL objects from all levels should be able to access
	// FirstGameManager.TheGameState
	void Start()
	{
		FirstGameManager.TheGameState.PrintCurrentLevel();
	}
}
