using System;
using UnityEngine;

namespace PuzzleSystem
{
    [AddComponentMenu("PuzzleSystem/Collider Based Triggers/On Trigger Event")]
    [HelpURL("https://puzzlesystem.gitbook.io/project/manual/triggers#onmouseeventpuzzletrigger")]
    /// <summary>
    /// The class defines the puzzle trigger based on OnTriggerEnter (Collider) event.
    /// </summary>
    public class OnTriggerEventPuzzleTrigger : CoreColliderBasedPuzzleTrigger
    {

        enum TriggerEvent { 
        
            OnTriggerEnter,
            OnTriggerExit
        
        }

        [SerializeField]
        private TriggerEvent triggerEvent = TriggerEvent.OnTriggerEnter;

        private void OnTriggerEnter(Collider other)
        {
            if (triggerEvent != TriggerEvent.OnTriggerEnter) return;


            // Making sure that the tag of the entered object is specified in the array of tags.
            if (tags.Length > 0 && Array.IndexOf(tags, other.tag) == -1)
                return;


            TriggerImpl();


        }

        private void OnTriggerExit(Collider other)
        {
            if (triggerEvent != TriggerEvent.OnTriggerExit) return;


            // Making sure that the tag of the entered object is specified in the array of tags.
            if (tags.Length > 0 && Array.IndexOf(tags, other.tag) == -1)
                return;


            TriggerImpl();


        }

    }

}