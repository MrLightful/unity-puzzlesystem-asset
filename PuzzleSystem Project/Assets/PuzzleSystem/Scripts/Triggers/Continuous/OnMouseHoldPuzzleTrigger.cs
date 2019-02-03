using UnityEngine;

namespace PuzzleSystem
{
    [AddComponentMenu("PuzzleSystem/Collider Based Triggers/Continuous/On Mouse Hold Trigger")]
    [HelpURL("https://puzzlesystem.gitbook.io/project/manual/triggers#ontriggerstaypuzzletrigger")]
    public class OnMouseHoldPuzzleTrigger : ActivationDelayedPuzzleTriggers
    {

        enum MouseContinuousEvent { 
        
            Click,
            Positional

        }

        [SerializeField]
        private MouseContinuousEvent mouseEvent = MouseContinuousEvent.Click;


        #region Event Listeners

        private void OnMouseEnter()
        {
            if (mouseEvent == MouseContinuousEvent.Positional) PerformStart();
        }


        private void OnMouseExit()
        {
            if (mouseEvent == MouseContinuousEvent.Positional) PerformEnd();

        }

        private void OnMouseDown()
        {
            if (mouseEvent == MouseContinuousEvent.Click) PerformStart();

        }

        private void OnMouseUp()
        {
            if (mouseEvent == MouseContinuousEvent.Click) PerformEnd();

        }

        #endregion


        #region Handlers

        private void PerformStart() {

            if (!isTriggering)
                StartCoroutine(ProceedDelay());

        }

        private void PerformEnd() {

            if (isTriggering && activationDelay == 0)
            {
                UnTrigger();


                if (debug)
                    Debug.Log("[PuzzleSystem] The trigger (id: " + id + ") is not triggered anymore as 'OnMouseHold'.");

            }

            if (!isTriggering && activationDelay > 0)
            {
                StopAllCoroutines();

                if (debug)
                    Debug.Log("[PuzzleSystem] The trigger (id: " + id + ") is not triggered as 'OnMouseHold', because the activation delay was not reached.");
            }

        }

        #endregion

    }

}

