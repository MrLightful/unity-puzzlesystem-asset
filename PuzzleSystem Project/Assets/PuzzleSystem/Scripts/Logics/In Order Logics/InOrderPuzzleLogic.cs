using UnityEngine;

namespace PuzzleSystem
{
    [AddComponentMenu("PuzzleSystem/Standard Logics/In Order Logic")]
    [HelpURL("https://puzzlesystem.gitbook.io/project/manual/standard-logics/inorderpuzzlelogic")]
    /// <summary>
    /// The class contains logic for a specific type of puzzle.
    /// The triggers must be triggered in a specific order. 
    /// Otherwise, the puzzle fails and resets.
    /// The order might be either randomized, or specified by the order in the triggers array.
    /// </summary>
    public class InOrderPuzzleLogic : CorePuzzleLogic
    {

        #region Variables

        [Space(10)]

        [SerializeField]
        [Tooltip("Does the order of the sequence needs to be randomized at the start of the session (it won't be shuffled if the player fails)?\n\nUse 'void RandomizeOrder()' in order to invoke randomization manually runtime.\n\nIf the variable is false, the order will be specified by the order in the triggers array.")]
        private bool randomizeOrder = true;

        // The order in which the triggers must be triggered. Matters iff 'randomizeOrder' is true.
        private int[] order;


        public CorePuzzleTrigger[] GetObjectsOfOrder() 
        {
            if (!randomizeOrder)
                return triggers;


            CorePuzzleTrigger[] result = new CorePuzzleTrigger[triggers.Length];

            for (int i = 0; i < triggers.Length; i++)
            {
                result[i] = triggers[ order[i] ];
            }

            return result;
        }

        #endregion


        #region Methods

        private void Start()
        {
            if (randomizeOrder)
                RandomizeOrder();
        }

        /// <summary>
        /// Randomizes/Shuffles the order in which the sequence must be triggered.
        /// </summary>
        public void RandomizeOrder()
        {
            // Create an array of the length of the triggers
            order = new int[triggers.Length];

            // Initialize values
            for (int i = 0; i < order.Length; i++)
                order[i] = i;

            // Suffle them
            System.Random r = new System.Random();
            for (int i = order.Length; i > 0; i--)
            {
                int j = r.Next(i);
                int k = order[j];
                order[j] = order[i - 1];
                order[i - 1] = k;
            }
        }


        // The method that will be passed through the delegates to all of the triggers.
        // It will be invoked whenever the triggers are triggered.
        // The id of the respective trigger will be passed.
        protected override void TriggerPuzzle(int id)
        {
            if (IsSolved || IsFailed)
                return;

            bool success = false;

            // Do we use randomized order or the order specified by the default order in the triggers array?
            if (randomizeOrder)
            {

                // If the triggered id is the same as specified in the order array at the current step,
                // then step is successful.
                success = (order[completedSteps] == id);

            }
            else
            {
                // If the current step is the same as the triggered id,
                // then the step is successful.
                success = (id == completedSteps);

            }


            if (success)
                completedSteps++;
            else
            {
                if(autoResetFailure)
                    Reset();

                handler.Fail();
            }

            // If the count of completed steps reached length of the triggers array,
            // it means that all triggers have been triggered in correct order.
            // Thus, the puzzle is solved.
            if (completedSteps == triggers.Length)
            {
                AcceptSolution();
            }
        }

        #endregion

    }

}