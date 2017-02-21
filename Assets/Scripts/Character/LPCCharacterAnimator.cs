using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts.Animation;
using Assets.Scripts.Animation.Interfaces;
using Assets.Scripts.Animation.ActionAnimations;
using Assets.Scripts.Animation.DNABlocks;

public class LPCCharacterAnimator : MonoBehaviour
{       
    private Dictionary<string, SpriteRenderer> _spriteRenderers;
    private Dictionary<string, BaseAnimationDNABlock> _animationCache;
    private LPCActionAnimationManager _animationManager;
    private BaseAction _currentAnimationAction;
    private LPCAnimationDNA _animationDNA;
    private bool _playing;
    private int _currentFrameNumber;
    private float _totalAnimTimeInSeconds;
    private bool _stopOnFinalFrame;
    private float _passedTime;

    public BaseAction CurrentAnimationAction { get { return _currentAnimationAction; } }

    public void AnimateAction(LPCAnimationDNA animationDNA, BaseAction animationAction, bool isDirty) {
        _animationDNA = animationDNA;
        _animationCache = animationDNA.GetAnimationCache();
        _currentAnimationAction = animationAction;
        _stopOnFinalFrame = animationAction.GetStopOnLastFrame();
        _playing = true;
        _currentFrameNumber = 0;
        _totalAnimTimeInSeconds = .4f;
        if (isDirty) {
            foreach (string rendererKey in _spriteRenderers.Keys) {
                if (!_animationCache.ContainsKey(rendererKey)) {
                    _spriteRenderers[rendererKey].enabled = false;
                } else {
                    _spriteRenderers[rendererKey].enabled = true;
                }
            }
        }
    }

    public void UpdateAnimationTime(float totalAnimTimeInSeconds) {
        _totalAnimTimeInSeconds = totalAnimTimeInSeconds;
    }

    public void SetSpriteRenderers(Dictionary<string, SpriteRenderer> spriteRenderers) {
        _spriteRenderers = spriteRenderers;
    }

    public void StopOnFinalFrame(bool stopOnFinalFrame) {
        _stopOnFinalFrame = stopOnFinalFrame;
    }

    void Start() {
        _passedTime = 0;
        _playing = true;
        _animationManager = new LPCActionAnimationManager();
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
                foreach(string animationKey in _animationCache.Keys) {
                    BaseAnimationDNABlock animationDNABlock = _animationCache[animationKey];
                    SpriteRenderer renderer = _spriteRenderers[animationKey];
                    renderer.sprite = animationDNABlock.SpriteList[0];
                    renderer.sortingOrder = animationDNABlock.SortingOrder;
                    renderer.sortingLayerName = "Units";

                    // TODO: Fix ordering so hair is hidden when going up..
                    if (CurrentAnimationAction.Direction.GetAnimationDirection() == "t" && animationKey == "back") {
                        renderer.sortingOrder = 100;
                    }

                    if (animationDNABlock.SpriteColor != Color.clear) {
                        renderer.material.SetColor("_Color", animationDNABlock.SpriteColor);
                    }
                }
                return;
            }

            float singleAnimTime = _totalAnimTimeInSeconds / _currentAnimationAction.NumberOfFrames;
            if (_passedTime >= singleAnimTime)
            {
                foreach (string animationKey in _animationCache.Keys) {
                    SpriteRenderer renderer = _spriteRenderers[animationKey];
                    BaseAnimationDNABlock animationDNABlock = _animationCache[animationKey];
                    renderer.sprite = animationDNABlock.SpriteList[currentFrameIndex];
                    renderer.sortingOrder = animationDNABlock.SortingOrder;
                    renderer.sortingLayerName = "Units";

                    // TODO: Fix ordering so hair is hidden when going up..
                    if (CurrentAnimationAction.Direction.GetAnimationDirection() == "t" && animationKey == "back") {
                        renderer.sortingOrder = 100;
                    }

                    // TODO: Really, we should be able to set things as clear if needed..
                    if (animationDNABlock.SpriteColor != Color.clear) {
                        renderer.material.SetColor("_Color", animationDNABlock.SpriteColor);  
                    }
                }
                
                _passedTime -= singleAnimTime;
            }

            _passedTime += Time.deltaTime;
        }
    }
}