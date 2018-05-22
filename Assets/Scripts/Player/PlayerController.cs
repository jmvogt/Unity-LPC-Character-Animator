using System.Collections.Generic;
using Assets.Scripts.Animation;
using Assets.Scripts.Animation.ActionAnimations;
using Assets.Scripts.Animation.Interfaces;
using Assets.Scripts.Character;
using Assets.Scripts.Types;
using UnityEngine;

// ReSharper disable FieldCanBeMadeReadOnly.Global

public class PlayerController : MonoBehaviour
{
    // ReSharper disable once NotAccessedField.Local
    private AnimationManager _animationManager;
    private AnimationRenderer _charAnimator;
    private string _lastDirection;
    private KeyCode _lastInput;
    private BaseAction _newAction;
    private KeyCode _newInput;
    private GameObject _playerObject;
    public float SpeedWalk = 1;
    public float SpeedRun = 2;
    public float SpeedAnimation = 1;
    public float SpeedCurrent = 1;
    private bool _isStillMoving;

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
            SpeedCurrent = SpeedRun;
        }
        else
        {
            SpeedCurrent = SpeedWalk;
        }
        SpeedAnimation = SpeedCurrent;

        var moveAmount = SpeedCurrent * Time.deltaTime;
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        var absHorizontal = Mathf.Abs(moveHorizontal);
        var absVertical = Mathf.Abs(moveVertical);

        var isDiagonal = absVertical > 0 && absVertical > 0;
        if (isDiagonal) moveAmount *= 0.75f; // account for diagonal movement speed increase

        if (moveHorizontal > 0)
        {
            gameObject.transform.position += Vector3.right * moveAmount * absHorizontal;
        }
        else if (moveHorizontal < 0)
        {
            gameObject.transform.position += Vector3.left * moveAmount * absHorizontal;
        }

        if (moveVertical > 0)
        {
            gameObject.transform.position += Vector3.up * moveAmount * absVertical;
        }
        else if (moveVertical < 0)
        {
            gameObject.transform.position += Vector3.down * moveAmount * absVertical;
        }

        _isStillMoving = absHorizontal > 0 || absVertical > 0;
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
            _newAction = new SlashAction();
            _newInput = KeyCode.Space;
            SpeedAnimation = 1.0f;
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            _newAction = new ThrustAction();
            _newInput = KeyCode.F;
            SpeedAnimation = 1.0f;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            _newAction = new SpellcastAction();
            _newInput = KeyCode.R;
            SpeedAnimation = 1.0f;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            _newAction = new ShootAction();
            _newInput = KeyCode.E;
            SpeedAnimation = 1.0f;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            _newAction = new DeathAction();
            _newInput = KeyCode.X;
            SpeedAnimation = 1.0f;
        }
        else
            _newInput = KeyCode.None;

        // continue using the last direction when the character stops moving
        if (newDirection == DirectionType.None)
            newDirection = _lastDirection;

        var sameAction = _lastDirection == newDirection && _lastInput == _newInput;

        _charAnimator.UpdateAnimationTime(1 / SpeedAnimation);

        if (!sameAction || Player.CharacterDNA.IsDirty())
        {
            AnimationManager.UpdateDNAForAction(Player.CharacterDNA, Player.AnimationDNA, _newAction, newDirection);
            _charAnimator.AnimateAction(Player.AnimationDNA, _newAction);
        }
        else if (!Input.anyKey && !_isStillMoving)
        {
            _charAnimator.ResetAnimation();
        }

        _lastDirection = _newAction.Direction = newDirection;
        _lastInput = _newInput;
    }
}
