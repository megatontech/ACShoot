﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class S506Puppeteer : CutscenePuppeteer {

	public AudioClip elevNoise;

	private GameObject ChefTony;
	private bool elevTriggered = false;
		
	// Use this for initialization
	void Start () {
		// get all the objects we'll need for the cutscene 
		ChefTony = GameObject.Find ("Chef Tony");
	}

	public override void OnEnable() {
		base.OnEnable();

		BattleController.OnBattleEvent += HandleBattleEvent;
	}
	
	
	public override void OnDisable() {
		base.OnDisable();

		BattleController.OnBattleEvent -= HandleBattleEvent;
	}

	
	// Update is called once per frame
	public override void Update () {
		base.Update();

		if(CurrentScene == 0) {
			if(ChefTony.transform.position.x > -1.79) {
				ChefTony.GetComponent<PlayerFreeze>().Freeze();

				//start the cutscene
				nextScene();
			}
		}

		if(ChefTony.transform.position.x > 2.4 && !elevTriggered) {
			// actions that must run before the elevator activates
			ChefTony.GetComponent<PlayerFreeze>().Freeze();

			playSound(elevNoise);
			ChefTony.GetComponent<Renderer>().enabled = false;

			//fade out
			ChefTony.transform.position = new Vector2(ChefTony.transform.position.x, -1.2f);
			
			FadeAndNext(Color.black, 2.5f, "5-07 Control Room", true);

			elevTriggered = true;
		}

		if(elevTriggered) {
			//FLY, CHEF
			ChefTony.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 15);
		}

	}

	public override void HandleSceneChange() {
		// once the text is ready, start the battle
		if(CurrentScene == 3)
			GetComponent<BattleController>().StartBattle();
		else if(CurrentScene == 9)  
			ChefTony.GetComponent<PlayerFreeze>().UnFreeze();
	}

	public void HandleBattleEvent(BattleEvent type) {
		if(type == BattleEvent.Finished) {
			nextScene();
		}
	}
}
