
using Assets.Scripts.Animation;
using Assets.Scripts.Animation.ActionAnimations;
using Assets.Scripts.Animation.AnimationDirections;
using Assets.Scripts.Animation.Interfaces;
using Assets.Scripts.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private AnimationManager _animationManager;
    private CharacterAnimator _charAnimator;

    private BaseAnimationDirection _lastDirection;
    private BaseAction _newAction;
    private KeyCode _lastInput;

    private GameObject _playerObject;
    private float _moveSpeed;
    
	void Start () {
        //prepare character sprites
        _playerObject = GameObject.Find("/player");
        _charAnimator = gameObject.AddComponent<CharacterAnimator>() as CharacterAnimator;
        InitializeCharacterRenderers(_charAnimator);

        //set defaults
        _animationManager = new AnimationManager();
        _lastDirection = null;
        _newAction = new IdleAction();
        _moveSpeed = .0275f;
	}

    void InitializeCharacterRenderers(CharacterAnimator charAnimator) {
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
        if (moveHorizontal > 0)
            gameObject.transform.position += new Vector3(1, 0, 0) * _moveSpeed;
        else if (moveHorizontal < 0)
            gameObject.transform.position += new Vector3(-1, 0, 0) * _moveSpeed;

        float moveVertical = Input.GetAxis("Vertical");
        if (moveVertical > 0)
            gameObject.transform.position += new Vector3(0, 1, 0) * _moveSpeed;
        else if (moveVertical < 0)
            gameObject.transform.position += new Vector3(0, -1, 0) * _moveSpeed;
    }

    void UpdateAnimation() {
        BaseAnimationDirection newDirection = null;
        if (Input.GetKeyDown(KeyCode.W) && _lastInput != KeyCode.W) {
            newDirection = new UpAnimationDirection();
            _newAction = new WalkAction();
            _lastInput = KeyCode.W;
        } else if (Input.GetKeyDown(KeyCode.A) && _lastInput != KeyCode.A) {
            newDirection = new LeftAnimationDirection();
            _newAction = new WalkAction();
            _lastInput = KeyCode.A;
        } else if (Input.GetKeyDown(KeyCode.S) && _lastInput != KeyCode.S) {
            newDirection = new DownAnimationDirection();
            _newAction = new WalkAction();
            _lastInput = KeyCode.S;
        } else if (Input.GetKeyDown(KeyCode.D) && _lastInput != KeyCode.D) {
            newDirection = new RightAnimationDirection();
            _newAction = new WalkAction();
            _lastInput = KeyCode.D;
        } else if (Input.GetKeyDown(KeyCode.Space)) {
            newDirection = _charAnimator.CurrentAnimationAction.Direction;
            _newAction = new SlashAction();
            _lastInput = KeyCode.Space;
        } else if (Input.GetKeyDown(KeyCode.F)) {
            newDirection = _charAnimator.CurrentAnimationAction.Direction;
            _newAction = new ThrustAction();
            _lastInput = KeyCode.F;
        } else if (Input.GetKeyDown(KeyCode.R)) {
            newDirection = _charAnimator.CurrentAnimationAction.Direction;
            _newAction = new SpellcastAction();
            _lastInput = KeyCode.R;
        } else {
            _lastInput = KeyCode.None;
        }

        if (newDirection == null)
            if (_charAnimator.CurrentAnimationAction == null)
                newDirection = new DownAnimationDirection();
            else
                newDirection = _charAnimator.CurrentAnimationAction.Direction;

        _newAction.Direction = newDirection;

        bool wasWalking = _charAnimator.CurrentAnimationAction is WalkAction;
        bool sameDirection = _charAnimator.CurrentAnimationAction.Direction.GetType() == newDirection.GetType();

        if (Player.characterDNA.IsDirty() && (wasWalking && !sameDirection) || _lastInput != KeyCode.None) {
            _animationManager.UpdateDNAForAction(Player.characterDNA, Player.animationDNA, _newAction, newDirection);
            _charAnimator.AnimateAction(Player.animationDNA, _newAction);
        } else if (!Input.anyKey) {
            _charAnimator.StopOnFinalFrame(true);
        } 
    }
}
