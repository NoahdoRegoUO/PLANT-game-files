using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

	public void QuitGame ()
	{
		//Quits out of the game
		Debug.Log ("Quit");
		Application.Quit();
	}

	
}
