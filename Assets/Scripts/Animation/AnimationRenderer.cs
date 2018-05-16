using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Animation.ActionAnimations;
using Assets.Scripts.Animation.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Animation
{
    public class AnimationRenderer : MonoBehaviour
    {
        private AnimationDNA _animationDNA;
        private int _currentFrameNumber;
        private float _passedTime;
        private bool _playing;
        private Dictionary<string, SpriteRenderer> _spriteRenderers;
        private bool _stopOnFinalFrame;
        private bool _stopNow;
        private float _totalAnimTimeInSeconds;

        public BaseAction CurrentAnimationAction { get; private set; }

        public void AnimateAction(AnimationDNA animationDNA, BaseAction animationAction)
        {
            _animationDNA = animationDNA;
            CurrentAnimationAction = animationAction;
            _stopOnFinalFrame = animationAction.StopOnLastFrame;
            _playing = true;
            _currentFrameNumber = 0;
            _totalAnimTimeInSeconds = 2f;
        }

        public void UpdateAnimationTime(float totalAnimTimeInSeconds)
        {
            _totalAnimTimeInSeconds = totalAnimTimeInSeconds;
        }

        public void InitializeSpriteRenderers(Dictionary<string, SpriteRenderer> spriteRenderers)
        {
            _spriteRenderers = spriteRenderers;
        }

        public void StopOnFinalFrame(bool stopOnFinalFrame, bool stopNow)
        {
            _stopOnFinalFrame = stopOnFinalFrame;
            _stopNow = stopNow;
        }

        private void Start()
        {
            _passedTime = 0;
            _playing = true;
            CurrentAnimationAction = new IdleAction();
        }

        private void Update()
        {
            if (!_playing) return;
            var hasAnimationKeys = _animationDNA?.DNABlocks?.Keys.Any() == true;
            if (!hasAnimationKeys) return;

            var currentFrameIndex = _currentFrameNumber % CurrentAnimationAction.NumberOfFrames;
            if (_stopOnFinalFrame && (currentFrameIndex == 0 || _stopNow))
            {
                _playing = false;
                _passedTime = 0;
                foreach (var animationKey in _animationDNA.DNABlocks.Keys)
                {
                    RenderAnimationFrame(animationKey, 0);
                }

                return;
            }

            _passedTime += Time.deltaTime;
            var singleAnimTime = _totalAnimTimeInSeconds / CurrentAnimationAction.NumberOfFrames;
            if (_passedTime >= singleAnimTime)
            {
                _currentFrameNumber++;
                foreach (var animationKey in _animationDNA.DNABlocks.Keys)
                {
                    RenderAnimationFrame(animationKey, currentFrameIndex);
                }

                _passedTime = 0;
            }
        }

        private void RenderAnimationFrame(string animationKey, int currentFrameIndex)
        {
            var animationDNABlock = _animationDNA.DNABlocks[animationKey];
            var rendererCurrent = _spriteRenderers[animationKey];
            if (animationDNABlock?.Enabled == true)
            {
                rendererCurrent.sprite = animationDNABlock.SpriteList[currentFrameIndex];
                rendererCurrent.sortingOrder = animationDNABlock.SortingOrder;
                rendererCurrent.sortingLayerName = "Units";

                // Don't color clear objects
                if (animationDNABlock.SpriteColor != Color.clear) rendererCurrent.material.SetColor("_Color", animationDNABlock.SpriteColor);
            }
            else
                rendererCurrent.sprite = null;
        }
    }
}
