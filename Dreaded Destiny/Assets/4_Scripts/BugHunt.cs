using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BugHunt : MonoBehaviour {
	public static BugHunt instance;
	[SerializeField] private GameObject[] bugs;
	[SerializeField] private int goalCount = 5, endCount = 3, spawnBurst = 5;
	[SerializeField] private float burstDelay = 2f;

	[SerializeField]
	private GameObject gameOverKill, gameOverGone, gameWin, startBt;
	
	private int grabCount, killCount, dissapearCount;
	private bool gameover;

	//---------debug
	public Text catchCountText, killCountText;
	public GameObject catchMsg, killMsg;


	private void Awake() {
		instance = this;
	}

	private void Start() {
		gameover = false;
		gameWin.SetActive(false);
		gameOverGone.SetActive(false);
		gameOverKill.SetActive(false);
		Time.timeScale = 0;
		startBt.SetActive(true);

		catchMsg.SetActive(false);
		killMsg.SetActive(false);

		for(int i = 0; i < bugs.Length; i++) {
			bugs[i].SetActive(false);
		}
	}

	public void StartPuzzle() {
		startBt.SetActive(false);
		Time.timeScale = 1;
		StartCoroutine(BugSpawnBurst());
	}

	public void OnInteract(bool grab) {
		if(grab) {
			grabCount++;
			catchCountText.text = "Catch: " + grabCount + "/5";
			BugHuntUI.instance.CollectText();
			//StartCoroutine(DebugMsg(true));
			//StartCoroutine(RemoveMsgOverTime());
			if(grabCount >= goalCount) {
				GameEnd(true);
				//gameWin.SetActive(true);
			}
		} else {
			killCount++;
			killCountText.text = "Lives: " + killCount + "/3";
			BugHuntUI.instance.KillWarning();
			//StartCoroutine(DebugMsg(false));
			//StartCoroutine(RemoveMsgOverTime());
			if(killCount >= endCount) {
				GameEnd(false);			
				//gameOverKill.SetActive(true);
			}
		}
	}

	IEnumerator DebugMsg(bool catched) {
		if(catched) {
			catchMsg.SetActive(true);
			
			yield return new WaitForSeconds(1f);
			catchMsg.SetActive(false);
		} else {
			killMsg.SetActive(true);
			
			yield return new WaitForSeconds(1f);
			killMsg.SetActive(false);
		}
	}

	IEnumerator RemoveMsgOverTime() {
		yield return new WaitForSeconds(2.5f);
		BugHuntUI.instance.NextText();
	}

	public void Dissapear() {
		dissapearCount++;

		if(dissapearCount >= bugs.Length && !gameover) {
			GameEnd(false);
			//if (gameOverGone != null)
				//gameOverGone.SetActive(true);
		}
	}

	public void GameEnd(bool winStatus) {
		gameover = true;
		BugHuntUI.instance.EndDialog(winStatus);
	}

	IEnumerator BugSpawnBurst() {
		int bugsSpawned = 0;

		while(bugsSpawned < bugs.Length) {
			for(int i = 0; i < spawnBurst; i++) {
				if(bugsSpawned >= bugs.Length) {
					break;
				}

				bugs[bugsSpawned].SetActive(true);
				bugsSpawned++;
				yield return null;
			}

			yield return new WaitForSeconds(burstDelay);
		}
	}
}
