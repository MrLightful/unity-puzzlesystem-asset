using System;
using UnityEngine;

namespace PuzzleSystem
{
    [AddComponentMenu("PuzzleSystem/Collider Based Triggers/On Key Trigger")]
    [HelpURL("https://puzzlesystem.gitbook.io/project/manual/triggers#onkeyeventpuzzletrigger")]
    public class OnKeyEventInColliderTrigger : CoreColliderBasedPuzzleTrigger
    {

        #region Variables

        private enum KeyboardEvent
        {
            GetKeyUp,
            GetKeyDown
        }

        [Space(20)]

        [SerializeField]
        [Tooltip("The triggering will be performed by this key on the keyboard if the object is within the Collider.")]
        private KeyCode key = KeyCode.E;

        [SerializeField]
        private KeyboardEvent keyEvent = KeyboardEvent.GetKeyDown;

        private bool isTriggered = false;

        #endregion


        private void OnTriggerStay(Collider other)
        {
            // Making sure that the tag of the entered object is specified in the array of tags.
            if (tags.Length > 0 && Array.IndexOf(tags, other.tag) == -1 && !isTriggered)
                return;

            switch (keyEvent) 
            {

                case KeyboardEvent.GetKeyUp:
                    GetKeyUpCheck();
                    break;

                case KeyboardEvent.GetKeyDown:
                    GetKeyDownCheck();
                    break;

                default:
                    return;
            }
        }


        #region Keyboard Type Implementations

        private void GetKeyUpCheck()
        {
            if (Input.GetKeyUp(key))
                TriggerImpl();
        }

        private void GetKeyDownCheck()
        {
            if (Input.GetKeyDown(key))
                TriggerImpl();
        }

        #endregion


    }

}
