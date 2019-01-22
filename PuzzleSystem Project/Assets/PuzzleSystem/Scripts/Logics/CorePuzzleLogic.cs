using UnityEngine;

namespace PuzzleSystem
{
    [AddComponentMenu("PuzzleSystem/Core/Core Logic")]
    [HelpURL("https://puzzlesystem.gitbook.io/project/manual/cores/logic")]
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
        [Tooltip("Should the puzzle be automatically reset as soon as it has been failed?")]
        /// <summary>
        /// Should the puzzle be automatically reset as soon as it has been solved or failed?
        /// </summary>
        protected bool autoResetFailure = false;

        [SerializeField]
        [Tooltip("Should the puzzle be automatically reset as soon as it has been solved?")]
        protected bool autoResetSolution = false;

        public bool IsAutoResetFailure {
            get {
                return autoResetFailure;
            }
        }

        public bool IsAutoResetSolution
        {
            get
            {
                return autoResetSolution;
            }
        }


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
        public bool IsSolved {
            get;
            protected set;
        }

        public bool IsFailed {
            get;
            protected set;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Forces the solve of the puzzle.
        /// </summary>
        public void ForceSolve() 
        {
            IsFailed = false;

            AcceptSolution();
        }

        /// <summary>
        /// Forces the fail of the puzzle.
        /// </summary>
        /// <param name="immediate">If set to <c>true</c>, resets the puzzle's elements immediately, regardless of internal implementations.</param>
        public void ForceFail(bool immediate = true) 
        {
            IsFailed = true;
            IsSolved = false;

            if (autoResetFailure && immediate)
                Reset();
        }

        /// <summary>
        /// Sets the triggers.
        /// </summary>
        /// <param name="newTriggers">New triggers.</param>
        public void SetTriggers(CorePuzzleTrigger[] newTriggers)
        {
            triggers = newTriggers;
        }

        protected virtual void Awake()
        {
            handler = this.GetComponent<CorePuzzleHandler>();

            for (int i = 0; i < triggers.Length; i++)
            {
                triggers[i].Init(this, i, handler.debug);
                triggers[i].onPuzzleTriggerEvent += TriggerPuzzle;
            }
        }

        /// <summary>
        /// Resets the puzzle to the initial state and calls 'UnTrigger' on Triggers, so the player can interact with it again.
        /// </summary>
        public virtual void Reset() {

            completedSteps = 0;
            IsSolved = false;
            IsFailed = false;

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

            IsSolved = true;

            if (autoResetSolution)
                Reset();

            handler.Solve();

        }

        /// <summary>
        /// The method that will be passed through the delegates to all of the triggers.
        /// It will be invoked whenever the triggers are triggered.
        /// The id of the respective trigger will be passed.
        /// </summary>
        protected virtual void TriggerPuzzle(int id) {

            if (IsSolved)
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