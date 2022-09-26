using System;
using System.Collections.Generic;
using System.Threading;
using Assets.Scripts.Animation;
using Assets.Scripts.Character;
using Assets.Scripts.Types;
using UnityEngine;

namespace Assets.Scripts.Scene
{
    internal class SceneController : MonoBehaviour
    {
        private AnimationManager _animationManager;
        private bool _animationsLoaded;
        private bool _animationsLoading;
        private Dictionary<string, string> _modelBLookup;
        private Dictionary<string, Color> _modelColorLookup;
        private Dictionary<string, string> _modelGLookup;
        private Dictionary<string, string> _modelRLookup;
        private Dictionary<string, string> _modelTextLookup;
        private GameObject _player;
        private PlayerController _playerController;

        private void Start()
        {
            _animationManager = new AnimationManager();
            _animationsLoaded = false;
            _animationsLoading = false;

            _player = GameObject.Find("player");
            _playerController = _player.GetComponent<PlayerController>();
        }

        //private Dictionary<string, string> modelTLookup;

        private void InitializeCharacter()
        {
            Player.CharacterDNA = new CharacterDNA();
            Player.AnimationDNA = new AnimationDNA();
        }

        private void InitializeCharacterUI()
        {
            _modelRLookup = new Dictionary<string, string>();
            _modelGLookup = new Dictionary<string, string>();
            _modelBLookup = new Dictionary<string, string>();

            //modelTLookup = new Dictionary<string, string>();

            _modelTextLookup = new Dictionary<string, string>();
            _modelColorLookup = new Dictionary<string, Color>();
            foreach (var blockKey in DNABlockType.TypeList)
            {
                _modelTextLookup[blockKey] = "";
                _modelColorLookup[blockKey] = Color.clear;

                _modelRLookup[blockKey] = "";
                _modelGLookup[blockKey] = "";
                _modelBLookup[blockKey] = "";

                //modelTLookup[blockKey] = "";
            }
        }

        private void InitializeDemoDNA()
        {
            // this will populate the UI
            _modelColorLookup[DNABlockType.Back] = Color.red;
            _modelColorLookup[DNABlockType.Neck] = Color.red;
            _modelTextLookup[DNABlockType.Back] = "back_female_cape";
            _modelTextLookup[DNABlockType.Neck] = "neck_female_capeclip";
            _modelTextLookup[DNABlockType.Chest] = "chest_female_tightdress";
            _modelTextLookup[DNABlockType.Feet] = "feet_female_shoes";
            _modelTextLookup[DNABlockType.Hair] = "hair_female_shoulderr";
            _modelTextLookup[DNABlockType.Hands] = "hands_female_cloth";
            _modelTextLookup[DNABlockType.Legs] = "legs_female_pants";
            _modelTextLookup[DNABlockType.Body] = "body_female_human_dark";

            // this will update the characterDNA, flagging it as dirty and causing the first frame to animate
            Player.CharacterDNA.UpdateBlock(DNABlockType.Neck, _modelTextLookup[DNABlockType.Neck], _modelColorLookup[DNABlockType.Neck]);
            Player.CharacterDNA.UpdateBlock(DNABlockType.Back, _modelTextLookup[DNABlockType.Back], _modelColorLookup[DNABlockType.Back]);
            Player.CharacterDNA.UpdateBlock(DNABlockType.Chest, _modelTextLookup[DNABlockType.Chest], _modelColorLookup[DNABlockType.Chest]);
            Player.CharacterDNA.UpdateBlock(DNABlockType.Feet, _modelTextLookup[DNABlockType.Feet], _modelColorLookup[DNABlockType.Feet]);
            Player.CharacterDNA.UpdateBlock(DNABlockType.Hair, _modelTextLookup[DNABlockType.Hair], _modelColorLookup[DNABlockType.Hair]);
            Player.CharacterDNA.UpdateBlock(DNABlockType.Hands, _modelTextLookup[DNABlockType.Hands], _modelColorLookup[DNABlockType.Hands]);
            Player.CharacterDNA.UpdateBlock(DNABlockType.Legs, _modelTextLookup[DNABlockType.Legs], _modelColorLookup[DNABlockType.Legs]);
            Player.CharacterDNA.UpdateBlock(DNABlockType.Body, _modelTextLookup[DNABlockType.Body], _modelColorLookup[DNABlockType.Body]);
        }

        private void OnGUI()
        {
            if (!_animationsLoaded)
            {
                var modelsLoaded = AtlasManager.Instance.ModelsLoaded;
                var modelsTotal = AtlasManager.Instance.ModelsTotal;
                // Loading message
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Caching all sprites... " + Math.Floor((double)modelsLoaded / modelsTotal * 100) + "%");
                if (modelsLoaded == modelsTotal) return;

                // The sprites are all cached. Lets initialize the scene.
                InitializeCharacterUI();
                InitializeCharacter();
                InitializeDemoDNA();
                _animationsLoaded = true;
                _playerController.enabled = true;
            }
            else
            {
                var increaseYAmt = 25;
                var currentY = 35;
                var currentX = 10;

                // generate the model text boxes
                GUI.Label(new Rect(100, 10, 60, 20), "Model Key IDs");
                foreach (var blockKey in DNABlockType.TypeList)
                {
                    GUI.Label(new Rect(currentX, currentY, 60, 20), $"{blockKey.ToLower()}:");
                    _modelTextLookup[blockKey] = GUI.TextField(new Rect(currentX + 60, currentY, 175, 20), _modelTextLookup[blockKey], 25);
                    currentY += increaseYAmt;
                }

                // "generate" button
                if (GUI.Button(new Rect(10, currentY, 75, 30), "Generate"))
                {
                    foreach (var blockKey in DNABlockType.TypeList)
                    {
                        try
                        {
                            Player.CharacterDNA.UpdateBlock(blockKey, _modelTextLookup[blockKey], _modelColorLookup[blockKey]);
                        }
                        catch (Exception ex)
                        {
                            Debug.Log($"ERROR when importing {blockKey} model: {ex.Message}");
                        }
                    }
                }

                // generate the model color text boxes
                currentY = 35;
                GUI.Label(new Rect(Screen.width - 230, 10, 220, 20), "Model Color RGB Values (0-255)");
                foreach (var blockKey in DNABlockType.TypeList)
                {
                    GUI.Label(new Rect(Screen.width - 240, currentY, 60, 20), $"{blockKey.ToLower()}:");
                    _modelRLookup[blockKey] = GUI.TextField(new Rect(Screen.width - 130, currentY, 35, 20), _modelRLookup[blockKey], 25);
                    _modelGLookup[blockKey] = GUI.TextField(new Rect(Screen.width - 90, currentY, 35, 20), _modelGLookup[blockKey], 25);
                    _modelBLookup[blockKey] = GUI.TextField(new Rect(Screen.width - 50, currentY, 35, 20), _modelBLookup[blockKey], 25);

                    //modelTLookup[blockKey] = GUI.TextField(new Rect(Screen.width - 50, currentY, 35, 20), modelTLookup[blockKey], 25);
                    try
                    {
                        float modelR, modelG, modelB; //, modelT;
                        var rIsFloat = float.TryParse(_modelRLookup[blockKey], out modelR);
                        var gIsFloat = float.TryParse(_modelGLookup[blockKey], out modelG);
                        var bIsFloat = float.TryParse(_modelBLookup[blockKey], out modelB);

                        //bool tIsFloat = float.TryParse(modelTLookup[blockKey], out modelT);

                        _modelColorLookup[blockKey] = new Color(
                            rIsFloat ? modelR / 255f : 0,
                            gIsFloat ? modelG / 255f : 0,
                            bIsFloat ? modelB / 255f : 0,
                            rIsFloat || gIsFloat || bIsFloat ? .85f : 0
                        );
                    }
                    catch (Exception ex)
                    {
                        Debug.Log($"ERROR when importing {blockKey} color: {ex.Message}");

                        // dont color the armor on parsing issues
                        _modelColorLookup[blockKey] = Color.clear;
                    }

                    currentY += increaseYAmt;
                }

                GUI.Label(new Rect(Screen.width - 115, currentY, 30, 20), "R");
                GUI.Label(new Rect(Screen.width - 80, currentY, 30, 20), "G");
                GUI.Label(new Rect(Screen.width - 40, currentY, 30, 20), "B");
            }
        }

        private void Update()
        {
            if (!_animationsLoading)
            {
                _animationsLoading = true;
                var thread = new Thread(_animationManager.LoadAllAnimationsIntoCache);
                thread.Start();
            }
        }
    }
}
