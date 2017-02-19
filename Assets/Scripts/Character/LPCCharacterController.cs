using Assets.Scripts.Animation;
using Assets.Scripts.Animation.ActionAnimations;
using Assets.Scripts.Animation.AnimationDirections;
using Assets.Scripts.Animation.Interfaces;
using Assets.Scripts.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LPCCharacterController : MonoBehaviour {

    LPCActionAnimationManager animationManager;
    LPCCharacterDNA characterDNA;
    BaseAction currentAction;
    LPCCharacterAnimator charAnimator;
    float moveSpeed;
    KeyCode lastInput;

	void Start () {
        //prepare character sprites
        charAnimator = gameObject.AddComponent<LPCCharacterAnimator>() as LPCCharacterAnimator;
        InitializeCharacterDNA();
        InitializeCharacterRenderer(charAnimator);

        //set defaults
        animationManager = new LPCActionAnimationManager();
        currentAction = new IdleAction();
        moveSpeed = .05f;
	}

    void Update() {
        UpdatePositioning();
        UpdateAnimation();
    }

    void InitializeCharacterDNA() {
        characterDNA = new LPCCharacterDNA();
        characterDNA.TorsoDNA = new LPCCharacterDNABlock("torso_gold_chest_male");
        characterDNA.BodyDNA = new LPCCharacterDNABlock("body_male_light");
        characterDNA.PrimaryDNA = new LPCCharacterDNABlock("weapons_right hand_male_spear_male");
    }

    void InitializeCharacterRenderer(LPCCharacterAnimator charAnimator) {
        Dictionary<string, SpriteRenderer> spriteRenderers = new Dictionary<string, SpriteRenderer>();
        GameObject body = GameObject.Find("/player/body");
        GameObject torso = GameObject.Find("/player/torso");
        GameObject primaryWeapon = GameObject.Find("/player/primary");

        SpriteRenderer bodyRenderer = body.GetComponent<SpriteRenderer>();
        SpriteRenderer torsoRenderer = torso.GetComponent<SpriteRenderer>();
        SpriteRenderer primaryWeaponRenderer = primaryWeapon.GetComponent<SpriteRenderer>();

        spriteRenderers["torso"] = torsoRenderer;
        spriteRenderers["body"] = bodyRenderer;
        spriteRenderers["primary"] = primaryWeaponRenderer;
        charAnimator.SetSpriteRenderers(spriteRenderers);
    }

    void UpdatePositioning() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            charAnimator.UpdateAnimationTime(.4f);
            moveSpeed = .045f;
        } else {
            moveSpeed = .03f;
            charAnimator.UpdateAnimationTime(.8f);
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        if (moveHorizontal > 0)
            gameObject.transform.position += new Vector3(1, 0, 0) * moveSpeed;
        else if (moveHorizontal < 0)
            gameObject.transform.position += new Vector3(-1, 0, 0) * moveSpeed;

        float moveVertical = Input.GetAxis("Vertical");
        if (moveVertical > 0)
            gameObject.transform.position += new Vector3(0, 1, 0) * moveSpeed;
        else if (moveVertical < 0)
            gameObject.transform.position += new Vector3(0, -1, 0) * moveSpeed;
    }

    void UpdateAnimation() {
        BaseAnimationDirection newDirection;
        if (Input.GetKeyDown(KeyCode.W)) {
            newDirection = new UpAnimationDirection();
            currentAction = new WalkAction();
            lastInput = KeyCode.W;
        } else if (Input.GetKeyDown(KeyCode.A)) {
            newDirection = new LeftAnimationDirection();
            currentAction = new WalkAction();
            lastInput = KeyCode.A;
        } else if (Input.GetKeyDown(KeyCode.S)) {
            newDirection = new DownAnimationDirection();
            currentAction = new WalkAction();
            lastInput = KeyCode.S;
        } else if (Input.GetKeyDown(KeyCode.D)) {
            newDirection = new RightAnimationDirection();
            currentAction = new WalkAction();
            lastInput = KeyCode.D;
        } else {
            newDirection = charAnimator.CurrentAnimationAction.Direction;
            lastInput = KeyCode.None;
        }

        bool wasWalking = charAnimator.CurrentAnimationAction is WalkAction;
        bool sameDirection = charAnimator.CurrentAnimationAction.Direction.GetType() == newDirection.GetType();

        if (!(wasWalking && sameDirection)) {
            LPCAnimationDNA animationDNA = animationManager.BuildDNAForAction(characterDNA, currentAction, newDirection);
            charAnimator.AnimateAction(animationDNA, currentAction);
        }

        if (!Input.anyKey) {
            charAnimator.StopOnFinalFrame(true);
        } 
    }
}
