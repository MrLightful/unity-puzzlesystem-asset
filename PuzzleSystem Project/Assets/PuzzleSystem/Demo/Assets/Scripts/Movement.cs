using UnityEngine;

namespace PuzzleSystem.Demo
{

    public class Movement : MonoBehaviour
    {
        [SerializeField]
        private float speed = 10f;


        void Update()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            transform.position += (transform.forward * moveVertical + transform.right * moveHorizontal) * speed * Time.deltaTime;
        }


    }

}