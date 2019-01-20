using UnityEngine;

namespace PuzzleSystem
{
    /// <summary>
    /// The class defines basics for the Collider Based triggers.
    /// It is abstract, thus cannot be instantiated into the application.
    /// Serves only as a parent class for explicit Collider based events.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public abstract class CoreColliderBasedPuzzleTrigger : CorePuzzleTrigger
    {

        [SerializeField]
        [Tooltip("The tag of the object that will be able to trigger this trigger. Matters only if the type involves the collider functionality.")]
        /// <summary>
        /// The tag of the object that will be able to trigger this trigger. Matters only if the type involves the collider functionality.
        /// </summary>
        protected string[] tags = { "Player" };

    }

}