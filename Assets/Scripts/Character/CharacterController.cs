using Assets.Scripts.Animation;
using Assets.Scripts.Animation.ActionAnimations;
using Assets.Scripts.Animation.AnimationDirections;
using Assets.Scripts.Animation.Interfaces;
using Assets.Scripts.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public float maxSpeed = 1f;
    public float moveSpeed = .05f;

    float speed = 1.0f;
    
    CharacterAnimator charAnimator;
    public Dictionary<string, Sprite> dictSprites = new Dictionary<string, Sprite>();

    SpriteRenderer torsoRenderer;
    SpriteRenderer bodyRenderer;

    ActionAnimationManager animationManager;
    CharacterDNA characterDNA;
    BaseAction currentAction;

	void Start () {
        characterDNA = new CharacterDNA();
        characterDNA.TorsoDNA = new CharacterDNABlock("torso_gold_chest_male");
        characterDNA.BodyDNA = new CharacterDNABlock("body_male_light");

        animationManager = new ActionAnimationManager();

        charAnimator = gameObject.AddComponent<CharacterAnimator>() as CharacterAnimator;

        Dictionary<string, SpriteRenderer> spriteRenderers = new Dictionary<string, SpriteRenderer>();
        GameObject torso = GameObject.Find("/player/torso");
        torsoRenderer = torso.GetComponent<SpriteRenderer>();
        bodyRenderer = gameObject.GetComponent<SpriteRenderer>();

        spriteRenderers["torso"] = torsoRenderer;
        spriteRenderers["body"] = bodyRenderer;
        charAnimator.SetSpriteRenderers(spriteRenderers);

        currentAction = new IdleAction();
        // TODO: Lets fix the default case so we dont get errors below..
        currentAction.Direction = new RightAnimationDirection();
	}
    
    KeyCode lastInput;

    void Update()
    {
        float adjustedSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            charAnimator.UpdateAnimationTime(.4f);
            moveSpeed = .045f;
        }
        else
        {
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


        bool alreadyWalking = String.Equals(charAnimator.CurrentAnimationAction.GetAnimationType(), "walk");
        bool differentDirection = currentAction.Direction == null;
        if (Input.GetKeyDown(KeyCode.W)) {
            // TODO: I dont like this >.>
            //if (!alreadyWalking && (hasNoDirection || !String.Equals(currentAction.Direction.GetAnimationDirection(), "t"))) { 
                lastInput = KeyCode.W;
                currentAction = new WalkAction();
                AnimationDNA animationDNA = animationManager.BuildDNAForAction(characterDNA, currentAction, new UpAnimationDirection());
                charAnimator.AnimateAction(animationDNA, currentAction);
            //}
        } else if (Input.GetKeyDown(KeyCode.A)) {
            //if (!alreadyWalking && (hasNoDirection || !String.Equals(currentAction.Direction.GetAnimationDirection(), "l"))) {
                lastInput = KeyCode.A;
                currentAction = new WalkAction();
                AnimationDNA animationDNA = animationManager.BuildDNAForAction(characterDNA, currentAction, new LeftAnimationDirection());
                charAnimator.AnimateAction(animationDNA, currentAction);
            //}
        } else if (Input.GetKeyDown(KeyCode.S)) {
            //if (!alreadyWalking && (hasNoDirection || !String.Equals(currentAction.Direction.GetAnimationDirection(), "d"))) {
                lastInput = KeyCode.S;
                currentAction = new WalkAction();
                AnimationDNA animationDNA = animationManager.BuildDNAForAction(characterDNA, currentAction, new DownAnimationDirection());
                charAnimator.AnimateAction(animationDNA, currentAction);
            //}
        } else if (Input.GetKeyDown(KeyCode.D)) {
            //if (!alreadyWalking && (hasNoDirection || !String.Equals(currentAction.Direction.GetAnimationDirection(), "r"))) {
                lastInput = KeyCode.D;
                currentAction = new WalkAction();
                AnimationDNA animationDNA = animationManager.BuildDNAForAction(characterDNA, currentAction, new RightAnimationDirection());
                charAnimator.AnimateAction(animationDNA, currentAction);
            //}
        } else if (Input.GetKeyUp(lastInput) && (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W))) {
            Debug.Log("STOPPING ON FINAL FRAME!");
            charAnimator.StopOnFinalFrame(true);
        } 
        
        foreach (Transform child in transform)
        {
            Animator childAnimator = child.GetComponent<Animator>();
            if (childAnimator) {
                childAnimator.SetFloat("xSpeed", moveHorizontal);
            }
        }
    }

}
