using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BugHuntUI : MonoBehaviour{
	public static BugHuntUI instance;

	public GameObject dialogBox;
	public Text speakerTxt;
	public Text dialogTxt;

	private int lineId = 0;
	private bool winstatus;
	private bool dialogMsg = false;

	private void Awake() {
		instance = this;
	}

	private void Start() {
		NextText();
	}

	public void KillWarning() {
		dialogMsg = false;
		speakerTxt.text = "Mya";
		dialogTxt.text = "Miss, don't kill them, be more careful while catching.";
		dialogBox.SetActive(true);
	}

	public void CollectText() {
		dialogMsg = false;
		speakerTxt.text = "Mya";
		dialogTxt.text = RandomCollectMsg();
		dialogBox.SetActive(true);
	}

	public void NextText() {
		if(dialogMsg) {
			if(lineId <= 0) {
				lineId++;
				EndDialog(winstatus);
			} else {
				ChangeScene();
			}
		} else {
			speakerTxt.text = "";
			dialogTxt.text = "";
			dialogBox.SetActive(false);
		}
	}

	public void EndDialog(bool gameWin) {
		winstatus = gameWin;

		if(winstatus) {
			WinText();
		} else {
			GameoverText();
		}
	}

	public void WinText() {
		dialogMsg = true;

		switch(lineId) {
			case 0:
				speakerTxt.text = "Rose";
				dialogTxt.text = "It's so pretty to see this many butterflies together. But it's time to let them free again.";
				break;
			case 1:
				speakerTxt.text = "Mya";
				dialogTxt.text = "They are indeed pretty. Once we let them free it's time for us to get back inside again.";
				break;
		}
	}

	public void GameoverText() {
		dialogMsg = true;

		switch(lineId) {
			case 0:
				speakerTxt.text = "Rose";
				dialogTxt.text = "Guess it's harder to catch them alive than I thought...";
				break;
			case 1:
				speakerTxt.text = "Mya";
				dialogTxt.text = "We should just leave them alone. Let's go back inside.";
				break;
		}
	}

	private string RandomCollectMsg() {
		string[] s = new string[5];
		
		s[0] = "Miss, you managed to catch one! Look how pretty it is.";
		s[1] = "Miss, this butterfly is so beautiful, look at its colors!";
		s[2] = "Miss, I've never seen a butterfly this big before!";

		int rid = Random.Range(0, s.Length);
		return s[rid];
	}

	public void ChangeScene() {
		//move to narative scene
		ScriptManager.SetNewFile("XML/StoryScript", "Chapter3");
		NewScene(1);
	}

	public void NewScene(int sceneId) {
		SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
		Resources.UnloadUnusedAssets();
		SceneManager.LoadScene(sceneId, LoadSceneMode.Single);
	}

}
