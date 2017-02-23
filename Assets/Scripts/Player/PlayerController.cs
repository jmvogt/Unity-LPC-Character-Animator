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
    private CharacterAnimator _charAnimator;

    private BaseAction _newAction;
    private KeyCode _newInput;
    private string _lastDirection;

    private GameObject _playerObject;
    private float _moveSpeed;
    
	void Start () {
        //prepare character sprites
        _playerObject = GameObject.Find("/player");
        _charAnimator = gameObject.AddComponent<CharacterAnimator>() as CharacterAnimator;
        InitializeCharacterRenderers(_charAnimator);

        //set defaults
        _animationManager = new AnimationManager();
        _newAction = new IdleAction();
        _lastDirection = DirectionType.NONE;
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
        string newDirection = DirectionType.NONE;
        if (Input.GetKeyDown(KeyCode.W) && _newInput != KeyCode.W) {
            newDirection = DirectionType.UP;
            _newAction = new WalkAction();
            _newInput = KeyCode.W;
        } else if (Input.GetKeyDown(KeyCode.A) && _newInput != KeyCode.A) {
            newDirection = DirectionType.LEFT;
            _newAction = new WalkAction();
            _newInput = KeyCode.A;
        } else if (Input.GetKeyDown(KeyCode.S) && _newInput != KeyCode.S) {
            newDirection = DirectionType.DOWN;
            _newAction = new WalkAction();
            _newInput = KeyCode.S;
        } else if (Input.GetKeyDown(KeyCode.D) && _newInput != KeyCode.D) {
            newDirection = DirectionType.RIGHT;
            _newAction = new WalkAction();
            _newInput = KeyCode.D;
        } else if (Input.GetKeyDown(KeyCode.Space)) {
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
        } else {
            _newInput = KeyCode.None;
        }

        if (newDirection == DirectionType.NONE)
            newDirection = _lastDirection;

        _lastDirection = _newAction.Direction = newDirection;

        bool wasWalking = _charAnimator.CurrentAnimationAction is WalkAction;
        bool sameDirection = _lastDirection == newDirection;

        if ((wasWalking && !sameDirection) || _newInput != KeyCode.None || Player.characterDNA.IsDirty()) {
            _animationManager.UpdateDNAForAction(Player.characterDNA, Player.animationDNA, _newAction, newDirection);
            _charAnimator.AnimateAction(Player.animationDNA, _newAction);
        } else if (!Input.anyKey) {
            _charAnimator.StopOnFinalFrame(true);
        }

    }
}
