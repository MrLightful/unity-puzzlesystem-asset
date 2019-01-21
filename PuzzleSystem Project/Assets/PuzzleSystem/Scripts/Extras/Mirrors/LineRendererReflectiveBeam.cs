using System;
using UnityEngine;
using System.Collections.Generic;


namespace PuzzleSystem.Extras
{

    /// <summary>
    /// This class extends the line of the LineRenderer component to the specified maxDistance. 
    /// If there is an obstacle, the LineRenderer will be abrupt there.
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class LineRendererReflectiveBeam : MonoBehaviour
    {

        // TODO: 
        // - Abrupt the Line Renderer by an obstacle with the corresponding alpha and not with the fully fading one.


        #region Variables

        [SerializeField]
        [Tooltip("The maximum distance for the beam to be rendered after last reflection.")]
        private float maxDistance = 50;

        [SerializeField]
        [Tooltip("The minimum angle (in degrees) for the beam to be reflected.")]
        private float minAngle = 10f;

        [SerializeField]
        [Tooltip("The maximum possible amount of reflections that the beam can go through.")]
        private int maxBeamReflections = 5;

        [SerializeField]
        [Tooltip("The tags that will represent the reflection surfaces for the beam.")]
        private string[] reflectorTags = { "Puzzles/BeamReflector" };

        /// <summary>
        /// The list of objects that the beam crossed/hit/reflected from.
        /// </summary>
        public List<GameObject> HitObjects
        {
            get;
            private set;
        }


        //private Gradient initialGradient;
        private LineRenderer lr;

        #endregion


        #region Methods


        private void Start()
        {
            lr = GetComponent<LineRenderer>();

            // List of hit objects
            HitObjects = new List<GameObject>();
        }

        private void Update()
        {
            // Raycast vars
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);

            // Reset the list of hit objects
            HitObjects.Clear();

            // Should there be another iteration of the raycasting?
            bool continueReflect = false;

            // Reset the count of the relfection iterations
            int curReflection = 0;

            // Reset the count of the position points on the line
            // (2 by default: origin, end)
            lr.positionCount = 2;

            // Raycast the beam atleast once,
            // then repeat the raycasting 
            // if needed  (beam met reflection surface & max count of the reflections isn't exceeded)
            do
            {
                // If the cast hits anything and the amount of reflections didn't exceed maximum
                if (Physics.Raycast(ray, out hit, maxDistance) && lr.positionCount - 2 <= maxBeamReflections)
                {
                    // If there is a collider on the hit
                    if (hit.collider)
                    {
                        // Track the hit object
                        HitObjects.Add(hit.collider.gameObject);

                        // Retrieve the hit point relatively to the local to the LineRenderer coordinate system
                        Vector3 hitLocalPoint = transform.InverseTransformPoint(hit.point);

                        // Set the position of the last point in the LineRenderer system 
                        // to the point of the hit object
                        lr.SetPosition(lr.positionCount - 1, hitLocalPoint);

                        // If the tag of a hit object corresponds to the tags specified for the reflector objects,
                        // then reflect the beam
                        if (reflectorTags.Length > 0 && Array.IndexOf(reflectorTags, hit.collider.tag) >= 0)
                        {

                            // If the beam met reflection surface,
                            // repeat the raycasting once more
                            continueReflect = true;
                            curReflection += 1;

                            // Adjust the amount of position points for the beam
                            if (lr.positionCount - 2 < curReflection)
                                lr.positionCount++;

                            // Reflected direction for the reflected beam
                            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);

                            // Abrupt the beam refleaction if the angle is too small.
                            float angl = 180 - Vector3.Angle(ray.direction, reflectDir);
                            if (angl < minAngle)
                            {
                                continueReflect = false;

                            }
                            else ray = new Ray(hit.point, reflectDir); // Otherwise, setup ray for the next iteration

                        }
                        else
                        {
                            // If the tag of the hit object isn't a reflector,
                            // abrupt line here
                            continueReflect = false;
                        }

                    }

                }
                else
                {

                    // If the are no obstacles on the beam's way,
                    // render the maximum possible length of it
                    // and don't reflect it
                    lr.SetPosition(lr.positionCount - 1, transform.InverseTransformPoint(ray.origin + ray.direction * maxDistance));
                    continueReflect = false;
                }


            } while (continueReflect);

        }


        #endregion

    }

}