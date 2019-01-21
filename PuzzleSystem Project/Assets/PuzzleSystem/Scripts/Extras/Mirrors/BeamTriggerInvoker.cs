using System.Collections.Generic;
using System.Linq;
using PuzzleSystem;
using UnityEngine;

namespace PuzzleSystem.Extras
{

    /// <summary>
    /// The additional component to the LineRendererReflectiveBeam (LRRB). 
    /// It serves as a bridge to PuzzleSystem.
    /// It takes the hits from the beam from LRRB component and invokes all puzzle triggers.
    /// </summary>
    [RequireComponent(typeof(LineRendererReflectiveBeam))]
    public class BeamTriggerInvoker : MonoBehaviour
    {

        private List<CorePuzzleTrigger> trackedTriggers = new List<CorePuzzleTrigger>();
        private LineRendererReflectiveBeam lrb;

        private void Start()
        {
            lrb = GetComponent<LineRendererReflectiveBeam>();
        }

        // TODO: 
        // - Quite heavy computing in Update. The optimization is needed here.
        private void Update()
        {
            List<CorePuzzleTrigger> copy = new List<CorePuzzleTrigger>(trackedTriggers);
            List<GameObject> hitObjects = lrb.HitObjects;

            // The beam might touch the same mirror multiple times.
            // No need to trigger it multiple times.
            // Thus, we need the list of distinct elements.
            hitObjects = hitObjects.Distinct().ToList();

            foreach (var obj in hitObjects)
            {

                CorePuzzleTrigger trigger = obj.GetComponent<CorePuzzleTrigger>();

                if (trigger != null)
                {

                    int index = copy.IndexOf(trigger);

                    if (index < 0)
                    {
                        trackedTriggers.Add(trigger);
                        trigger.Trigger();
                    }
                    else
                    {
                        copy.RemoveAt(index);
                    }

                }


            }

            foreach (var trigger in copy)
            {
                trackedTriggers.Remove(trigger);
                trigger.UnTrigger();
            }
        }
    }

}