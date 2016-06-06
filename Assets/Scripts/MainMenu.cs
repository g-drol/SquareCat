using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public string firstScene;

	public void StartGame()
	{
		SceneManager.LoadScene(firstScene, LoadSceneMode.Single);
	}

	public void LoadScene(string loadedScene){
		SceneManager.LoadScene (loadedScene, LoadSceneMode.Single);
	}
}
