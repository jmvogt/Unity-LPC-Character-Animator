using Assets.Scripts.Animation;
using Assets.Scripts.Animation.ActionAnimations;
using Assets.Scripts.Character;
using Assets.Scripts.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.Scene {
    class SceneController : MonoBehaviour {

        private AnimationManager animationManager;
        private bool animationsLoading;
        private bool animationsLoaded;

        private GameObject player;
        private PlayerController playerController;

        void Start() {
            animationManager = new AnimationManager();
            animationsLoaded = false;
            animationsLoading = false;

            player = GameObject.Find("player");
            playerController = player.GetComponent<PlayerController>();
        }

        private Dictionary<string, string> modelTextLookup;
        private Dictionary<string, Color> modelColorLookup;
        private Dictionary<string, string> modelRLookup;
        private Dictionary<string, string> modelGLookup;
        private Dictionary<string, string> modelBLookup;
        private Dictionary<string, string> modelTLookup;

        void InitializeCharacter() {
            Player.characterDNA = new CharacterDNA();
            Player.animationDNA = new AnimationDNA();
        }

        void InitializeCharacterUI() {
            modelRLookup = new Dictionary<string, string>();
            modelGLookup = new Dictionary<string, string>();
            modelBLookup = new Dictionary<string, string>();
            modelTLookup = new Dictionary<string, string>();

            modelTextLookup = new Dictionary<string, string>();
            modelColorLookup = new Dictionary<string, Color>();
            foreach (string blockKey in DNABlockType.GetTypeList()) {
                modelTextLookup[blockKey] = "";
                modelColorLookup[blockKey] = Color.clear;

                modelRLookup[blockKey] = "";
                modelGLookup[blockKey] = "";
                modelBLookup[blockKey] = "";
                modelTLookup[blockKey] = "";
            }
        }

        void InitializeDemoDNA() {
            // this will populate the UI
            modelColorLookup["BACK"] = Color.red;
            modelColorLookup["NECK"] = Color.red;
            modelTextLookup["BACK"] = "back_female_cape";
            modelTextLookup["NECK"] = "neck_female_capeclip";
            modelTextLookup["CHEST"] = "chest_female_tightdress";
            modelTextLookup["FEET"] = "feet_female_shoes";
            modelTextLookup["HAIR"] = "hair_female_shoulderr";
            modelTextLookup["HANDS"] = "hands_female_cloth";
            modelTextLookup["LEGS"] = "legs_female_pants";
            modelTextLookup["BODY"] = "body_female_dark";

            // this will update the characterDNA, flagging it as dirty and causing the first frame to animate
            Player.characterDNA.UpdateBlock("NECK", modelTextLookup["NECK"], modelColorLookup["NECK"]);
            Player.characterDNA.UpdateBlock("BACK", modelTextLookup["BACK"], modelColorLookup["BACK"]);
            Player.characterDNA.UpdateBlock("CHEST", modelTextLookup["CHEST"], modelColorLookup["CHEST"]);
            Player.characterDNA.UpdateBlock("FEET", modelTextLookup["FEET"], modelColorLookup["FEET"]);
            Player.characterDNA.UpdateBlock("HAIR", modelTextLookup["HAIR"], modelColorLookup["HAIR"]);
            Player.characterDNA.UpdateBlock("HANDS", modelTextLookup["HANDS"], modelColorLookup["HANDS"]);
            Player.characterDNA.UpdateBlock("LEGS", modelTextLookup["LEGS"], modelColorLookup["LEGS"]);
            Player.characterDNA.UpdateBlock("BODY", modelTextLookup["BODY"], modelColorLookup["BODY"]);
        }

        void OnGUI() {
            if (!animationsLoaded) {
                // Loading message
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Caching all sprites... " + 
                    Math.Floor((AtlasManager.GetModelsLoaded() / AtlasManager.GetModelsTotal() * 100)) + "%");
                if (AtlasManager.GetModelsLoaded() == AtlasManager.GetModelsTotal()) {
                    // The sprites are all cached. Lets initialize the scene.
                    InitializeCharacterUI();
                    InitializeCharacter();
                    InitializeDemoDNA();
                    animationsLoaded = true;
                    playerController.enabled = true;
                }
            } else {
                int increaseYAmt = 25;
                int currentY = 35;
                int currentX = 10;

                // generate the model text boxes
                GUI.Label(new Rect(100, 10, 60, 20), "Model Key IDs");
                foreach (string blockKey in DNABlockType.GetTypeList()) {
                    GUI.Label(new Rect(currentX, currentY, 60, 20), String.Format("{0}:", blockKey.ToLower()));
                    modelTextLookup[blockKey] = GUI.TextField(new Rect(currentX+60, currentY, 175, 20), modelTextLookup[blockKey], 25);
                    currentY += increaseYAmt;
                }

                // "generate" button
                if (GUI.Button(new Rect(10, currentY, 75, 30), "Generate")) {
                    foreach (string blockKey in DNABlockType.GetTypeList()) {
                        if (modelTextLookup[blockKey].Length > 0) {
                            try { 
                                Player.characterDNA.UpdateBlock(blockKey, modelTextLookup[blockKey], modelColorLookup[blockKey]);
                            } catch (Exception ex) {
                                Debug.Log(String.Format("ERROR when importing {0} model: {1}", blockKey, ex.Message));
                            }
                        }
                    }
                }

                // generate the model color text boxes
                currentY = 35;
                GUI.Label(new Rect(Screen.width - 230, 10, 220, 20), "Model Color RGBT Values (0-255)");
                foreach (string blockKey in DNABlockType.GetTypeList()) {
                    GUI.Label(new Rect(Screen.width - 240, currentY, 60, 20), String.Format("{0}:", blockKey.ToLower()));
                    modelRLookup[blockKey] = GUI.TextField(new Rect(Screen.width - 170, currentY, 35, 20), modelRLookup[blockKey], 25);
                    modelGLookup[blockKey] = GUI.TextField(new Rect(Screen.width - 130, currentY, 35, 20), modelGLookup[blockKey], 25);
                    modelBLookup[blockKey] = GUI.TextField(new Rect(Screen.width - 90, currentY, 35, 20), modelBLookup[blockKey], 25);
                    modelTLookup[blockKey] = GUI.TextField(new Rect(Screen.width - 50, currentY, 35, 20), modelTLookup[blockKey], 25);
                    try {
                        float modelR, modelG, modelB, modelT;
                        bool rIsFloat = float.TryParse(modelRLookup[blockKey], out modelR);
                        bool gIsFloat = float.TryParse(modelGLookup[blockKey], out modelG);
                        bool bIsFloat = float.TryParse(modelBLookup[blockKey], out modelB);
                        bool tIsFloat = float.TryParse(modelTLookup[blockKey], out modelT);

                        modelColorLookup[blockKey] = new Color(
                            rIsFloat ? modelR : 0 / 255f,
                            gIsFloat ? modelG : 0 / 255f,
                            bIsFloat ? modelB : 0 / 255f, 
                            tIsFloat ? modelT : 0 / 255f
                        );
                    } catch (Exception ex) {
                        Debug.Log(String.Format("ERROR when importing {0} color: {1}", blockKey, ex.Message));

                        // dont color the armor on parsing issues
                        modelColorLookup[blockKey] = Color.clear;
                    }
                    currentY += increaseYAmt;
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
