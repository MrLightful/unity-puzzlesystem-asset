using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleSystem
{
    /// <summary>
    /// The superclass for the triggers that need the delayed activation.
    /// </summary>
    public abstract class ActivationDelayedPuzzleTriggers: CoreColliderBasedPuzzleTrigger
    {
    
        [SerializeField]
        [Tooltip("Do you want to make a delay for trigger activation?")]
        protected float activationDelay = 0f;

        protected IEnumerator ProceedDelay()
        {
            yield return new WaitForSeconds(activationDelay);

            TriggerImpl();
        }

    }

}