using UnityEngine;
using UnityEngine.Events;
using System;

namespace PuzzleSystem
{
    [AddComponentMenu("PuzzleSystem/Core/Core Handler")]
    [HelpURL("https://puzzlesystem.gitbook.io/project/manual/cores/handler")]
    /// <summary>
    /// The class contains basic event handler and represents a single puzzle system.
    /// It specifies what actions to be taken when the player fails in solving puzzle or succeeds.
    /// You can also enable debugging option for the system, so it will log events into the console.
    /// </summary>
    public class CorePuzzleHandler : MonoBehaviour
    {
    

        #region Variables

        [Space(20)]

        [SerializeField]
        [Tooltip("The Events that will happen whenever the player solves the puzzle.")]
        protected UnityEvent onSolutionEvent;

        [SerializeField]
        [Tooltip("The Events that will happen whenever the player makes a mistake in solving the puzzle.")]
        protected UnityEvent onFailureEvent;


        [Tooltip("Debug this specific puzzle system into the console?")]
        /// <summary>
        /// Debug this specific puzzle system into the console?
        /// </summary>
        public bool debug = false;

        #endregion


        #region Public Methods

        /// <summary>
        /// The method invokes the events that represent the solved puzzle.
        /// </summary>
        public void Solve()
        {
            if (debug)
                Debug.Log("[PuzzleSystem] The puzzle '" + this.name + "' has been solved!");

            onSolutionEvent.Invoke();
            OnSolution();
        }

        /// <summary>
        /// The method invokes the events that represent the solved puzzle.
        /// </summary>
        public void Fail()
        {
            if (debug)
                Debug.Log("[PuzzleSystem] The puzzle '" + this.name + "' has been failed!");

            onFailureEvent.Invoke();
            OnFailure();
        }

        #endregion


        #region Protected Virtual Methods

        /// <summary>
        /// The function that is triggered whenever the player has solved the puzzle.
        /// </summary>
        /// <exception cref="T:System.NotImplementedException"></exception>
        protected virtual void OnSolution()
        {

            if (this.GetType().IsSubclassOf(typeof(CorePuzzleHandler)))
                throw new NotImplementedException();

        }

        /// <summary>
        /// The function that is triggered whenever the player makes a mistake in solving the puzzle.
        /// </summary>
        protected virtual void OnFailure() 
        {

            if (this.GetType().IsSubclassOf(typeof(CorePuzzleHandler)))
                throw new NotImplementedException();

        }

        #endregion


    }

}