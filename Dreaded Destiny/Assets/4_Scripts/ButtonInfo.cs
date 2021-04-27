using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour{
	[SerializeField]
	private Text buttonTextField;

	public int optionPoints {get; private set; }
	public string buttonText {get; private set;	}
	public string afterText{get; private set; }
	public string nextChapter {get; private set;}

	private void Start() {
		buttonTextField = transform.GetComponentInChildren<Text>();
	}

	public void SetInfo(string btText, string afterDialog, string points, string chapterName) {
		if(buttonTextField != null) {
			buttonTextField.text = btText;
		}
		
		afterText			 = afterDialog;
		optionPoints		 = int.Parse(points);
		nextChapter			 = chapterName;
	}

	public void Clicked() {
		StoryReader.instance.currentChapter = nextChapter;		

		if(afterText != "" && afterText != null) {
			afterText = StoryReader.instance.CodeCheck(afterText);
			Manager.instance.dialogBox.text = afterText;
			Manager.instance.SetButtons();
		} else {			
			Manager.instance.NextText(true);
		}
	}
}
