using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class MainMenuGUI : MonoBehaviour {

	public AudioClip beep;
	public GUISkin menuSkin;
	public Rect menuArea;
	public Rect playButton;
	public Rect instructionsButton;
	public Rect quitButton;
	public Rect instructions;
	
	Rect menuAreaNormalized;
	
	string menuPage = "main";
	
	// Use this for initialization
	void Start () 
	{
		menuAreaNormalized = new Rect(menuArea.x * Screen.width - (menuArea.x * 0.5f), menuArea.y * Screen.height - (menuArea.y * 0.5f), menuArea.width, menuArea.height);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnGUI()
	{
		GUI.skin = menuSkin;
		GUI.BeginGroup(menuAreaNormalized);
		
		if(menuPage == "main")
		{		
			if(Application.CanStreamedLevelBeLoaded("Island"))
			{
				if(GUI.Button(new Rect(playButton), "Play"))
				{
					StartCoroutine("ButtonAction", "Island");
				}
			}
			else
			{
				float percentLoaded = Application.GetStreamProgressForLevel(1) * 100;
				GUI.Box(new Rect(playButton), "Loading.." + percentLoaded.ToString("f0") + "% Loaded");
			}
			if(GUI.Button(new Rect(instructionsButton), "Instructions"))
			{
				audio.PlayOneShot(beep);
				menuPage="instructions";
			}
			
			if(Application.platform != RuntimePlatform.OSXWebPlayer && Application.platform != RuntimePlatform.WindowsWebPlayer)
			{
				if(GUI.Button(new Rect(quitButton), "Quit"))
				{
					StartCoroutine("ButtonAction", "quit");
				}
			}
		}
		else if(menuPage == "instructions")
		{
			GUI.Label(new Rect(instructions),"You awake on a mysterious island... Find a way to signal for help or face certain doom!");
			//You awake on a mysterious island... Find a way to signal for help or face certain doom!
			//Budzisz sie na tajemniczej wyspie... Znajdz sposob, aby nadac sygnal o pomoc lub czekaj na pewna zgube!
			if(GUI.Button(new Rect(quitButton), "Back"))
			{
				audio.PlayOneShot(beep);
				menuPage="main";
			}
		}
		
		GUI.EndGroup();
	}
	
	IEnumerator ButtonAction(string levelName)
	{
		audio.PlayOneShot(beep);
		yield return new WaitForSeconds(0.35f);
		
		if(levelName != "quit")
		{
			Application.LoadLevel(levelName);
		}
		else
		{
			Application.Quit();
			Debug.Log("Have Quit");
		}
	}
	
}
