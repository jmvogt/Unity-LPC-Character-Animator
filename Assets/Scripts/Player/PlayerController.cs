using Assets.Scripts.Animation;
using Assets.Scripts.Animation.ActionAnimations;
using Assets.Scripts.Animation.Interfaces;
using Assets.Scripts.Character;
using Assets.Scripts.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private AnimationManager _animationManager;
    private AnimationRenderer _charAnimator;

    private BaseAction _newAction;
    private KeyCode _newInput;
    private KeyCode _lastInput;
    private string _lastDirection;

    private GameObject _playerObject;
    private float _moveSpeed;

    private bool _movingHorizontally;
    private bool _movingVertically;
    private bool _isAttacking;

    private Rigidbody2D _rigidbody;

	void Start () {
        //prepare character sprites
        _playerObject = GameObject.Find("/player");
        _charAnimator = gameObject.AddComponent<AnimationRenderer>() as AnimationRenderer;
        _rigidbody = GetComponent<Rigidbody2D>();
        InitializeCharacterRenderers(_charAnimator);

        //set defaults
        _animationManager = new AnimationManager();
        _newAction = new IdleAction();
        _lastDirection = DirectionType.DOWN;
        _moveSpeed = .0275f;
        _movingHorizontally = false;
        _movingVertically = false;
        _isAttacking = false;
        
	}

    void InitializeCharacterRenderers(AnimationRenderer charAnimator) {
        Dictionary<string, SpriteRenderer> spriteRenderers = new Dictionary<string, SpriteRenderer>();
        foreach(string blockKey in DNABlockType.GetTypeList()) {
            GameObject blockObject = new GameObject(blockKey);
            blockObject.transform.parent = _playerObject.transform;
            spriteRenderers[blockKey] = blockObject.AddComponent<SpriteRenderer>();
        }

        charAnimator.InitializeSpriteRenderers(spriteRenderers);
    }

    void Update() {
        UpdateAnimation();
        UpdatePositioning();
    }

    void UpdatePositioning() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            _charAnimator.UpdateAnimationTime(.3f);
            _moveSpeed = .0425f;
        } else {
            _charAnimator.UpdateAnimationTime(.7f);
            _moveSpeed = .0275f;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        Debug.Log("Force: " + movement);
        //_rigidbody.AddForce(movement * _moveSpeed);

        _rigidbody.MovePosition(_rigidbody.position + movement * _moveSpeed);
        
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        //    _movingHorizontally = false;
        //else if (moveHorizontal != 0 && !_isAttacking)
        //{
        //    _movingHorizontally = true;
        //    if (moveHorizontal > 0)
        //        gameObject.transform.position += new Vector3(1, 0, 0) * _moveSpeed;
        //    else if (moveHorizontal < 0)
        //        gameObject.transform.position += new Vector3(-1, 0, 0) * _moveSpeed;
        //}

        //float moveVertical = Input.GetAxis("Vertical");
        //if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        //    _movingVertically = false;
        //else if (moveVertical != 0 && !_isAttacking)
        //{
        //    _movingVertically = true;
        //    if (moveVertical > 0)
        //        gameObject.transform.position += new Vector3(0, 1, 0) * _moveSpeed;
        //    else if (moveVertical < 0)
        //        gameObject.transform.position += new Vector3(0, -1, 0) * _moveSpeed;
        //}
    }

    void UpdateAnimation() {
        string newDirection = DirectionType.NONE;
        if (Input.GetKeyDown(KeyCode.W)) {
            newDirection = DirectionType.UP;
            _newAction = new WalkAction();
            _newInput = KeyCode.W;
            _isAttacking = false;
        } else if (Input.GetKeyDown(KeyCode.A)) {
            newDirection = DirectionType.LEFT;
            _newAction = new WalkAction();
            _newInput = KeyCode.A;
            _isAttacking = false;
        } else if (Input.GetKeyDown(KeyCode.S)) {
            newDirection = DirectionType.DOWN;
            _newAction = new WalkAction();
            _newInput = KeyCode.S;
            _isAttacking = false;
        } else if (Input.GetKeyDown(KeyCode.D)) {
            newDirection = DirectionType.RIGHT;
            _newAction = new WalkAction();
            _newInput = KeyCode.D;
            _isAttacking = false;
        } else if (Input.GetKeyDown(KeyCode.Space)) {
            _newAction = new SlashAction();
            _newInput = KeyCode.Space;
            _isAttacking = true;
        } else if (Input.GetKeyDown(KeyCode.F)) {
            _newAction = new ThrustAction();
            _newInput = KeyCode.F;
            _isAttacking = true;
        } else if (Input.GetKeyDown(KeyCode.R)) {
            _newAction = new SpellcastAction();
            _newInput = KeyCode.R;
            _isAttacking = true;
        } else if (Input.GetKeyDown(KeyCode.E)) {
            _newAction = new ShootAction();
            _newInput = KeyCode.E;
            _isAttacking = true;
        } else {
            _newInput = KeyCode.None;
        }

        // continue using the last direction when the character stops moving
        if (newDirection == DirectionType.NONE)
            newDirection = _lastDirection;
         
        Debug.Log("Last Input: " + _lastInput);
        Debug.Log("New Input: " + _newInput);

        bool sameAction = _lastDirection == newDirection && _lastInput == _newInput;

        // if there is a new action or the characterDNA has changed, animate the current player state
        if (!sameAction || Player.characterDNA.IsDirty()) {
            _animationManager.UpdateDNAForAction(Player.characterDNA, Player.animationDNA, _newAction, newDirection);
            _charAnimator.AnimateAction(Player.animationDNA, _newAction);
        } else if (!Input.anyKey) {
            // if there is no movement, stop on the final frame of the current animation
            _charAnimator.StopOnFinalFrame(true);
        }

        _lastDirection = _newAction.Direction = newDirection;
        _lastInput = _newInput;
    }
}
