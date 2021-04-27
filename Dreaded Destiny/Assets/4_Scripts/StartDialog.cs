using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartDialog : StoryReader{
    [SerializeField]
    private Color roseColor, myaColor, narratorColor;
	[SerializeField]
	private GameObject startMenu;

	void Start(){
        ChangeScriptFile("XML/WarmupScript");
		startMenu.SetActive(true);
		Manager.currentSceneId = 0;
	}

	public void StartGame() {
		startMenu.SetActive(false);		
		Manager.instance.NextText(true);
	}

	public void NewScene(int sceneId) {
		ChangeScriptFile("XML/StoryScript");
		Manager.instance.NewScene(sceneId);
	}

	protected override string NameCheck(string s) {
		if(s.Contains("[Rose]")) {
			s = s.Replace("[Rose]", "");
			Manager.instance.dialogBox.color = roseColor;

		} else if(s.Contains("[Mya]")) {
			s = s.Replace("[Mya]", "");
			Manager.instance.dialogBox.color = myaColor;

		} else if(s.Contains("[Narrator]")) {
			s = s.Replace("[Narrator]", "");
			Manager.instance.dialogBox.color = narratorColor;

		} else {
			Manager.instance.dialogBox.color = Color.black;
		}

		return s;
    }
}
