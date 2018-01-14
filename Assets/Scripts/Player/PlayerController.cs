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
    
	void Start () {
        //prepare character sprites
        _playerObject = GameObject.Find("/player");
        _charAnimator = gameObject.AddComponent<AnimationRenderer>() as AnimationRenderer;
        InitializeCharacterRenderers(_charAnimator);

        //set defaults
        _animationManager = new AnimationManager();
        _newAction = new IdleAction();
        _lastDirection = DirectionType.DOWN;
        _moveSpeed = .0275f;
        _movingHorizontally = false;
        _movingVertically = false;
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
        UpdatePositioning();
        UpdateAnimation();
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
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            _movingHorizontally = false;
        else if (moveHorizontal != 0)
        {
            _movingHorizontally = true;
            if (moveHorizontal > 0)
                gameObject.transform.position += new Vector3(1, 0, 0) * _moveSpeed;
            else if (moveHorizontal < 0)
                gameObject.transform.position += new Vector3(-1, 0, 0) * _moveSpeed;
        }

        float moveVertical = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
            _movingVertically = false;
        else if (moveVertical != 0)
        {
            _movingVertically = true;
            if (moveVertical > 0)
                gameObject.transform.position += new Vector3(0, 1, 0) * _moveSpeed;
            else if (moveVertical < 0)
                gameObject.transform.position += new Vector3(0, -1, 0) * _moveSpeed;
        }
    }

    void UpdateAnimation() {
        string newDirection = DirectionType.NONE;
        _newInput = KeyCode.None;
        if (Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.W) && _lastInput != KeyCode.W) {
            newDirection = DirectionType.UP;
            _newAction = new WalkAction();
            _newInput = KeyCode.W;
        } else if (Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.A) && _lastInput != KeyCode.A) {
            newDirection = DirectionType.LEFT;
            _newAction = new WalkAction();
            _newInput = KeyCode.A;
        } else if (Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.S) && _lastInput != KeyCode.S) {
            newDirection = DirectionType.DOWN;
            _newAction = new WalkAction();
            _newInput = KeyCode.S;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.D) && _lastInput != KeyCode.D) {
            newDirection = DirectionType.RIGHT;
            _newAction = new WalkAction();
            _newInput = KeyCode.D;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            newDirection = _lastDirection;
            _newAction = new SlashAction();
            _newInput = KeyCode.Space;
        } else if (Input.GetKeyDown(KeyCode.F)) {
            newDirection = _lastDirection;
            _newAction = new ThrustAction();
            _newInput = KeyCode.F;
        } else if (Input.GetKeyDown(KeyCode.R)) {
            newDirection = _lastDirection;
            _newAction = new SpellcastAction();
            _newInput = KeyCode.R;
        } else if (Input.GetKeyDown(KeyCode.E)) {
            newDirection = _lastDirection;
            _newAction = new ShootAction();
            _newInput = KeyCode.E;
        }

        // continue using the last direction when the character stops moving
        if (newDirection == DirectionType.NONE)
            newDirection = _lastDirection;

        bool sameAction = _lastDirection == newDirection && _lastInput == _newInput;

        // if there is a new action or the characterDNA has changed, animate the current player state
        if (!sameAction || Player.characterDNA.IsDirty()) {
            _animationManager.UpdateDNAForAction(Player.characterDNA, Player.animationDNA, _newAction, newDirection);
            _charAnimator.AnimateAction(Player.animationDNA, _newAction);
        } else if (_newInput == KeyCode.None && _movingHorizontally && _movingVertically) {
            // if there is no movement, stop on the final frame of the current animation
            _charAnimator.StopOnFinalFrame(true);
        }

        _lastDirection = _newAction.Direction = newDirection;
        _lastInput = _newInput;
    }
}
