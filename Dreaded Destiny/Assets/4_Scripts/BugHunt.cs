using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
			StartCoroutine(DebugMsg(true));
			if(grabCount >= goalCount) {
				gameover = true;
				gameWin.SetActive(true);
			}
		} else {
			killCount++;
			killCountText.text = "Lives: " + killCount + "/3";
			StartCoroutine(DebugMsg(false));
			if(killCount >= endCount) {
				gameover = true;
				gameOverKill.SetActive(true);
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

	public void Dissapear() {
		dissapearCount++;

		if(dissapearCount >= bugs.Length && !gameover) {		
			gameover = true;
			if (gameOverGone != null)
				gameOverGone.SetActive(true);
		}
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
