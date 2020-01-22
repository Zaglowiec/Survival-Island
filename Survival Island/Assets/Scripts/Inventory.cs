using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

	public static int charge = 0;
	public AudioClip collectSound;
	//HUD
	public Texture2D[] hudCharge;
	public GUITexture chargeHudGUI;
	//Generator
	public Texture2D[] meterCharge;
	public Renderer meter;
	//Zapalki
	bool haveMatches = false;
	public GUITexture matchGUIprefab;
	GUITexture matchGUI;
	public GUIText textHints;
	
	bool fireIsLit = false;
	
	public GameObject winObj;
	
	// Use this for initialization
	void Start () 
	{
		charge = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void CellPickup()
	{
		HUDon();
		AudioSource.PlayClipAtPoint(collectSound, transform.position);
		charge++;
		chargeHudGUI.texture = hudCharge[charge];
		meter.material.mainTexture = meterCharge[charge];
	}
	
	void HUDon()
	{
		if(!chargeHudGUI.enabled)
		{
			chargeHudGUI.enabled = true;
		}
	}
	
	void MatchPickup()
	{
		haveMatches = true;
		AudioSource.PlayClipAtPoint(collectSound, transform.position);
		GUITexture matchHUD = Instantiate(matchGUIprefab, new Vector3(0.15f, 0.1f, 0), transform.rotation) as GUITexture;
		matchGUI = matchHUD;
	}
	
	void OnControllerColliderHit(ControllerColliderHit col)
	{
		if(col.gameObject.name == "campfire")
		{
			if(haveMatches && !fireIsLit)
			{
				LightFire(col.gameObject);
			}
			else if(!haveMatches && !fireIsLit)
			{
				textHints.SendMessage("ShowHint", "I could use this campfire to signal for help... if only I could light it...");
					//I could use this campfire to signal for help... if only I could light it...
					//Powiniennem uzyc ogniska do wezwania pomocy... gdybym tylko znalazl zapalki...
			}
		}
	}
	
	void LightFire(GameObject campfire)
	{
		
		ParticleSystem[] fireEmitters;
		fireEmitters = campfire.GetComponentsInChildren<ParticleSystem>();
		
		foreach(ParticleSystem emission in fireEmitters)
		{
			emission.Play();
		}
		
		campfire.audio.Play();
		Destroy(matchGUI);
		haveMatches=false;
		fireIsLit=true;
		
		winObj.SendMessage("GameOver");
	}
	
}
