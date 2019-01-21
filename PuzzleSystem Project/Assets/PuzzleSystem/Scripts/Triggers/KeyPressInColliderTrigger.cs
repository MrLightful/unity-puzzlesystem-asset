using System;
using UnityEngine;

namespace PuzzleSystem
{
    [AddComponentMenu("PuzzleSystem/Collider Based Triggers/Key Press Trigger")]
    [HelpURL("https://puzzlesystem.gitbook.io/project/~/drafts/-LWlSLhxdE-k6aU8t8q3/primary/manual/triggers#keypressincollidertrigger")]
    public class KeyPressInColliderTrigger : CoreColliderBasedPuzzleTrigger
    {

        #region Variables

        private enum KeyboardTriggerType
        {
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
