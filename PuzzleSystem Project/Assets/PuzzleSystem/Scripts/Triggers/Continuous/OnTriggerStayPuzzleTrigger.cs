using System;
using UnityEngine;


namespace PuzzleSystem
{
    [AddComponentMenu("PuzzleSystem/Collider Based Triggers/Continuous/On Stay Trigger")]
    [HelpURL("https://puzzlesystem.gitbook.io/project/manual/triggers/continuous-triggers#ontriggerstaypuzzletrigger")]
    /// <summary>
    /// The class defines the puzzle trigger based on OnTriggerStay (Collider) event.
    /// </summary>
    public class OnTriggerStayPuzzleTrigger : ActivationDelayedPuzzleTriggers
    {
    

        // The method triggers the puzzle step iff the type of the trigger is 'OnTriggerEnter'. Involves Collider and RigidBody.
        protected void OnTriggerEnter(Collider other)
        { 
            // Making sure that the tag of the entered object is specified in the array of tags.
            if (tags.Length > 0 && Array.IndexOf(tags, other.tag) == -1)
                return;

            StartCoroutine(ProceedDelay());

        }

        // The method triggers the puzzle step iff the type of the trigger is 'OnTriggerExit'. Involves Collider and RigidBody.
        protected void OnTriggerExit(Collider other)
        {
            if ( (isTriggering && activationDelay == 0) )
            {
                // Making sure that the tag of the entered object is specified in the array of tags.
                if (tags.Length > 0 && Array.IndexOf(tags, other.tag) == -1)
                    return;

                UnTrigger();

                if(debug)
                    Debug.Log("[PuzzleSystem] The trigger (id: " + id + ") is not triggered anymore as 'OnTriggerStay'.");

            }

            if (!isTriggering && activationDelay > 0)
            {
                if (debug)
                    Debug.Log("[PuzzleSystem] The trigger (id: " + id + ") is not triggered as 'OnTriggerStay', because the activation delay was not reached.");

                StopAllCoroutines();
            }

        }
    }
}
