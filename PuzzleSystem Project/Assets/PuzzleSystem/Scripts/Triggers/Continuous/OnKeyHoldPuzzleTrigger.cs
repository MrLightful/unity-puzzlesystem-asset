using System;
using UnityEngine;

namespace PuzzleSystem
{
    [AddComponentMenu("PuzzleSystem/Collider Based Triggers/Continuous/On Key Hold Trigger")]
    [HelpURL("https://puzzlesystem.gitbook.io/project/manual/triggers/continuous-triggers#onkeyholdpuzzletrigger")]
    public class OnKeyHoldPuzzleTrigger : ActivationDelayedPuzzleTriggers
    {

        #region Variables

        [SerializeField]
        [Tooltip("The triggering will be performed by this key on the keyboard if the object is within the Collider.")]
        private KeyCode key = KeyCode.E;

        private bool isWithinCollider = false;

        #endregion


        #region Triggers Logic

        private void Update()
        {
            if (!isWithinCollider) 
                return;

            if (Input.GetKeyDown(key))
            {
                StartCoroutine(ProceedDelay());
            }
            else if(Input.GetKeyUp(key))
            {
                if(isTriggering && activationDelay == 0) {
                    isTriggering = false;


                    if (debug)
                        Debug.Log("[PuzzleSystem] The trigger (id: " + id + ") is not triggered anymore as 'OnKeyHold'.");
                }

                if (!isTriggering && activationDelay > 0) {
                    StopAllCoroutines();

                    if (debug)
                        Debug.Log("[PuzzleSystem] The trigger (id: " + id + ") is not triggered as 'OnKeyHold', because the activation delay was not reached.");
                }
            }

        }


        #endregion


        #region Collider-Trigger Behaviour

        private void OnTriggerEnter(Collider other)
        {
            // Making sure that the tag of the entered object is specified in the array of tags.
            if (tags.Length > 0 && Array.IndexOf(tags, other.tag) == -1 && !isTriggering)
                return;

            isWithinCollider = true;
        }

        private void OnTriggerExit(Collider other)
        {
            // Making sure that the tag of the entered object is specified in the array of tags.
            if (tags.Length > 0 && Array.IndexOf(tags, other.tag) == -1 && !isTriggering)
                return;

            isWithinCollider = false;



            // Make sure no bugs appear if the object leaves the zone holding the key

            // Abrupt all coroutines if the activation delay is greater than 0,
            if (activationDelay > 0) StopAllCoroutines();

            // Untrigger the trigger if the actiovation delay is 0,
            else if (isTriggering) UnTrigger();

        }

        #endregion


    }

}
