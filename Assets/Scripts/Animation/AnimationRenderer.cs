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
        private float _totalAnimTimeInSeconds;

        public BaseAction CurrentAnimationAction { get; private set; }

        public void AnimateAction(AnimationDNA animationDNA, BaseAction animationAction)
        {
            _animationDNA = animationDNA;
            CurrentAnimationAction = animationAction;
            _stopOnFinalFrame = animationAction.GetStopOnLastFrame();
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

        public void StopOnFinalFrame(bool stopOnFinalFrame)
        {
            _stopOnFinalFrame = stopOnFinalFrame;
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

            _currentFrameNumber++;
            _passedTime += Time.deltaTime;
            var currentFrameIndex = _currentFrameNumber % CurrentAnimationAction.NumberOfFrames;

            if (_stopOnFinalFrame && _currentFrameNumber % CurrentAnimationAction.NumberOfFrames == 0)
            {
                _playing = false;
                foreach (var animationKey in _animationDNA.DNABlocks.Keys)
                {
                    RenderAnimationFrame(animationKey, 0);
                }

                return;
            }

            var singleAnimTime = _totalAnimTimeInSeconds / CurrentAnimationAction.NumberOfFrames;
            if (_passedTime >= singleAnimTime)
            {
                foreach (var animationKey in _animationDNA.DNABlocks.Keys)
                {
                    RenderAnimationFrame(animationKey, currentFrameIndex);
                }

                _passedTime -= singleAnimTime;
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
