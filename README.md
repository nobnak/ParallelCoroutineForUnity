Parallel Coroutine Execution for Unity
=========================
This utility let the coroutine suspend its execution until all the given coroutines finish.


## Example
```c#
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
```
