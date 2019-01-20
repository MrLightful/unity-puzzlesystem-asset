using UnityEngine;

namespace PuzzleSystem.Demo
{

    public class Movement : MonoBehaviour
    {
        [SerializeField]
        private float speed = 10f;

        [SerializeField]
        private float jumpForce = 50f;

        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            transform.position += (Vector3.forward * moveVertical + Vector3.right * moveHorizontal) * speed * Time.deltaTime;

        }

        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                rb.AddForce(Vector3.up * jumpForce);
        }

    }

}