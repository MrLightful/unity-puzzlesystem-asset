using System;
using System.Collections;
using UnityEngine;


namespace PuzzleSystem
{
    [AddComponentMenu("PuzzleSystem/Collider Based Triggers/On Stay Trigger")]
    [HelpURL("https://puzzlesystem.gitbook.io/project/manual/triggers#ontriggerstaypuzzletrigger")]
    /// <summary>
    /// The class defines the puzzle trigger based on OnTriggerStay (Collider) event.
    /// </summary>
    public class OnTriggerStayPuzzleTrigger : CoreColliderBasedPuzzleTrigger
    {

        [SerializeField]
        [Tooltip("Do you want to make a delayed activation?\nNB! If the value is greater than 0, then the trigger won't be deactivated after the object leaves the collider.")]
        private float activationDelay = 0f;

        // The method triggers the puzzle step iff the type of the trigger is 'OnTriggerEnter'. Involves Collider and RigidBody.
        protected void OnTriggerEnter(Collider other)
        { 
            // Making sure that the tag of the entered object is specified in the array of tags.
            if (tags.Length > 0 && Array.IndexOf(tags, other.tag) == -1)
                return;

            StartCoroutine(ProceedDelay());

        }

        private IEnumerator ProceedDelay() 
        {
            yield return new WaitForSeconds(activationDelay);

            TriggerImpl();
        }


        // The method triggers the puzzle step iff the type of the trigger is 'OnTriggerExit'. Involves Collider and RigidBody.
        protected void OnTriggerExit(Collider other)
        {
            if ( (isTriggering && activationDelay == 0) )
            {
                // Making sure that the tag of the entered object is specified in the array of tags.
                if (tags.Length > 0 && Array.IndexOf(tags, other.tag) == -1)
                    return;

                if(debug)
                    Debug.Log("[PuzzleSystem] The trigger (id: " + id + ") is not triggered anymore as 'OnTriggerStay'.");

                isTriggering = false;
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
