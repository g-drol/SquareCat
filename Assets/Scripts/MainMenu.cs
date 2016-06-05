using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public string firstScene;

	public void StartGame()
	{
			SceneManager.LoadScene(firstScene, LoadSceneMode.Single);
	}
}
