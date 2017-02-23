﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts.Animation;
using Assets.Scripts.Animation.Interfaces;
using Assets.Scripts.Animation.ActionAnimations;

public class CharacterAnimator : MonoBehaviour
{       
    private Dictionary<string, SpriteRenderer> _spriteRenderers;
    private AnimationManager _animationManager;
    private BaseAction _currentAnimationAction;
    private AnimationDNA _animationDNA;
    private bool _playing;
    private int _currentFrameNumber;
    private float _totalAnimTimeInSeconds;
    private bool _stopOnFinalFrame;
    private float _passedTime;

    public BaseAction CurrentAnimationAction { get { return _currentAnimationAction; } }

    public void AnimateAction(AnimationDNA animationDNA, BaseAction animationAction) {
        _animationDNA = animationDNA;
        _currentAnimationAction = animationAction;
        _stopOnFinalFrame = animationAction.GetStopOnLastFrame();
        _playing = true;
        _currentFrameNumber = 0;
        _totalAnimTimeInSeconds = .4f;
    }

    public void UpdateAnimationTime(float totalAnimTimeInSeconds) {
        _totalAnimTimeInSeconds = totalAnimTimeInSeconds;
    }

    public void InitializeSpriteRenderers(Dictionary<string, SpriteRenderer> spriteRenderers) {
        _spriteRenderers = spriteRenderers;
    }

    public void StopOnFinalFrame(bool stopOnFinalFrame) {
        _stopOnFinalFrame = stopOnFinalFrame;
    }

    void Start() {
        _passedTime = 0;
        _playing = true;
        _animationManager = new AnimationManager();
        _currentAnimationAction = new IdleAction();
    }

    void Update()
    {
        if (_playing && _animationDNA != null)
        {
            int currentFrameIndex = _currentFrameNumber % _currentAnimationAction.NumberOfFrames;

            _currentFrameNumber++;

            if (_stopOnFinalFrame && _currentFrameNumber % _currentAnimationAction.NumberOfFrames == 0)
            {
                _playing = false;
                foreach(string animationKey in _animationDNA.DNABlocks.Keys) {
                    AnimationDNABlock animationDNABlock = _animationDNA.DNABlocks[animationKey];
                    if (animationDNABlock.Enabled) { 
                        SpriteRenderer renderer = _spriteRenderers[animationKey];
                        renderer.sprite = animationDNABlock.SpriteList[0];
                        renderer.sortingOrder = _animationDNA.GetSortingOrder(animationKey);
                        renderer.sortingLayerName = "Units";

                        // TODO: Fix ordering so hair is hidden when going up..
                        if (CurrentAnimationAction.Direction.GetAnimationDirection() == "t" && animationKey == "BACK") {
                            renderer.sortingOrder = 100;
                        }

                        if (animationDNABlock.SpriteColor != Color.clear) {
                            renderer.material.SetColor("_Color", animationDNABlock.SpriteColor);
                        }
                    }
                }
                return;
            }

            float singleAnimTime = _totalAnimTimeInSeconds / _currentAnimationAction.NumberOfFrames;
            if (_passedTime >= singleAnimTime)
            {
                foreach (string animationKey in _animationDNA.DNABlocks.Keys) {
                    SpriteRenderer renderer = _spriteRenderers[animationKey];
                    AnimationDNABlock animationDNABlock = _animationDNA.DNABlocks[animationKey];
                    if (animationDNABlock.Enabled) {
                        renderer.sprite = animationDNABlock.SpriteList[currentFrameIndex];
                        renderer.sortingOrder = _animationDNA.GetSortingOrder(animationKey); ;
                        renderer.sortingLayerName = "Units";

                        // TODO: Fix ordering so hair is hidden when going up..
                        if (CurrentAnimationAction.Direction.GetAnimationDirection() == "t" && animationKey == "BACK") {
                            renderer.sortingOrder = 100;
                        }

                        // TODO: Really, we should be able to set things as clear if needed..
                        if (animationDNABlock.SpriteColor != Color.clear) {
                            renderer.material.SetColor("_Color", animationDNABlock.SpriteColor);
                        }
                    }
                }
                
                _passedTime -= singleAnimTime;
            }

            _passedTime += Time.deltaTime;
        }
    }
}