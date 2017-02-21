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
    BaseAction newAction;
    LPCCharacterAnimator charAnimator;
    float moveSpeed;
    KeyCode lastInput;
    bool characterIsDirty;
    BaseAnimationDirection lastDirection;

	void Start () {
        //prepare character sprites
        charAnimator = gameObject.AddComponent<LPCCharacterAnimator>() as LPCCharacterAnimator;
        InitializeCharacterDNA();
        InitializeCharacterRenderer(charAnimator);

        //set defaults
        animationManager = new LPCActionAnimationManager();
        lastDirection = null;
        newAction = new IdleAction();
        moveSpeed = .05f;

        //LPCAnimationDNA animationDNA = animationManager.BuildDNAForAction(characterDNA, newAction, null);
        //charAnimator.AnimateAction(animationDNA, newAction);
        //charAnimator.enabled = true;
        characterIsDirty = true;
	}

    void InitializeCharacterDNA() {
        characterDNA = new LPCCharacterDNA();
        characterDNA.TorsoDNA = new LPCCharacterDNABlock("chest_female_tightdress");
        characterDNA.NeckDNA = new LPCCharacterDNABlock("neck_female_capeclip", Color.red);
        characterDNA.BodyDNA = new LPCCharacterDNABlock("body_female_light");
        characterDNA.BackDNA = new LPCCharacterDNABlock("back_female_cape", Color.red);
        characterDNA.FeetDNA = new LPCCharacterDNABlock("feet_female_shoes");
        //characterDNA.HeadDNA = new LPCCharacterDNABlock("head_female_tiara");
        characterDNA.HairDNA = new LPCCharacterDNABlock("hair_female_shoulderr", new Color(.847f, .753f, .471f, 1f));
        characterDNA.HandDNA = new LPCCharacterDNABlock("hands_female_cloth");
        characterDNA.LegDNA = new LPCCharacterDNABlock("legs_female_pants");
        //characterDNA.WaistDNA = new LPCCharacterDNABlock("waist_female_leather");
        //characterDNA.PrimaryDNA = new LPCCharacterDNABlock("weapons_oversize_two hand_either_spear");
        //characterDNA.SecondaryDNA = new LPCCharacterDNABlock("weapons_left hand_male_shield_male_cutoutforhat");

    }

    Dictionary<string, SpriteRenderer> spriteRenderers;

    void InitializeCharacterRenderer(LPCCharacterAnimator charAnimator) {
        spriteRenderers = new Dictionary<string, SpriteRenderer>();
        GameObject body = GameObject.Find("/player/body");
        GameObject torso = GameObject.Find("/player/torso");
        GameObject neck = GameObject.Find("/player/neck");
        GameObject primaryWeapon = GameObject.Find("/player/primary");
        GameObject secondaryWeapon = GameObject.Find("/player/secondary");
        GameObject back = GameObject.Find("/player/back");
        GameObject feet = GameObject.Find("/player/feet");
        GameObject head = GameObject.Find("/player/head");
        GameObject hair = GameObject.Find("/player/hair");
        GameObject hands = GameObject.Find("/player/hands");
        GameObject legs = GameObject.Find("/player/legs");
        GameObject waist = GameObject.Find("/player/waist");

        SpriteRenderer bodyRenderer = body.GetComponent<SpriteRenderer>();
        SpriteRenderer torsoRenderer = torso.GetComponent<SpriteRenderer>();
        SpriteRenderer backRenderer = back.GetComponent<SpriteRenderer>();
        SpriteRenderer feetRenderer = feet.GetComponent<SpriteRenderer>();
        SpriteRenderer headRenderer = head.GetComponent<SpriteRenderer>();
        SpriteRenderer hairRenderer = hair.GetComponent<SpriteRenderer>();
        SpriteRenderer handsRenderer = hands.GetComponent<SpriteRenderer>();
        SpriteRenderer legsRenderer = legs.GetComponent<SpriteRenderer>();
        SpriteRenderer waistRenderer = waist.GetComponent<SpriteRenderer>();
        SpriteRenderer neckRenderer = neck.GetComponent<SpriteRenderer>();
        SpriteRenderer primaryWeaponRenderer = primaryWeapon.GetComponent<SpriteRenderer>();
        SpriteRenderer secondaryWeaponRenderer = primaryWeapon.GetComponent<SpriteRenderer>();

        spriteRenderers["torso"] = torsoRenderer;
        spriteRenderers["body"] = bodyRenderer;
        spriteRenderers["back"] = backRenderer;
        spriteRenderers["feet"] = feetRenderer;
        //spriteRenderers["head"] = headRenderer;
        spriteRenderers["hair"] = hairRenderer;
        spriteRenderers["hands"] = handsRenderer;
        spriteRenderers["legs"] = legsRenderer;
        //spriteRenderers["waist"] = waistRenderer;
        spriteRenderers["neck"] = neckRenderer;
        //spriteRenderers["primary"] = primaryWeaponRenderer;
        //spriteRenderers["secondary"] = secondaryWeaponRenderer;
        charAnimator.SetSpriteRenderers(spriteRenderers);

    }

    void Update() {
        UpdatePositioning();
        UpdateAnimation();
    }

    void UpdatePositioning() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            charAnimator.UpdateAnimationTime(.3f);
            moveSpeed = .0425f;
        } else {
            charAnimator.UpdateAnimationTime(.7f);
            moveSpeed = .0275f;
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
        BaseAnimationDirection newDirection = charAnimator.CurrentAnimationAction.Direction;
        if (Input.GetKeyDown(KeyCode.W) && lastInput != KeyCode.W) {
            newDirection = new UpAnimationDirection();
            newAction = new WalkAction();
            lastInput = KeyCode.W;
        } else if (Input.GetKeyDown(KeyCode.A) && lastInput != KeyCode.A) {
            newDirection = new LeftAnimationDirection();
            newAction = new WalkAction();
            lastInput = KeyCode.A;
        } else if (Input.GetKeyDown(KeyCode.S) && lastInput != KeyCode.S) {
            newDirection = new DownAnimationDirection();
            newAction = new WalkAction();
            lastInput = KeyCode.S;
        } else if (Input.GetKeyDown(KeyCode.D) && lastInput != KeyCode.D) {
            newDirection = new RightAnimationDirection();
            newAction = new WalkAction();
            lastInput = KeyCode.D;
        } else if (Input.GetKeyDown(KeyCode.Space)) {
            newDirection = charAnimator.CurrentAnimationAction.Direction;
            newAction = new SlashAction();
            lastInput = KeyCode.Space;
        } else if (Input.GetKeyDown(KeyCode.F)) {
            newDirection = charAnimator.CurrentAnimationAction.Direction;
            newAction = new ThrustAction();
            lastInput = KeyCode.F;
        } else if (Input.GetKeyDown(KeyCode.R)) {
            newDirection = charAnimator.CurrentAnimationAction.Direction;
            newAction = new SpellcastAction();
            lastInput = KeyCode.R;
        } else if (Input.GetKeyDown(KeyCode.P)) {
            characterDNA = new LPCCharacterDNA();
            characterDNA.TorsoDNA = new LPCCharacterDNABlock("chest_male_chainmail");
            characterDNA.BodyDNA = new LPCCharacterDNABlock("body_male_orc");
            lastInput = KeyCode.P;
            characterIsDirty = true;
        } else {
            lastInput = KeyCode.None;
        }

        newAction.Direction = newDirection;

        bool wasWalking = charAnimator.CurrentAnimationAction is WalkAction;
        bool sameDirection = charAnimator.CurrentAnimationAction.Direction.GetType() == newDirection.GetType();

        if (characterIsDirty || (wasWalking && !sameDirection) || lastInput != KeyCode.None) {
            LPCAnimationDNA animationDNA = animationManager.BuildDNAForAction(characterDNA, newAction, newDirection);
            charAnimator.AnimateAction(animationDNA, newAction, characterIsDirty);
            characterIsDirty = false;
        } else if (!Input.anyKey) {
            charAnimator.StopOnFinalFrame(true);
        } 
    }
}
