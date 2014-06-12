using UnityEngine;
using System.Collections;
using nobnak.Coroutine;

public class TestCorutine : MonoBehaviour {
	private bool _started = false;

	void OnGUI() {
		GUILayout.BeginVertical(GUILayout.Width(200));

		if (_started) {
			if (GUILayout.Button("Stop"))
				_started = false;
		} else {
			if (GUILayout.Button("Start")) {
				_started = true;
				StartCoroutine(Go ());
			}
		}

		GUILayout.EndVertical();
	}

	IEnumerator Go () {
		Debug.Log("Start");
		var c = ParallelCoroutine.Build(this, Process("A", 0.3f), Process("B", 0.5f), Process("C", 0.7f));
		yield return StartCoroutine(c);
		Debug.Log("Done");
	}

	IEnumerator Process(string label, float wait) {
		yield return null;
		var counter = 0;
		while (_started) {
			Debug.Log(string.Format("{0} : {1:d}", label, ++counter));
			yield return new WaitForSeconds(wait);
		}
	}

}
