using UnityEngine;

namespace SpelunkVania
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float speed = 5f;

        [SerializeField]
        private float jumpSpeed = 10f;

        [SerializeField]
        private float slideSpeed = -1;

        [SerializeField]
        private float wallCheckRadius;

        [SerializeField]
        private Transform groundChecker;

        [SerializeField]
        private float groundedRadius;

        [SerializeField]
        private LayerMask layer;

        [SerializeField]
        private GameObject jumpOffDust;

        private Rigidbody2D rb;

        private Animator anim;

        private Vector2 move;

        private bool grounded;

        private bool jump;

        private bool canDoubleJump;

        private bool doubleJump;

        private bool canJumpOffWall;

        private bool sliding;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            anim = GetComponent<Animator>();
        }

        void Update()
        {
            PressMoveButtons();

            PressJumpButton();

            Slide();
        }

        void FixedUpdate()
        {
            CheckGround();

            MovePlayer();

            JumpPlayer();

            JumpOrSlidePlayer();
        }

        private void PressMoveButtons()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");

            int direction = 0;

            if (horizontal != 0)
            {
                var scale = transform.localScale;

                direction = horizontal > 0 ? 1 : -1;

                scale.x = direction;

                transform.localScale = scale;

                anim.SetBool("run", true);
            }
            else
            {
                anim.SetBool("run", false);
            }

            move = new Vector2(direction * speed, rb.velocity.y);
        }

        private void PressJumpButton()
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (!jump && grounded)
                {
                    jump = true;
                    canDoubleJump = true;
                }
                else if (canDoubleJump && !grounded)
                {
                    doubleJump = true;
                    canDoubleJump = false;
                }
                else if (sliding)
                {
                    canJumpOffWall = true;
                    canDoubleJump = false;
                }
            }
        }

        private void Slide()
        {
            if (Physics2D.Raycast(transform.position, Vector2.right, wallCheckRadius, layer) ||
                Physics2D.Raycast(transform.position, Vector2.left, wallCheckRadius, layer))
            {
                if (rb.velocity.y <= -1)
                {
                    var scale = transform.localScale;

                    if (Physics2D.Raycast(transform.position, Vector2.right, wallCheckRadius, layer))
                    {
                        scale.x = -1;

                        transform.localScale = scale;
                    }
                    else if (Physics2D.Raycast(transform.position, Vector2.left, wallCheckRadius, layer))
                    {
                        scale.x = 1;

                        transform.localScale = scale;
                    }

                    sliding = true;
                    canDoubleJump = false;
                }
                else
                {
                    sliding = false;
                }


            }
            else
            {
                sliding = false;
            }
        }

        private void CheckGround()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundChecker.position, groundedRadius);

            grounded = false;

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    grounded = true;
                    break;
                }
            }
        }

        private void MovePlayer()
        {
            rb.velocity = new Vector2(move.x, rb.velocity.y);
        }

        private void JumpPlayer()
        {
            if (jump || doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                grounded = false;
                jump = false;
                doubleJump = false;

            }
            else if (canJumpOffWall)
            {
                var scale = transform.localScale;

                var x = scale.x == -1 ? -10f : 10f;

                Instantiate(jumpOffDust, transform.position + new Vector3(scale.x == 1 ? 0.2f : -0.2f, 0, -1), Quaternion.identity);

                rb.velocity = new Vector2(x, jumpSpeed);
                grounded = false;
                sliding = false;
                canJumpOffWall = false;
            }
        }

        private void JumpOrSlidePlayer()
        {
            if (!grounded)
            {
                if (sliding)
                {
                    rb.velocity = new Vector2(rb.velocity.x, slideSpeed);

                    anim.SetFloat("jump", 1);
                }
                else
                {
                    if (rb.velocity.y > 0)
                    {
                        anim.SetFloat("jump", 2);
                    }
                    else if (rb.velocity.y < 0)
                    {
                        anim.SetFloat("jump", 0);
                    }
                }


            }
            else
            {
                sliding = false;

                anim.SetFloat("jump", -1);
            }
        }
    }
}