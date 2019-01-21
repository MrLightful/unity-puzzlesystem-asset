using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleSystem
{
    [AddComponentMenu("PuzzleSystem/Core/Logic")]
    /// <summary>
    /// The class contains basic (empty) puzzle logic.
    /// By default, it just counts triggered events and 
    /// is being solved whenever all triggers were encountered.
    /// </summary>
    [RequireComponent(typeof(CorePuzzleHandler))]
    public class CorePuzzleLogic : MonoBehaviour
    {

        #region Variables

        [Space(10)]

        [SerializeField]
        [Tooltip("Objects that will trigger next step in solving the puzzle.")]
        /// <summary>
        /// Objects that will trigger next step in solving the puzzle.
        /// </summary>
        protected CorePuzzleTrigger[] triggers = new CorePuzzleTrigger[0];

        [SerializeField]
        [Tooltip("Should the puzzle be automatically reset as soon as it has been solved or failed?")]
        /// <summary>
        /// Should the puzzle be automatically reset as soon as it has been solved or failed?
        /// </summary>
        protected bool autoReset = false;


        /// <summary>
        /// The handler of the puzzle; will determine what will happen on solved or failed puzzle.
        /// </summary>
        protected CorePuzzleHandler handler;

        /// <summary>
        /// The amount of completed steps. The reach of the length of the triggers array means solved puzzle.
        /// </summary>
        protected int completedSteps = 0;

        /// <summary>
        /// Has this puzzle already been solved?
        /// </summary>
        protected bool isSolved = false;

        #endregion


        #region Methods

        public void SetTriggers(CorePuzzleTrigger[] newTriggers) 
        {
            triggers = newTriggers;
        }

        protected virtual void Awake()
        {
            handler = this.GetComponent<CorePuzzleHandler>();

            for (int i = 0; i < triggers.Length; i++)
            {
                triggers[i].Init(i, handler.debug);
                triggers[i].onPuzzleTriggerEvent += TriggerPuzzle;
            }
        }

        /// <summary>
        /// Resets the puzzle to the initial state and calls 'UnTrigger' on Triggers, so the player can interact with it again.
        /// </summary>
        public virtual void Reset() {

            completedSteps = 0;
            isSolved = false;

            foreach (var trigger in triggers)
            {
                trigger.UnTrigger();
            }

        }

        /// <summary>
        /// Accepts the solution for the puzzle.
        /// </summary>
        protected virtual void AcceptSolution() 
        {

            isSolved = true;

            if (autoReset)
                Reset();

            handler.Solve();

        }

        /// <summary>
        /// The method that will be passed through the delegates to all of the triggers.
        /// It will be invoked whenever the triggers are triggered.
        /// The id of the respective trigger will be passed.
        /// </summary>
        protected virtual void TriggerPuzzle(int id) {

            if (isSolved && !autoReset)
                return;

            completedSteps++;

            if (completedSteps == triggers.Length)
            {
                AcceptSolution();
            }

        }


        #endregion

    }

}