using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PuzzleSystem.Extras
{
    [AddComponentMenu("PuzzleSystem/Extras/Timer")]
    [RequireComponent(typeof(CorePuzzleLogic))]
    [RequireComponent(typeof(CorePuzzleHandler))]
    public class TimerToSolvePuzzle : MonoBehaviour
    {

        #region Variables

        [SerializeField]
        private float timer;

        [SerializeField]
        private bool activateTimerOnStart = false;

        [SerializeField]
        [Tooltip("Force reset of the puzzle's elements on failure? Might be important to consider this option to be 'false' when, for example, SimultDelayedPuzzleLogic is used.")]
        private bool forceResetOnTimeoutFail = true;

        [Space(30)]

        [SerializeField]
        [Tooltip("The Events that will happen whenever the player fails the puzzle due to the timeout.")]
        protected UnityEvent onTimoutFailEvent;

        private CorePuzzleLogic logic;
        private CorePuzzleHandler handler;

        #endregion


        #region Public Methods

        /// <summary>
        /// Gets the timer count.
        /// </summary>
        /// <returns>The timer.</returns>
        public float GetTimer()
        {
            return timer;
        }

        /// <summary>
        /// Sets the timer count.
        /// </summary>
        /// <param name="val">Value.</param>
        public void SetTimer(float val)
        {
            if (handler.debug)
                Debug.Log("[PuzzleSystem] The timer's count of the puzzle " + gameObject.name + " was updated!");

            timer = val;
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void StartTimer()
        {
            if (handler.debug)
                Debug.Log("[PuzzleSystem] Timer of the puzzle " + gameObject.name + " is counting down!");

            StartCoroutine(TimerCoroutine());
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void StopTimer()
        {
            if (handler.debug)
                Debug.Log("[PuzzleSystem] Timer of the puzzle " + gameObject.name + " is interupted!");

            StopAllCoroutines();
        }

        #endregion


        #region Private Methods

        private void Start()
        {
            logic = GetComponent<CorePuzzleLogic>();
            handler = GetComponent<CorePuzzleHandler>();

            if (activateTimerOnStart)
                StartTimer();
        }

        private IEnumerator TimerCoroutine() 
        {
            yield return new WaitForSeconds(timer);

            if (logic.IsSolved)
                yield break;

            if (handler.debug)
                Debug.Log("[PuzzleSystem] Timer of the puzzle " + gameObject.name + " is out of count! The puzzle is failed!");

            onTimoutFailEvent.Invoke();
            logic.ForceFail(forceResetOnTimeoutFail);

        }

        #endregion


    }

}
