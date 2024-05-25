using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PlayerController : Controller
    {
        static public PlayerController Player;
        public float speed;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            Player = this;
        }


        private void Update()
        {
            // Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector2 targetVelocity = Joystick._executor.Axes;
            Vector2 dir = targetVelocity;
            if (targetVelocity.x < 0)
            {
                //dir.x = -1;
                animator.SetInteger("Direction", 3);
            }
            else if (targetVelocity.x > 0)
            {
               // dir.x = 1;
                animator.SetInteger("Direction", 2);
            }
            if(Mathf.Abs(targetVelocity.y) > Mathf.Abs(targetVelocity.x)) 
            { 
                if (targetVelocity.y > 0)
                {
                    //dir.y = 1;
                    animator.SetInteger("Direction", 1);
                }
                else if (targetVelocity.y < 0)
                {
                    //dir.y = -1;
                    animator.SetInteger("Direction", 0);
                }
            }

            //dir.Normalize();
            animator.SetBool("IsMoving", dir.magnitude > 0);

            GetComponent<Rigidbody2D>().velocity = speed * dir;
        }
    }
