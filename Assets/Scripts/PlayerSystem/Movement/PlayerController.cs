using PlayerSystem.Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerSystem.Movement
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Ground Check")]
        public float groundDistance = 0.4f;
        private bool isGrounded;

        [Header("Movement Settings")]
        public float maxSpeed = 5f;
        public float sprintMultiplier = 1.6f;
        public float drag = 6f;

        [Header("Mouse Settings")]
        public float mouseSensitivity = 100f;
        public Transform playerCamera;

        [Header("Jump Settings")]
        public float jumpForce = 7f;
        private bool jumpRequested = false;

        [Header("Footstep Audio")]
        public AudioSource footstepSource;
        public AudioClip[] footstepClips;
        public float stepInterval = 0.5f;
        private float stepTimer = 0f;

        private Rigidbody rb;
        private float xRotation;

        [Space(10)]

        [SerializeField]
        private PlayerInventoryController inventoryController;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            // Prevent player from tipping over
            rb.freezeRotation = true;
            // Locks mouse
            isGrounded = true;
        }

        private void Update()
        {
            // Ground Check
            isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance);
            Debug.DrawLine(transform.position, transform.position + Vector3.down * groundDistance, Color.darkRed);

            if (!inventoryController.inInventory)
            {
                // Mouse Look
                CameraFunctions();

                // Jump Input
                if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                    jumpRequested = true;

                // Footsteps
                Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
                bool isMoving = horizontalVelocity.magnitude > 0.2f && isGrounded;

                float currentStepInterval = Input.GetKey(KeyCode.LeftShift) ? stepInterval * 0.6f : stepInterval;

                if (isMoving)
                {
                    stepTimer += Time.deltaTime;
                    if (stepTimer >= currentStepInterval)
                    {
                        PlayFootstep();
                        stepTimer = 0f;
                    }
                }
                else
                {
                    stepTimer = 0f;
                }
            }
        }

        private void FixedUpdate()
        {
            if (!inventoryController.inInventory)
            {
                // Input
                float moveX = Input.GetAxisRaw("Horizontal");
                float moveZ = Input.GetAxisRaw("Vertical");

                Vector3 moveDir = (transform.right * moveX + transform.forward * moveZ).normalized;

                bool isSprinting = Input.GetKey(KeyCode.LeftShift) && isGrounded;
                float currentMaxSpeed = isSprinting ? maxSpeed * sprintMultiplier : maxSpeed;

                // Calculate target velocity
                Vector3 targetVelocity = moveDir * currentMaxSpeed;
                Vector3 currentVelocity = rb.linearVelocity;

                // Keep vertical velocity untouched
                Vector3 velocityChange = (targetVelocity - new Vector3(currentVelocity.x, 0, currentVelocity.z));

                // Apply snappy movement by directly setting velocity
                rb.linearVelocity = new Vector3(targetVelocity.x, currentVelocity.y, targetVelocity.z);

                rb.linearDamping = isGrounded ? drag : 0f;

                // Jumping
                if (jumpRequested)
                {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    jumpRequested = false;
                }
            }
        }

        void PlayFootstep()
        {
            if (footstepClips.Length == 0 || footstepSource == null)
                return;

            int randomIndex = Random.Range(0, footstepClips.Length);
            footstepSource.clip = footstepClips[randomIndex];
            footstepSource.Play();
        }

        public void CameraFunctions()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity /* * Time.deltaTime */;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity /* * Time.deltaTime */;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }
    }
}