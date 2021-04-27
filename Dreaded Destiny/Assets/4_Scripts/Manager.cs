using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour{
	public static Manager instance;

	public ButtonInfo[] btInfo;
	public Text dialogBox;
	
	public GameObject[] endingScreens;
	public SpriteRenderer backgroundObj;
	public Sprite[] backgroundSprites;
	public Text speakerNameText;

	public bool buttonsActive = false;
	public static int currentSceneId;

	private void Awake() {
		instance = this;
	}

	private void Start() {
		ChangeBackgrounds(0);

		for(int i = 0; i < endingScreens.Length; i++) {
			endingScreens[i].SetActive(false);
		}

		if(currentSceneId > 0) {
			StoryReader.instance.ChangeScriptFile("XML/StoryScript");
			NextText(false);
		} else {
			currentSceneId++;
		}
	}

	private void Update() {
		if(Input.GetButtonDown("Jump") && !buttonsActive) {
			NextText(false);
		}
	}

	public void NextText(bool button) {
		if(button || !buttonsActive) {
			if(!CheckGameOver()) {
				SetButtons();
				StoryReader.instance.FindData();
			}
		}
	}

	public void SetButtons() {
		buttonsActive = false;

		for(int i = 0; i < btInfo.Length; i++) {
			btInfo[i].gameObject.SetActive(false);
		}
	}

	bool CheckGameOver() {
		string chapter = StoryReader.instance.currentChapter;

		if(!chapter.Contains("Ending")) {
			return false;
		} else {
			int endingId;
			chapter = chapter.Replace("Ending", "");
			endingId = int.Parse(chapter);
			endingScreens[endingId - 1].SetActive(true);
			return true;
		}
	}
	public void RestartButton() {
		SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
		Resources.UnloadUnusedAssets();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}

	public void NewScene(string sceneName) {
		SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
		Resources.UnloadUnusedAssets();
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
	}

	public void NewScene(int sceneId) {
		SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
		Resources.UnloadUnusedAssets();
		SceneManager.LoadScene(sceneId, LoadSceneMode.Single);
	}

	public void ChangeBackgrounds(int enableId) {
		if(enableId < backgroundSprites.Length) {
			backgroundObj.sprite = backgroundSprites[enableId];
		} 
	}

	//test
	public IEnumerator TextOverTime(string text) {
		string[] s = text.Split(' ');

		dialogBox.text = "";

		for(int i = 0; i < s.Length; i++) {
			dialogBox.text += s[i];
			if(i < s.Length - 1) dialogBox.text += "";
			yield return new WaitForSeconds(0);
		}
	}
}
