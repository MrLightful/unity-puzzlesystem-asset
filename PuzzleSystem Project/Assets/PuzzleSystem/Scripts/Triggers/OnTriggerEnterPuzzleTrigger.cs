using System;
using UnityEngine;

namespace PuzzleSystem
{
    [AddComponentMenu("PuzzleSystem/Collider Based Triggers/On Enter")]
    /// <summary>
    /// The class defines the puzzle trigger based on OnTriggerEnter (Collider) event.
    /// </summary>
    public class OnTriggerEnterPuzzleTrigger : CoreColliderBasedPuzzleTrigger
    {

        // The method triggers the puzzle step iff the type of the trigger is 'OnTriggerEnter'. Involves Collider and RigidBody.
        protected void OnTriggerEnter(Collider other)
        {

            // Making sure that the tag of the entered object is specified in the array of tags.
            if (tags.Length > 0 && Array.IndexOf(tags, other.tag) == -1)
                return;

            TriggerImpl();


        }

    }

}