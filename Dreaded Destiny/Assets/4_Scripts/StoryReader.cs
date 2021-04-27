using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;


public class StoryReader:MonoBehaviour {
	public static StoryReader instance;
	private XmlDocument storyDoc;

	public string currentChapter {
		get; set;
	}
	public int currentLineId {
		get; private set;
	}

	public string scriptFileName;
	protected int buttonObjectId = 0;

	private void Awake() {
		instance = this;
	}

	public void ChangeScriptFile(string newFileName) {
		scriptFileName = newFileName;
		currentChapter = "Chapter1";
		currentLineId = 0;
	}

	public void FindData() {
		TextAsset storyFile = Resources.Load<TextAsset>(scriptFileName);
		XmlDocument doc = new XmlDocument();
		doc.LoadXml(storyFile.text);

		string chapter = currentChapter;
		XmlNodeList nl = doc.GetElementsByTagName(chapter);

		if(nl[0] != null) {
			if(currentLineId < GetLineCount(nl)) {
				GetXmlLineInfo(nl);
				currentLineId++;

				if(currentLineId == GetLineCount(nl)) {
					for(int i = 0; i < GetButtonCount(nl); i++) {
						GetXmlButtonInfo(nl, i);
					}

					Manager.instance.buttonsActive = true;
					currentLineId = 0;
				}
			} else {
				Debug.Log("Error?");
			}
		} else {
			Debug.LogError("Missing Chapter.");
		}
	}

	protected int GetLineCount(XmlNodeList nl) {
		return nl[0].ChildNodes[1].ChildNodes.Count;
	}

	protected int GetButtonCount(XmlNodeList nl) {
		return nl[0].ChildNodes[2].ChildNodes.Count;
	}

	protected void GetXmlLineInfo(XmlNodeList nl) {
		string str = "";
		str = nl[0].ChildNodes[1].ChildNodes[currentLineId].ChildNodes[0].Value;

		if(!str.Contains("[@]") && str != null) {
			str = CodeCheck(str);
			Manager.instance.dialogBox.text = str;
		} else if(str.Contains("[@]")) {
			CodeCheck(str);
			str = "";
			Manager.instance.dialogBox.text = str;
		}
	}

	protected void GetXmlButtonInfo(XmlNodeList nl, int buttonId) {
		string buttonText = "", afterText = "", points = "", nextChapter = "", bId = "";

		buttonText = nl[0].ChildNodes[2].ChildNodes[buttonId].ChildNodes[0].ChildNodes[0].ChildNodes[0].Value;
		afterText = nl[0].ChildNodes[2].ChildNodes[buttonId].ChildNodes[0].ChildNodes[1].ChildNodes[0].Value;	
		points = nl[0].ChildNodes[2].ChildNodes[buttonId].ChildNodes[1].ChildNodes[0].Value;
		nextChapter = nl[0].ChildNodes[2].ChildNodes[buttonId].ChildNodes[2].ChildNodes[0].Value;
		bId = nl[0].ChildNodes[2].ChildNodes[buttonId].ChildNodes[3].ChildNodes[0].Value;

		//buttontext
		if(buttonText == null || buttonText == "[@]") {
			buttonText = "";
		}

		//aftertext
		if(afterText == null || afterText.Contains("[@]")) {
			afterText = afterText.Replace("[@]", "");
		}

		//set info & enable button
		buttonObjectId = int.Parse(bId);
		Manager.instance.btInfo[buttonObjectId].SetInfo(buttonText, afterText, points, nextChapter);
		Manager.instance.btInfo[buttonObjectId].gameObject.SetActive(true);
	}

	public string CodeCheck(string s) {

		if(s.Contains("[")) {
			if(s.Contains("[\n]")) {
				//s = s.Replace('$', '\n');
				s = s.Replace("[\n]", "\n");
			}

			s = NameCheck(s);
			s = FaceImages(s);
			s = CheckBackground(s);		
			s = CheckSoundEffects(s);
		}

		return s;
	}

	protected virtual string NameCheck(string s) {
		if(s.Contains("[Rose]")) {
			s = s.Replace("[Rose]", "");
			Manager.instance.speakerNameText.text = "Rose";
		} else if(s.Contains("[Narrator]")) {
			s = s.Replace("[Narrator]", "");
			Manager.instance.speakerNameText.text = "";
		} else if(s.Contains("[Father]")) {
			s = s.Replace("[Father]", "");
			Manager.instance.speakerNameText.text = "Father";
		} else if(s.Contains("[Mother]")) {
			s = s.Replace("[Mother]", "");
			Manager.instance.speakerNameText.text = "Mother";
		} else if(s.Contains("[Emperor]")) {
			s = s.Replace("[Emperor]", "");
			Manager.instance.speakerNameText.text = "Emperor";
		} else if(s.Contains("[Mya]")) {
			s = s.Replace("[Mya]", "");
			Manager.instance.speakerNameText.text = "Mya";
		} else if(s.Contains("[Maid]")) {
			s = s.Replace("[Maid]", "");
			Manager.instance.speakerNameText.text = "Maid";
		}

		return s;
	}

	protected string FaceImages(string s) {
		if(!s.Contains("[Face:")) {
			return s;
		}

		return s;
	}

	protected string CheckSoundEffects(string s) {
		if(!s.Contains("[Audio:") && !s.Contains("[BGM:")) {
			return s;
		}

		//sound effects
		if(s.Contains("[Audio:happy]")) {
			s = s.Replace("[Audio:happy]", "");
		}

		//bgm
		if(s.Contains("[BGM:date]")) {
			s = s.Replace("[BGM:happy]", "");
			//Manager.instance.bgmEffect.PlayLoopBGM("date");
		} 

		return s;
	}

	protected string CheckBackground(string s) {
		if(!s.Contains("[BG:")) {
			return s;
		}

		switch(s) {
			case "[BG:Emperor]":
				s = s.Replace("[BG:Emperor]", "");
				Manager.instance.ChangeBackgrounds(0);
				break;
			case "[BG:Home]":
				s = s.Replace("[BG:Home]", "");
				Manager.instance.ChangeBackgrounds(1);
				break;
			case "[BG:Garden]":
				s = s.Replace("[BG:Garden]", "");
				Manager.instance.ChangeBackgrounds(2);
				break;
		}

		return s;
	}
}