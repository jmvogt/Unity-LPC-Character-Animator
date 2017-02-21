using Assets.Scripts.Animation;
using Assets.Scripts.Animation.ActionAnimations;
using Assets.Scripts.Character;
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

        private string bodyKey = "body_female_light";
        private string torsoKey = "chest_female_tightdress";
        private string neckKey = "neck_female_capeclip";
        private string backKey = "back_female_cape";
        private string feetKey = "feet_female_shoes";
        private string hairKey = "hair_female_shoulderr";
        private string handKey = "hands_female_cloth";
        private string legKey = "legs_female_pants";

        private string bodyR = "";
        private string bodyG = "";
        private string bodyB = "";
        private string bodyT = "";

        private string torsoR = "";
        private string torsoG = "";
        private string torsoB = "";
        private string torsoT = "";

        private string neckR = "";
        private string neckG = "";
        private string neckB = "";
        private string neckT = "";

        private string backR = "";
        private string backG = "";
        private string backB = "";
        private string backT = "";

        private string feetR = "";
        private string feetG = "";
        private string feetB = "";
        private string feetT = "";

        private string hairR = "";
        private string hairG = "";
        private string hairB = "";
        private string hairT = "";

        private string handR = "";
        private string handG = "";
        private string handB = "";
        private string handT = "";

        private string legR = "";
        private string legG = "";
        private string legB = "";
        private string legT = "";

        Color bodyColor;
        Color torsoColor;
        Color neckColor = Color.red;
        Color backColor = Color.red;
        Color feetColor;
        Color hairColor = new Color(.847f, .753f, .471f, 1f);
        Color handColor;
        Color legColor;

        void Start() {
            animationManager = new LPCActionAnimationManager();
            animationsLoaded = false;
            animationsLoading = false;

            player = GameObject.Find("player");
            playerController = player.GetComponent<LPCCharacterController>();

        }



        void InitializecharacterDNA() {
            string bodyKey = "body_female_light";
            string torsoKey = "chest_female_tightdress";
            string neckKey = "neck_female_capeclip";
            string backKey = "back_female_cape";
            string feetKey = "feet_female_shoes";
            string hairKey = "hair_female_shoulderr";
            string handKey = "hands_female_cloth";
            string legKey = "legs_female_pants";

            LPCCharacter.characterDNA = new LPCCharacterDNA();
            LPCCharacter.characterDNA.TorsoDNA = new LPCCharacterDNABlock(torsoKey);
            LPCCharacter.characterDNA.NeckDNA = new LPCCharacterDNABlock(neckKey, neckColor);
            LPCCharacter.characterDNA.BodyDNA = new LPCCharacterDNABlock(bodyKey);
            LPCCharacter.characterDNA.BackDNA = new LPCCharacterDNABlock(backKey, backColor);
            LPCCharacter.characterDNA.FeetDNA = new LPCCharacterDNABlock(feetKey);
            LPCCharacter.characterDNA.HairDNA = new LPCCharacterDNABlock(hairKey, hairColor);
            LPCCharacter.characterDNA.HandDNA = new LPCCharacterDNABlock(handKey);
            LPCCharacter.characterDNA.LegDNA = new LPCCharacterDNABlock(legKey);
            LPCCharacter.isDirty = true;
        }

        void OnGUI() {
            if (!animationsLoaded) {
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Caching all sprites... " + Math.Floor((LPCAtlasManager.GetModelsLoaded() / LPCAtlasManager.GetModelsTotal() * 100)) + "%");
                if (LPCAtlasManager.GetModelsLoaded() == LPCAtlasManager.GetModelsTotal()) {
                    InitializecharacterDNA();
                    animationsLoaded = true;
                    playerController.enabled = true;
                }
            } else {
                // TODO: Use more loops..
                int increaseYAmt = 25;
                int currentY = 10;

                GUI.Label(new Rect(10, currentY, 40, 20), "Body:");
                bodyKey = GUI.TextField(new Rect(50, currentY, 175, 20), bodyKey, 25);
                currentY += increaseYAmt;

                GUI.Label(new Rect(10, currentY, 40, 20), "Chest:");
                torsoKey = GUI.TextField(new Rect(50, currentY, 175, 20), torsoKey, 25);
                currentY += increaseYAmt;


                GUI.Label(new Rect(10, currentY, 40, 20), "Neck:");
                neckKey = GUI.TextField(new Rect(50, currentY, 175, 20), neckKey, 25);
                currentY += increaseYAmt;

                GUI.Label(new Rect(10, currentY, 40, 20), "Back:");
                backKey = GUI.TextField(new Rect(50, currentY, 175, 20), backKey, 25);
                currentY += increaseYAmt;

                GUI.Label(new Rect(10, currentY, 40, 20), "Feet:");
                feetKey = GUI.TextField(new Rect(50, currentY, 175, 20), feetKey, 25);
                currentY += increaseYAmt;

                GUI.Label(new Rect(10, currentY, 40, 20), "Hair:");
                hairKey = GUI.TextField(new Rect(50, currentY, 175, 20), hairKey, 25);
                currentY += increaseYAmt;

                GUI.Label(new Rect(10, currentY, 40, 20), "Hands:");
                handKey = GUI.TextField(new Rect(50, currentY, 175, 20), handKey, 25);
                currentY += increaseYAmt;

                GUI.Label(new Rect(10, currentY, 40, 20), "Legs:");
                legKey = GUI.TextField(new Rect(50, currentY, 175, 20), legKey, 25);
                currentY += increaseYAmt;

                if (GUI.Button(new Rect(10, currentY, 75, 30), "Generate")) {
                    LPCCharacterDNA characterDNA = new LPCCharacterDNA();
                    if (bodyKey.Length > 0)
                        characterDNA.BodyDNA = new LPCCharacterDNABlock(bodyKey, bodyColor);
                    if (torsoKey.Length > 0)
                        characterDNA.TorsoDNA = new LPCCharacterDNABlock(torsoKey, torsoColor);
                    if (neckKey.Length > 0)
                        characterDNA.NeckDNA = new LPCCharacterDNABlock(neckKey, neckColor);
                    if (backKey.Length > 0)
                        characterDNA.BackDNA = new LPCCharacterDNABlock(backKey, backColor);
                    if (feetKey.Length > 0)
                        characterDNA.FeetDNA = new LPCCharacterDNABlock(feetKey, feetColor);
                    if (hairKey.Length > 0)
                        characterDNA.HairDNA = new LPCCharacterDNABlock(hairKey, hairColor);
                    if (handKey.Length > 0)
                        characterDNA.HandDNA = new LPCCharacterDNABlock(handKey, handColor);
                    if (legKey.Length > 0)
                        characterDNA.LegDNA = new LPCCharacterDNABlock(legKey, legColor);
                    LPCCharacter.characterDNA = characterDNA;
                    LPCCharacter.isDirty = true;
                }

                currentY = 10;

                GUI.Label(new Rect(Screen.width - 215, currentY, 40, 20), "RGBT");
                bodyR = GUI.TextField(new Rect(Screen.width - 170, currentY, 35, 20), bodyR, 25);
                bodyG = GUI.TextField(new Rect(Screen.width - 130, currentY, 35, 20), bodyG, 25);
                bodyB = GUI.TextField(new Rect(Screen.width - 90, currentY, 35, 20), bodyB, 25);
                bodyT = GUI.TextField(new Rect(Screen.width - 50, currentY, 35, 20), bodyT, 25);
                try {
                    bodyColor = new Color(float.Parse(bodyR) / 255f, float.Parse(bodyG) / 255f, float.Parse(bodyB) / 255f, float.Parse(bodyT) / 255f);
                } catch (Exception ex) {
                    bodyColor = Color.clear;
                }
                currentY += increaseYAmt;

                GUI.Label(new Rect(Screen.width - 215, currentY, 40, 20), "RGBT");
                torsoR = GUI.TextField(new Rect(Screen.width - 170, currentY, 35, 20), torsoR, 25);
                torsoG = GUI.TextField(new Rect(Screen.width - 130, currentY, 35, 20), torsoG, 25);
                torsoB = GUI.TextField(new Rect(Screen.width - 90, currentY, 35, 20), torsoB, 25);
                torsoT = GUI.TextField(new Rect(Screen.width - 50, currentY, 35, 20), torsoT, 25);
                try {
                    torsoColor = new Color(float.Parse(torsoR) / 255f, float.Parse(torsoG) / 255f, float.Parse(torsoB) / 255f, float.Parse(torsoT) / 255f);
                } catch (Exception ex) {
                    torsoColor = Color.clear;
                }
                currentY += increaseYAmt;

                GUI.Label(new Rect(Screen.width - 215, currentY, 40, 20), "RGBT");
                neckR = GUI.TextField(new Rect(Screen.width - 170, currentY, 35, 20), neckR, 25);
                neckG = GUI.TextField(new Rect(Screen.width - 130, currentY, 35, 20), neckG, 25);
                neckB = GUI.TextField(new Rect(Screen.width - 90, currentY, 35, 20), neckB, 25);
                neckT = GUI.TextField(new Rect(Screen.width - 50, currentY, 35, 20), neckT, 25);
                try {
                    neckColor = new Color(float.Parse(neckR) / 255f, float.Parse(neckG) / 255f, float.Parse(neckB) / 255f, float.Parse(neckT) / 255f);
                } catch (Exception ex) {
                    neckColor = Color.clear;
                }
                currentY += increaseYAmt;

                GUI.Label(new Rect(Screen.width - 215, currentY, 40, 20), "RGBT");
                backR = GUI.TextField(new Rect(Screen.width - 170, currentY, 35, 20), backR, 25);
                backG = GUI.TextField(new Rect(Screen.width - 130, currentY, 35, 20), backG, 25);
                backB = GUI.TextField(new Rect(Screen.width - 90, currentY, 35, 20), backB, 25);
                backT = GUI.TextField(new Rect(Screen.width - 50, currentY, 35, 20), backT, 25);
                try {
                    backColor = new Color(float.Parse(backR) / 255f, float.Parse(backG) / 255f, float.Parse(backB) / 255f, float.Parse(backT) / 255f);
                } catch (Exception ex) {
                    backColor = Color.clear;
                }
                currentY += increaseYAmt;

                GUI.Label(new Rect(Screen.width - 215, currentY, 40, 20), "RGBT");
                feetR = GUI.TextField(new Rect(Screen.width - 170, currentY, 35, 20), feetR, 25);
                feetG = GUI.TextField(new Rect(Screen.width - 130, currentY, 35, 20), feetG, 25);
                feetB = GUI.TextField(new Rect(Screen.width - 90, currentY, 35, 20), feetB, 25);
                feetT = GUI.TextField(new Rect(Screen.width - 50, currentY, 35, 20), feetT, 25);
                try {
                    feetColor = new Color(float.Parse(feetR) / 255f, float.Parse(feetG) / 255f, float.Parse(feetB) / 255f, float.Parse(feetT) / 255f);
                } catch (Exception ex) {
                    feetColor = Color.clear;
                }
                currentY += increaseYAmt;

                GUI.Label(new Rect(Screen.width - 215, currentY, 40, 20), "RGBT");
                hairR = GUI.TextField(new Rect(Screen.width - 170, currentY, 35, 20), hairR, 25);
                hairG = GUI.TextField(new Rect(Screen.width - 130, currentY, 35, 20), hairG, 25);
                hairB = GUI.TextField(new Rect(Screen.width - 90, currentY, 35, 20), hairB, 25);
                hairT = GUI.TextField(new Rect(Screen.width - 50, currentY, 35, 20), hairT, 25);
                try {
                    hairColor = new Color(float.Parse(hairR) / 255f, float.Parse(hairG) / 255f, float.Parse(hairB) / 255f, float.Parse(hairT) / 255f);
                } catch (Exception ex) {
                    hairColor = Color.clear;
                }
                currentY += increaseYAmt;

                GUI.Label(new Rect(Screen.width - 215, currentY, 40, 20), "RGBT");
                handR = GUI.TextField(new Rect(Screen.width - 170, currentY, 35, 20), handR, 25);
                handG = GUI.TextField(new Rect(Screen.width - 130, currentY, 35, 20), handG, 25);
                handB = GUI.TextField(new Rect(Screen.width - 90, currentY, 35, 20), handB, 25);
                handT = GUI.TextField(new Rect(Screen.width - 50, currentY, 35, 20), handT, 25);
                try {
                    handColor = new Color(float.Parse(handR) / 255f, float.Parse(handG) / 255f, float.Parse(handB) / 255f, float.Parse(handT) / 255f);
                } catch (Exception ex) {
                    handColor = Color.clear;
                }
                currentY += increaseYAmt;

                GUI.Label(new Rect(Screen.width - 215, currentY, 40, 20), "RGBT");
                legR = GUI.TextField(new Rect(Screen.width - 170, currentY, 35, 20), legR, 25);
                legG = GUI.TextField(new Rect(Screen.width - 130, currentY, 35, 20), legG, 25);
                legB = GUI.TextField(new Rect(Screen.width - 90, currentY, 35, 20), legB, 25);
                legT = GUI.TextField(new Rect(Screen.width - 50, currentY, 35, 20), legT, 25);
                try {
                    legColor = new Color(float.Parse(legR) / 255f, float.Parse(legG) / 255f, float.Parse(legB) / 255f, float.Parse(legT) / 255f);
                } catch (Exception ex) {
                    legColor = Color.clear;
                }
                currentY += increaseYAmt;
                    

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
