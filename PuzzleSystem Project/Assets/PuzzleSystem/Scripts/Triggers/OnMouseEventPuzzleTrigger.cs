using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleSystem
{

    [AddComponentMenu("PuzzleSystem/Collider Based Triggers/On Mouse Trigger")]
    [HelpURL("https://puzzlesystem.gitbook.io/project/manual/triggers#onmouseeventpuzzletrigger")]
    public class OnMouseEventPuzzleTrigger : CoreColliderBasedPuzzleTrigger
    {

        enum MouseEvent { 
        
            OnMouseUp,
            OnMouseDown,

            OnMouseEnter,
            OnMouseExit,

            OnMouseUpAsButton,

        }

        [SerializeField]
        private MouseEvent mouseEvent = MouseEvent.OnMouseDown;


        private void OnMouseUp()
        {
            if (mouseEvent == MouseEvent.OnMouseUp) TriggerImpl();
        }

        private void OnMouseDown()
        {
            if(mouseEvent == MouseEvent.OnMouseDown) TriggerImpl();
        }

        private void OnMouseEnter()
        {
            if (mouseEvent == MouseEvent.OnMouseEnter) TriggerImpl();

        }

        private void OnMouseExit()
        {
            if (mouseEvent == MouseEvent.OnMouseExit) TriggerImpl();

        }


        private void OnMouseUpAsButton()
        {
            if (mouseEvent == MouseEvent.OnMouseUpAsButton) TriggerImpl();
        }

    }

}
