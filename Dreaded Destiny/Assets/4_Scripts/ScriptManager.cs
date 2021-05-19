using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScriptManager {
	public static string ScriptFileName {
		get;
		private set;
	}

	public static string ScriptChapter {
		get;
		private set;
	}

	public static void SetNewFile(string fileName, string chapterName) {
		ScriptFileName = fileName;
		ScriptChapter = chapterName;
	}
}
