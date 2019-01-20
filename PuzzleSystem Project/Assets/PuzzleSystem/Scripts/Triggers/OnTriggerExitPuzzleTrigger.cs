using System;
using UnityEngine;

namespace PuzzleSystem
{
    [AddComponentMenu("PuzzleSystem/Collider Based Triggers/On Exit")]
    /// <summary>
    /// The class defines the puzzle trigger based on OnTriggerExit (Collider) event.
    /// </summary>
    public class OnTriggerExitPuzzleTrigger : CoreColliderBasedPuzzleTrigger
    {

        // The method triggers the puzzle step iff the type of the trigger is 'OnTriggerExit'. Involves Collider and RigidBody.
        protected void OnTriggerExit(Collider other)
        {
            // Making sure that the tag of the entered object is specified in the array of tags.
            if (tags.Length > 0 && Array.IndexOf(tags, other.tag) == -1)
                return;

            TriggerImpl();
        }

    }

}