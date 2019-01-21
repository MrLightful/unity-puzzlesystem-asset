using System.Collections;
using UnityEngine;

namespace PuzzleSystem
{
    [AddComponentMenu("PuzzleSystem/Standard Logics/Simult w Delay Logic")]
    [HelpURL("https://puzzlesystem.gitbook.io/project/manual/standard-logics/simultdelayedpuzzlelogic")]
    /// <summary>
    /// The class contains logic for a specific type of puzzles.
    /// The triggers are triggered for some time specified by the delay variable, and then they are deactivated back to the initial state.
    /// In order to complete the puzzle, player must activate all triggers very quickly, 
    /// before the first activated trigger goes off again.
    /// </summary>
    public class SimultDelayedPuzzleLogic : CorePuzzleLogic
    {

        #region Variables

        [Space(10)]

        [SerializeField]
        [Tooltip("How long the triggers will be active after triggering?")]
        private float delay = 3f;

        [SerializeField]
        [Tooltip("Should the triggers be reset, after the given delay, even if the puzzle was meanwhile solved?")]
        protected bool delayedSolvedReset = false;

        [SerializeField]
        [Tooltip("Should the triggers be reset, after the given delay, even if the puzzle was meanwhile failed? Otherwise, they will be reset all together as soon as the puzzle is failed.")]
        protected bool delayedFailedReset = false;

        #endregion


        #region Methods

        // The method that will be passed through the delegates to all of the triggers.
        // It will be invoked whenever the triggers are triggered.
        // The id of the respective trigger will be passed.
        protected override void TriggerPuzzle(int id)
        {
            if (IsSolved || IsFailed)
                return;

            StartCoroutine(TriggerDelayThread(id));

            // If the count of completed steps reached length of the triggers array,
            // it means that all triggers are still active.
            // Thus, the puzzle is solved.
            if (completedSteps == triggers.Length)
            {
                AcceptSolution();
            }
        }

        // This type of puzzle logic needs a special callback on teh solved event.
        protected override void AcceptSolution()
        {
            IsSolved = true;

            // Instead of resetting the puzzle immediately,
            // Give time for triggers to untrigger over the delay.
            if (autoResetSolution && !delayedSolvedReset)
                Reset();

            handler.Solve();
        }

        // The Coroutine that holds the active state of the trigger, and then disables it.
        protected IEnumerator TriggerDelayThread(int id) {

            completedSteps++;

            yield return new WaitForSeconds(delay);

            if (!IsSolved && !IsFailed)
            {

                handler.Fail();
                IsFailed = true;

                if (autoResetFailure && !delayedFailedReset)
                {
                    Reset();
                    StopAllCoroutines();
                }
            

            }

            if ((IsFailed && autoResetFailure) || (IsSolved && autoResetSolution) || (!IsSolved && !IsFailed))
            {
                triggers[id].UnTrigger();
                completedSteps--;
            }

        }

        #endregion

    }

}