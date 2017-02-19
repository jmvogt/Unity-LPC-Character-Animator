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

    public void AnimateAction(LPCAnimationDNA animationDNA, BaseAction animationAction) {
        _animationDNA = animationDNA;
        _animationCache = animationDNA.GetAnimationCache();
        _currentAnimationAction = animationAction;
        _stopOnFinalFrame = animationAction.GetStopOnLastFrame();
        _playing = true;
        _currentFrameNumber = 0;
        _totalAnimTimeInSeconds = .4f;
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
        _playing = false;
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
                foreach(string rendererKey in _spriteRenderers.Keys) {
                    _spriteRenderers[rendererKey].sprite = _animationCache[rendererKey].SpriteList[0];
                }
                return;
            }

            float singleAnimTime = _totalAnimTimeInSeconds / _currentAnimationAction.NumberOfFrames;
            if (_passedTime >= singleAnimTime)
            {
                foreach (string rendererKey in _spriteRenderers.Keys) {
                    SpriteRenderer renderer = _spriteRenderers[rendererKey];
                    BaseAnimationDNABlock animationDNABlock = _animationCache[rendererKey];
                    renderer.sprite = animationDNABlock.SpriteList[currentFrameIndex];
                    renderer.sortingOrder = animationDNABlock.SortingOrder;
                    renderer.sortingLayerName = "Units"; 
                }
                
                _passedTime -= singleAnimTime;
            }

            _passedTime += Time.deltaTime;
        }
    }
}