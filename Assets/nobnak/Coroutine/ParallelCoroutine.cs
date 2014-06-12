using UnityEngine;
using System.Collections;

namespace nobnak.Coroutine {

	public static class ParallelCoroutine {
		public static IEnumerator Build(MonoBehaviour target, params IEnumerator[] coroutines) {
			var monitors = new Monitor[coroutines.Length];
			for (var i = 0; i < coroutines.Length; i++)
				target.StartCoroutine(monitors[i] = new Monitor(coroutines[i]));
			while (!AllDone(monitors))
				yield return null;
		}

		public static bool AllDone(Monitor[] monitors) {
			foreach (var m in monitors)
				if (m.Progress != Monitor.State.Done)
					return false;
			return true;
		}
	}


	public class Monitor : IEnumerator {
		public enum State { Reset = 0, Playing, Done }

		private IEnumerator _coroutine;

		public Monitor(IEnumerator coroutine) {
			this._coroutine = coroutine;
		}

		public State Progress { get; private set; }

		#region IEnumerator implementation
		public bool MoveNext () { 
			var flag = _coroutine.MoveNext(); 
			Progress = (flag ? State.Playing : State.Done);
			return flag;
		}
		public void Reset () { 
			Progress = State.Reset;
			_coroutine.Reset(); 
		}
		public object Current { get { return _coroutine.Current; } }

		#endregion
	}
}