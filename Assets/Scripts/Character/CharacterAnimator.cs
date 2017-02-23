using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts.Animation;
using Assets.Scripts.Animation.Interfaces;
using Assets.Scripts.Animation.ActionAnimations;
using Assets.Scripts.Types;

public class CharacterAnimator : MonoBehaviour
{       
    private Dictionary<string, SpriteRenderer> _spriteRenderers;
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
        _currentAnimationAction = new IdleAction();
    }

    void Update()
    {
        if (_playing)
        {
            int currentFrameIndex = _currentFrameNumber % _currentAnimationAction.NumberOfFrames;

            _currentFrameNumber++;

            if (_stopOnFinalFrame && _currentFrameNumber % _currentAnimationAction.NumberOfFrames == 0)
            {
                _playing = false;
                foreach(string animationKey in _animationDNA.DNABlocks.Keys) {
                    RenderAnimationFrame(animationKey, 0);
                }
                return;
            }

            float singleAnimTime = _totalAnimTimeInSeconds / _currentAnimationAction.NumberOfFrames;
            if (_passedTime >= singleAnimTime)
            {
                foreach (string animationKey in _animationDNA.DNABlocks.Keys) {
                    RenderAnimationFrame(animationKey, currentFrameIndex);
                }
                
                _passedTime -= singleAnimTime;
            }

            _passedTime += Time.deltaTime;
        }
    }

    void RenderAnimationFrame(string animationKey, int currentFrameIndex) {
        AnimationDNABlock animationDNABlock = _animationDNA.DNABlocks[animationKey];
        if (animationDNABlock.Enabled) {
            SpriteRenderer renderer = _spriteRenderers[animationKey];
            renderer.sprite = animationDNABlock.SpriteList[currentFrameIndex];
            renderer.sortingOrder = animationDNABlock.SortingOrder;
            renderer.sortingLayerName = "Units";

            // Don't color clear objects
            if (animationDNABlock.SpriteColor != Color.clear) {
                renderer.material.SetColor("_Color", animationDNABlock.SpriteColor);
            }
        }
    }
}