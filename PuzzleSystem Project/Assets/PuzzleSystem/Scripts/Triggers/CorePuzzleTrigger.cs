using UnityEngine;
using UnityEngine.Events;

namespace PuzzleSystem
{
    [AddComponentMenu("PuzzleSystem/Core/Core Trigger")]
    [HelpURL("https://puzzlesystem.gitbook.io/project/manual/core-elements/corepuzzletrigger-component")]
    /// <summary>
    /// The class contains basic puzzle trigger functionality.
    /// The entity might be triggered only from the code by calling the 'void Trigger()' method.
    /// Other, more complex, puzzle triggers must inherit from it.
    /// </summary>
    public class CorePuzzleTrigger : MonoBehaviour
    {

        #region Variables

        [Space(20)]
    
        [SerializeField]
        [Tooltip("Is it allowed to trigger an entity multiple times?")]
        /// <summary>
        /// Is it allowed to trigger an entity multiple times?
        /// </summary>
        protected bool allowMultipleTriggerings;

        [SerializeField]
        [Tooltip("If the puzzle is solved/failed, and the trigger is inactive, allow its activation?")]
        protected bool allowTriggeringAfterHandled;

        [Space(20)]

        [SerializeField]
        [Tooltip("The events invoked when this trigger is triggered.")]
        /// <summary>
        /// The events invoked when this trigger is triggered.
        /// </summary>
        protected UnityEvent onTriggered;

        [SerializeField]
        [Tooltip("The events invoked when the triggering is diactivated.")]
        /// <summary>
        /// The events invoked when the triggering is diactivated.
        /// </summary>
        protected UnityEvent onUnTriggered;

        /// <summary>
        /// Delegates for the triggered actions in the logic script.
        /// </summary>
        public delegate void OnPuzzleTriggerDelegate(int triggerID);
        public event OnPuzzleTriggerDelegate onPuzzleTriggerEvent;

        private CorePuzzleLogic logic;

        /// <summary>
        /// Debug the events into console? Inherits its state from the puzzle handler.
        /// </summary>
        protected bool debug = false;


        /// <summary>
        /// Determines whether or not this trigger is triggered at the moment.
        /// </summary>
        public bool isTriggering 
        {
            get;
            protected set;
        }

        /// <summary>
        /// Trigger ID. It is needed for the track of order.
        /// </summary>
        protected int id = -1;

        #endregion


        #region Public Methods

        /// <summary>
        /// Initilalize the trigger with the identificator and the debug state.
        /// </summary>
        /// <param name="id">Identifier of the trigger</param>
        /// <param name="debug">If set to <c>true</c>, the system will debug the events and actions into console. Preferebly keep in synch with the puzzle handler.</param>
        public virtual void Init(CorePuzzleLogic logic, int id, bool debug) 
        {
            this.logic = logic;
            this.id = id;
            this.debug = debug;
        }

        /// <summary>
        /// The method that triggers the puzzle step. Active iff the type of the trigger is 'OnMessage'.
        /// </summary>
        public virtual void Trigger()
        {
            TriggerImpl();
        }

        /// <summary>
        /// Deactivates the triggering state manually.
        /// </summary>
        public virtual void UnTrigger() 
        {
            isTriggering = false;
            onUnTriggered.Invoke();
        }

        #endregion


        #region Protected Virtual Methods

        /// <summary>
        /// Debug the triggered events to the console.
        /// </summary>
        protected virtual void DebugTriggered()
        {
            Debug.Log("[PuzzleSystem] The trigger (id: " + id + ", name: " + this.name + ") was triggered as '" + this.GetType() + "'.");
        }

        /// <summary>
        /// Trigger implementation for calling the dedicated delegates.
        /// </summary>
        protected virtual void TriggerImpl()
        {
            if ( (!allowMultipleTriggerings && isTriggering) || (!allowTriggeringAfterHandled && (logic.IsFailed || logic.IsSolved)) )
                return;

            if (debug)
                DebugTriggered();

            isTriggering = true;

            onTriggered.Invoke();

            onPuzzleTriggerEvent?.Invoke(id);
        }

        #endregion

    }

}