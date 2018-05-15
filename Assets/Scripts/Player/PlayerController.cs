using System.Collections.Generic;
using Assets.Scripts.Animation;
using Assets.Scripts.Animation.ActionAnimations;
using Assets.Scripts.Animation.Interfaces;
using Assets.Scripts.Character;
using Assets.Scripts.Types;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ReSharper disable once NotAccessedField.Local
    private AnimationManager _animationManager;
    private AnimationRenderer _charAnimator;
    private string _lastDirection;
    private KeyCode _lastInput;
    private float _moveSpeed;
    private BaseAction _newAction;
    private KeyCode _newInput;
    private GameObject _playerObject;

    private void Start()
    {
        //prepare character sprites
        _playerObject = GameObject.Find("/player");
        _charAnimator = gameObject.AddComponent<AnimationRenderer>();
        InitializeCharacterRenderers(_charAnimator);

        //set defaults
        _animationManager = new AnimationManager();
        _newAction = new IdleAction();
        _lastDirection = DirectionType.Down;
        _moveSpeed = .0275f;
    }

    private void InitializeCharacterRenderers(AnimationRenderer charAnimator)
    {
        var spriteRenderers = new Dictionary<string, SpriteRenderer>();
        foreach (var blockKey in DNABlockType.TypeList)
        {
            var blockObject = new GameObject(blockKey);
            blockObject.transform.parent = _playerObject.transform;
            spriteRenderers[blockKey] = blockObject.AddComponent<SpriteRenderer>();
        }

        charAnimator.InitializeSpriteRenderers(spriteRenderers);
    }

    private void Update()
    {
        UpdatePositioning();
        UpdateAnimation();
    }

    private void UpdatePositioning()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _charAnimator.UpdateAnimationTime(.3f);
            _moveSpeed = .0425f;
        }
        else
        {
            _charAnimator.UpdateAnimationTime(.7f);
            _moveSpeed = .0275f;
        }

        var moveHorizontal = Input.GetAxis("Horizontal");
        if (moveHorizontal > 0)
            gameObject.transform.position += new Vector3(1, 0, 0) * _moveSpeed;
        else if (moveHorizontal < 0)
            gameObject.transform.position += new Vector3(-1, 0, 0) * _moveSpeed;

        var moveVertical = Input.GetAxis("Vertical");
        if (moveVertical > 0)
            gameObject.transform.position += new Vector3(0, 1, 0) * _moveSpeed;
        else if (moveVertical < 0)
            gameObject.transform.position += new Vector3(0, -1, 0) * _moveSpeed;
    }

    private void UpdateAnimation()
    {
        var newDirection = DirectionType.None;
        if (Input.GetKeyDown(KeyCode.W) && _newInput != KeyCode.W)
        {
            newDirection = DirectionType.Up;
            _newAction = new WalkAction();
            _newInput = KeyCode.W;
        }
        else if (Input.GetKeyDown(KeyCode.A) && _newInput != KeyCode.A)
        {
            newDirection = DirectionType.Left;
            _newAction = new WalkAction();
            _newInput = KeyCode.A;
        }
        else if (Input.GetKeyDown(KeyCode.S) && _newInput != KeyCode.S)
        {
            newDirection = DirectionType.Down;
            _newAction = new WalkAction();
            _newInput = KeyCode.S;
        }
        else if (Input.GetKeyDown(KeyCode.D) && _newInput != KeyCode.D)
        {
            newDirection = DirectionType.Right;
            _newAction = new WalkAction();
            _newInput = KeyCode.D;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            newDirection = _lastDirection;
            _newAction = new SlashAction();
            _newInput = KeyCode.Space;
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            newDirection = _lastDirection;
            _newAction = new ThrustAction();
            _newInput = KeyCode.F;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            newDirection = _lastDirection;
            _newAction = new SpellcastAction();
            _newInput = KeyCode.R;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            newDirection = _lastDirection;
            _newAction = new ShootAction();
            _newInput = KeyCode.E;
        }
        else
            _newInput = KeyCode.None;

        // continue using the last direction when the character stops moving
        if (newDirection == DirectionType.None)
            newDirection = _lastDirection;

        var sameAction = _lastDirection == newDirection && _lastInput == _newInput;

        if (!sameAction || Player.CharacterDNA.IsDirty())
        {
            AnimationManager.UpdateDNAForAction(Player.CharacterDNA, Player.AnimationDNA, _newAction, newDirection);
            _charAnimator.AnimateAction(Player.AnimationDNA, _newAction);
        }
        else if (!Input.anyKey) _charAnimator.StopOnFinalFrame(true);

        _lastDirection = _newAction.Direction = newDirection;
        _lastInput = _newInput;
    }
}
