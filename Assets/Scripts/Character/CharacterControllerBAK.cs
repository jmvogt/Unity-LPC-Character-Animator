using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBAK : MonoBehaviour {

    public float maxSpeed = 1f;
    public float moveSpeed = .05f;

    Animator anim;

    float speed = 1.0f;
    
    CharacterAnimator charAnimator;
    public Dictionary<string, Sprite> dictSprites = new Dictionary<string, Sprite>();

    SpriteRenderer torsoRenderer;
    SpriteRenderer bodyRenderer;

    Rigidbody2D rigidBody;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        charAnimator = gameObject.AddComponent<CharacterAnimator>() as CharacterAnimator;
        GameObject torso = GameObject.Find("/player/torso");
        torsoRenderer = torso.GetComponent<SpriteRenderer>();
        bodyRenderer = gameObject.GetComponent<SpriteRenderer>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();



	}

    KeyCode lastInput;

    void Update()
    {
        float adjustedSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            charAnimator._totalAnimTimeInSeconds = .4f;
            moveSpeed = .045f;
        }
        else
        {
            moveSpeed = .03f;
            charAnimator._totalAnimTimeInSeconds = .8f;
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

        if (Input.GetKeyDown(KeyCode.D) && charAnimator._currentAction != CharacterAnimator.Action.WALK_RIGHT)
        {
            lastInput = KeyCode.D;
            charAnimator.LoadAction(CharacterAnimator.Action.WALK_RIGHT, "body_male_orc", "torso_plate_chest_male", bodyRenderer, torsoRenderer);
        }
        else if (Input.GetKeyDown(KeyCode.A) && charAnimator._currentAction != CharacterAnimator.Action.WALK_LEFT)
        {
            lastInput = KeyCode.A;
            charAnimator.LoadAction(CharacterAnimator.Action.WALK_LEFT, "body_male_orc", "torso_plate_chest_male", bodyRenderer, torsoRenderer);
        }
        else if (Input.GetKeyDown(KeyCode.W) && charAnimator._currentAction != CharacterAnimator.Action.WALK_TOP)
        {
            lastInput = KeyCode.W;
            charAnimator.LoadAction(CharacterAnimator.Action.WALK_TOP, "body_male_orc", "torso_plate_chest_male", bodyRenderer, torsoRenderer);
        }
        else if (Input.GetKeyDown(KeyCode.S) && charAnimator._currentAction != CharacterAnimator.Action.WALK_DOWN)
        {
            lastInput = KeyCode.S;
            charAnimator.LoadAction(CharacterAnimator.Action.WALK_DOWN, "body_male_orc", "torso_plate_chest_male", bodyRenderer, torsoRenderer);
        }
        else if (Input.GetKeyUp(lastInput) && (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W)))
        {
            Debug.Log("STOPPING ON FINAL FRAME!");
            charAnimator.stopOnFinalFrame = true;
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
