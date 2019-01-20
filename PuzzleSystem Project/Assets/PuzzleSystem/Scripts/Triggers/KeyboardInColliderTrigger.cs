using System;
using UnityEngine;

namespace PuzzleSystem
{
    [AddComponentMenu("PuzzleSystem/Collider Based Triggers/On Keyboard's Key")]
    public class KeyboardInColliderTrigger : CoreColliderBasedPuzzleTrigger
    {

        #region Variables

        private enum KeyboardTriggerType
        {
            //GetKey,
            GetKeyUp,
            GetKeyDown
        }

        [Space(20)]

        [SerializeField]
        [Tooltip("The triggering will be performed by this key on the keyboard if the object is within the Collider.")]
        private KeyCode key = KeyCode.E;

        [SerializeField]
        private KeyboardTriggerType type = KeyboardTriggerType.GetKeyUp;

        private bool isTriggered = false;

        #endregion


        private void OnTriggerStay(Collider other)
        {
            // Making sure that the tag of the entered object is specified in the array of tags.
            if (tags.Length > 0 && Array.IndexOf(tags, other.tag) == -1 && !isTriggered)
                return;

            switch (type) 
            {

                //case KeyboardTriggerType.GetKey:
                    //GetKeyCheck();
                    //break;

                case KeyboardTriggerType.GetKeyUp:
                    GetKeyUpCheck();
                    break;

                case KeyboardTriggerType.GetKeyDown:
                    GetKeyDownCheck();
                    break;

                default:
                    return;
            }
        }


        #region Keyboard Type Implementations

        private void GetKeyCheck()
        {
            if (Input.GetKey(key))
                TriggerImpl();
        }

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
