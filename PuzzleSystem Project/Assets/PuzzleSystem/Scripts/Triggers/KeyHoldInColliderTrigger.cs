using System;
using System.Collections;
using UnityEngine;

namespace PuzzleSystem
{
    [AddComponentMenu("PuzzleSystem/Collider Based Triggers/Key Hold Trigger")]
    [HelpURL("https://puzzlesystem.gitbook.io/project/~/drafts/-LWlSLhxdE-k6aU8t8q3/primary/manual/triggers#keyholdincollidertrigger")]
    public class KeyHoldInColliderTrigger : CoreColliderBasedPuzzleTrigger
    {

        #region Variables

        [SerializeField]
        [Tooltip("The triggering will be performed by this key on the keyboard if the object is within the Collider.")]
        private KeyCode key = KeyCode.E;

        [SerializeField]
        [Tooltip("Do you want to make a delay for trigger activation?")]
        private float activationDelay = 0f;

        private bool isDelayDone = false;
        private bool isWithinCollider = false;
        private bool isDelaying = false;

        #endregion


        #region Triggers Logic

        private void Update()
        {
            if (!isWithinCollider) 
                return;


            if (Input.GetKey(key)) {

                if (activationDelay > 0 && !isDelaying)
                    StartCoroutine(ProceedDelay());

                else if(activationDelay == 0f)
                    TriggerImpl();
                

            }
            else if(activationDelay > 0 && !isDelayDone) 
            {
                if (isDelaying)
                {
                    isDelaying = false;
                    StopAllCoroutines();
                }

            } else if(activationDelay == 0 && isTriggering) {
                UnTrigger();
            }
        }

        public override void UnTrigger()
        {
            base.UnTrigger();

            isDelaying = false;
            isDelayDone = false;
        }

        private IEnumerator ProceedDelay() 
        {
            isDelaying = true;

            yield return new WaitForSeconds(activationDelay);

            isDelayDone = true;
            TriggerImpl();
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
        }

        #endregion


    }

}
