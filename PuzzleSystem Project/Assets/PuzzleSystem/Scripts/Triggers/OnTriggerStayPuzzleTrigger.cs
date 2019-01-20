using System;
using UnityEngine;


namespace PuzzleSystem
{
    [AddComponentMenu("PuzzleSystem/Collider Based Triggers/On Stay")]
    /// <summary>
    /// The class defines the puzzle trigger based on OnTriggerStay (Collider) event.
    /// </summary>
    public class OnTriggerStayPuzzleTrigger : CoreColliderBasedPuzzleTrigger
    {

        // The method triggers the puzzle step iff the type of the trigger is 'OnTriggerEnter'. Involves Collider and RigidBody.
        protected void OnTriggerEnter(Collider other)
        { 
            // Making sure that the tag of the entered object is specified in the array of tags.
            if (tags.Length > 0 && Array.IndexOf(tags, other.tag) == -1)
                return;

            TriggerImpl();

        }

        // The method triggers the puzzle step iff the type of the trigger is 'OnTriggerExit'. Involves Collider and RigidBody.
        protected void OnTriggerExit(Collider other)
        {
            if (isTriggering)
            {
                // Making sure that the tag of the entered object is specified in the array of tags.
                if (tags.Length > 0 && Array.IndexOf(tags, other.tag) == -1)
                    return;

                if(debug)
                    Debug.Log("[PuzzleSystem] The trigger (id: " + id + ") is not triggered anymore as 'OnTriggerStay'.");

                isTriggering = false;
            }
        }
    }
}
