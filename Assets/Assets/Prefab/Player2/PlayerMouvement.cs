using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    [Header("Horizontal Movement")]
    [SerializeField] private float PlayerSpeed;
    private float horizontal;
    
    [Header("Jump")]
    [SerializeField] private float PlayerJumpPower;
    [SerializeField] private float MaxPlayerJumpPower;
    
    [Header("Wall Interaction")]
    [SerializeField] private float PlayerWallSlidingSpeed;
    [SerializeField] private float wallJumpingTimer = 0.2f;
    [SerializeField] private float wallJumpingDuration = 0.4f;
    [SerializeField] private Vector2 wallJumpingPower = new Vector2(8f, 10f);
    
    private bool IsWallSliding;
    private bool IsWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingCounter;

    [Header("Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] private SASManager _sasManager;
    
    
    
    [Header("Sprite Renderer")]
    private bool IsFacingRight = true;
    
    
    
    [Header("Raycast")]
    //Empty in game pour check si le joueur est au sol ou sur un mur
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;


    public void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (_sasManager.playerHasCoal)
        {
            _animator.SetBool("PlayerHasCoal", true);
        }
        else
        {
            _animator.SetBool("PlayerHasCoal",false);
        }
        
        //Permet de sauter quand le joueur appuie sur la touche saut et qu'il est au sol
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, PlayerJumpPower);
            if (_sasManager.playerHasCoal)
            {
                _animator.SetTrigger("Jump");
            }
            else
            {
                _animator.SetTrigger("Jump");
            }
        }

        //Modifie la hauteur du saut en fonction du temps que le joueur appuie sur la touche saut
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * MaxPlayerJumpPower);
            if (_sasManager.playerHasCoal)
            {
                _animator.SetTrigger("Jump");
            }
            else
            {
                _animator.SetTrigger("Jump");
            }
        }

        WallSliding();
        WallJump();

        //Permet de changer la direction du joueur si il ne walljump pas
        if(!IsWallJumping)
        {
            FlipPlayer();
        }  
    }

    private void FixedUpdate()
    {
        //Déplacer le joueur en fonction de la touche appuyée (Q ou D || fléche directionnel)
        if (!IsWallJumping || IsWalled())
        {
            rb.velocity = new Vector2(horizontal * PlayerSpeed, rb.velocity.y);
            if (horizontal != 0)
            {
                if (_sasManager.playerHasCoal)
                {
                    _animator.SetBool("WalkCoal",true);
                }
                else
                {
                    _animator.SetBool("Walk",true);

                }
            }
            else
            {
                if (_sasManager.playerHasCoal)
                {
                    _animator.SetBool("WalkCoal",false);
                }
                else
                {
                    _animator.SetBool("Walk",false);

                }
            }
        }
    }

    //Permet de savoir si le joueur est au sol
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }   

    //Permet de savoir si le joueur est contre un mur
    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.3f, wallLayer);
    }

    //Gére le wallslide
    private void WallSliding()
    {
        if(IsWalled() && !IsGrounded())
        {
            IsWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -PlayerWallSlidingSpeed, float.MaxValue));
        }
        else
        {
            IsWallSliding = false;
        } 
        WallSlideAnimation(IsWallSliding);
    }

    private void WallSlideAnimation(bool WallSliding)
    {
        if (WallSliding)
        {
            if (_sasManager.playerHasCoal)
            {
                _animator.SetBool("WallSlideCoal",true);
            }
            else
            {
                _animator.SetBool("WallSlide",true);

            }
        }
        else
        {
            if (_sasManager.playerHasCoal)
            {
                _animator.SetBool("WallSlideCoal",false);
            }
            else
            {
                _animator.SetBool("WallSlide",false);

            }
        }
    }
    
    //Gére le walljump
    private void WallJump()
    {
        if (IsWallSliding)
        {
            IsWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTimer;
            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }
        if(Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            IsWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;
            
            if(transform.localScale.x != wallJumpingDirection)
            {
                IsFacingRight = !IsFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }   
    }

    //Permet de stopper le walljump
    private void StopWallJumping()
    {
        IsWallJumping = false;
    }

    //Permet de retourner le joueur en fonction de la direction
    private void FlipPlayer()
    {
        if(IsFacingRight && horizontal < 0 || !IsFacingRight && horizontal > 0)
        {
            IsFacingRight = !IsFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}

