using Assets.Scripts.Animation;
using Assets.Scripts.Animation.ActionAnimations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.Scene {
    class SceneController : MonoBehaviour {

        private LPCActionAnimationManager animationManager;
        private bool animationsLoading;
        private bool animationsLoaded;

        private GameObject player;
        private LPCCharacterController playerController;


        void Start() {
            animationManager = new LPCActionAnimationManager();
            animationsLoaded = false;
            animationsLoading = false;

            player = GameObject.Find("player");
            playerController = player.GetComponent<LPCCharacterController>();

        }
        void OnGUI() {
            if (!animationsLoaded) {
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Loading... " + Math.Floor((LPCAtlasManager.GetModelsLoaded() / LPCAtlasManager.GetModelsTotal() * 100)) + "%");
                if (LPCAtlasManager.GetModelsLoaded() == LPCAtlasManager.GetModelsTotal()) {
                    animationsLoaded = true;
                    playerController.enabled = true;
                }
            }
                
        }

        void Update() {
            if (!animationsLoading) {
                animationsLoading = true;
                Thread thread = new Thread(animationManager.LoadAllAnimationsIntoCache);
                thread.Start();
            }
        }
    }
}
