using UnityEngine;

namespace PuzzleSystem
{
    [AddComponentMenu("PuzzleSystem/Standard Logics/Simult Logic")]
    [HelpURL("https://puzzlesystem.gitbook.io/project/manual/standard-logics/simultpuzzlelogic")]
    /// <summary>
    /// The class contains logic for a specific type of puzzles.
    /// In order to complete the puzzle, all triggers must be active at the same time.
    /// </summary>
    public class SimultPuzzleLogic : CorePuzzleLogic
    {

        // The method that will be passed through the delegates to all of the triggers.
        // It will be invoked whenever the triggers are triggered.
        // The id of the respective trigger will be passed.
        protected override void TriggerPuzzle(int id)
        {
            if (IsSolved && !autoResetSolution)
                return;

            completedSteps = 0;

            foreach(var trigger in triggers) 
            {
                if (trigger.isTriggering)
                    completedSteps++;
            }

            // If the count of completed steps reached length of the triggers array,
            // it means that all triggers are active.
            // Thus, the puzzle is solved.
            if (completedSteps == triggers.Length)
            {
                AcceptSolution();
            }
        }


    }

}