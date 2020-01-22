using UnityEngine;
using System.Collections;

public class ThrowTigger : MonoBehaviour {

	public GUITexture crosshair;
	public GUIText textHints;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			CoconutThrower.canThrow = true;
			crosshair.enabled=true;
			if(!CoconutWin.haveWon)
			{
				textHints.SendMessage("ShowHint", "\n\n\n\n\n There's power cell attached to this game, \n maybe I'll win if I can knock down all the targets...");
				// \n\n\n\n\n There's power cell attached to this game, \n maybe I'll win if I can knock down all the targets...
				//\n\n\n\n\n Po prawej stronie znajduje sie pojemnik z ogniwem, \n moze jesli wygram skrzynka z zawartoscia sie otworzy
			}
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			CoconutThrower.canThrow = false;
			crosshair.enabled=false;
		}
	}
	
}
