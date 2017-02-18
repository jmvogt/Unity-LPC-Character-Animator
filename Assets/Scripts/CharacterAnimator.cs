using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterAnimator : MonoBehaviour
{
    Dictionary<string, Sprite> cachedSprites;
    Dictionary<string, List<Sprite>> cachedAnimationStrip;
    public bool stopOnFinalFrame;
    
    public enum Action
    {
        IDLE,
        DEAD,
        SPELLCAST_LEFT,
        SPELLCAST_RIGHT,
        SPELLCAST_TOP,
        SPELLCAST_DOWN,
        SHOOT_LEFT,
        SHOOT_RIGHT,
        SHOOT_TOP,
        SHOOT_DOWN,
        SLASH_LEFT,
        SLASH_RIGHT,
        SLASH_TOP,
        SLASH_DOWN,
        THRUST_LEFT,
        THRUST_RIGHT,
        THRUST_TOP,
        THRUST_DOWN,
        WALK_LEFT,
        WALK_RIGHT,
        WALK_TOP,
        WALK_DOWN
    }

    private Dictionary<Action, AnimationActionDefition> actionDefinitions;

    public CharacterAnimator()
    {
        actionDefinitions = new Dictionary<Action, AnimationActionDefition>();

        AnimationActionDefition hurtAction = new AnimationActionDefition("hu_0", 6, 0, true);
        actionDefinitions.Add(Action.DEAD, hurtAction);

        AnimationActionDefition spellcastDownAction = new AnimationActionDefition("sc_d", 7, 6, true);
        AnimationActionDefition spellcastLeftAction = new AnimationActionDefition("sc_l", 7, 13, true);
        AnimationActionDefition spellcastRightAction = new AnimationActionDefition("sc_r", 7, 20, true);
        AnimationActionDefition spellcastTopAction = new AnimationActionDefition("sc_t", 7, 27, true);

        actionDefinitions.Add(Action.SPELLCAST_DOWN, spellcastDownAction);
        actionDefinitions.Add(Action.SPELLCAST_LEFT, spellcastLeftAction);
        actionDefinitions.Add(Action.SPELLCAST_RIGHT, spellcastRightAction);
        actionDefinitions.Add(Action.SPELLCAST_TOP, spellcastTopAction);

        AnimationActionDefition shootDownAction = new AnimationActionDefition("sh_d", 13, 34, false);
        AnimationActionDefition shootLeftAction = new AnimationActionDefition("sh_l", 13, 47, false);
        AnimationActionDefition shootRightAction = new AnimationActionDefition("sh_r", 13, 60, false);
        AnimationActionDefition shootTopAction = new AnimationActionDefition("sh_t", 13, 73, false);

        actionDefinitions.Add(Action.SHOOT_DOWN, shootDownAction);
        actionDefinitions.Add(Action.SHOOT_LEFT, shootLeftAction);
        actionDefinitions.Add(Action.SHOOT_RIGHT, shootRightAction);
        actionDefinitions.Add(Action.SHOOT_TOP, shootTopAction);

        AnimationActionDefition slashDownAction = new AnimationActionDefition("sl_d", 6, 86, false);
        AnimationActionDefition slashLeftAction = new AnimationActionDefition("sl_l", 6, 92, false);
        AnimationActionDefition slashRightAction = new AnimationActionDefition("sl_r", 6, 98, false);
        AnimationActionDefition slashTopAction = new AnimationActionDefition("sl_t", 6, 104, false);

        actionDefinitions.Add(Action.SLASH_DOWN, slashDownAction);
        actionDefinitions.Add(Action.SLASH_LEFT, slashLeftAction);
        actionDefinitions.Add(Action.SLASH_RIGHT, slashRightAction);
        actionDefinitions.Add(Action.SLASH_TOP, slashTopAction);

        AnimationActionDefition thrustDownAction = new AnimationActionDefition("th_d", 8, 110, true);
        AnimationActionDefition thrustLeftAction = new AnimationActionDefition("th_l", 8, 118, true);
        AnimationActionDefition thrustRightAction = new AnimationActionDefition("th_r", 8, 126, true);
        AnimationActionDefition thrustTopAction = new AnimationActionDefition("th_t", 8, 134, true);

        actionDefinitions.Add(Action.THRUST_DOWN, thrustDownAction);
        actionDefinitions.Add(Action.THRUST_LEFT, thrustLeftAction);
        actionDefinitions.Add(Action.THRUST_RIGHT, thrustRightAction);
        actionDefinitions.Add(Action.THRUST_TOP, thrustTopAction);

        AnimationActionDefition walkDownAction = new AnimationActionDefition("wc_d", 9, 142, false);
        AnimationActionDefition walkLeftAction = new AnimationActionDefition("wc_l", 9, 151, false);
        AnimationActionDefition walkRightAction = new AnimationActionDefition("wc_r", 9, 160, false);
        AnimationActionDefition walkTopAction = new AnimationActionDefition("wc_t", 9, 169, false);

        actionDefinitions.Add(Action.WALK_DOWN, walkDownAction);
        actionDefinitions.Add(Action.WALK_LEFT, walkLeftAction);
        actionDefinitions.Add(Action.WALK_RIGHT, walkRightAction);
        actionDefinitions.Add(Action.WALK_TOP, walkTopAction);

        _torsoSprites = new List<Sprite>();
        _bodySprites = new List<Sprite>();
        cachedAnimationStrip = new Dictionary<string, List<Sprite>>();
    }

    private class AnimationActionDefition {
        public string actionName;
        public int numberOfFrames;
        public int spriteStartIndex;
        public bool stopOnFinalFrame;

        public AnimationActionDefition(string actionName, int numberOfFrames, int spriteStartIndex, bool stopOnFinalFrame)
        {
            this.actionName = actionName;
            this.numberOfFrames = numberOfFrames;
            this.spriteStartIndex = spriteStartIndex;
            this.stopOnFinalFrame = stopOnFinalFrame;
        }
    }

    public void ClearAction()
    {
        //stops the current action from animating
        _torsoSprites.Clear();
        _bodySprites.Clear();
        _currentAction = Action.IDLE;
    }

    public void LoadAction(Action ACTION, string bodyName, string torsoName, SpriteRenderer bodyRenderer, SpriteRenderer torsoRenderer)
    {
        if (ACTION == _currentAction)
            return;
            //ClearAction();
            _currentNumber = 0;
            torso = torsoRenderer;
            body = bodyRenderer;

            _totalAnimTimeInSeconds = .4f;
            AnimationActionDefition actionDefinition = actionDefinitions[ACTION];

            stopOnFinalFrame = false;
            playing = true;

            string torsoKey = torsoName + "_" + actionDefinition.actionName;
            string bodyKey = bodyName + "_" + actionDefinition.actionName;

            if (cachedAnimationStrip.ContainsKey(bodyKey))
            {
                _bodySprites = cachedAnimationStrip[bodyKey];
                _torsoSprites = cachedAnimationStrip[torsoKey];

                Debug.Log("BODY SPRITE COUNT: " + _bodySprites.Count);
            }
            else
            {
                List<Sprite> newTorsoSprites = new List<Sprite>();
                List<Sprite> newBodySprites = new List<Sprite>();

                for (int i = 0; i < actionDefinition.numberOfFrames; i++)
                {
                    string currentTorsoKey = torsoName + "_" + actionDefinition.actionName + "_" + i;
                    string currentBodyKey = bodyName + "_" + actionDefinition.actionName + "_" + i;

                    Sprite torsoSprite = AtlasManager.GetSprite(currentTorsoKey);
                    Sprite bodySprite = AtlasManager.GetSprite(currentBodyKey);

                    newTorsoSprites.Add(torsoSprite);
                    newBodySprites.Add(bodySprite);

                }
                cachedAnimationStrip.Add(torsoKey, newTorsoSprites);
                cachedAnimationStrip.Add(bodyKey, newBodySprites);
                _torsoSprites = newTorsoSprites;
                _bodySprites = newBodySprites;

            }
            _numberOfFrames = actionDefinition.numberOfFrames;
            _currentAction = ACTION;
        //}
    }

    public Action _currentAction = Action.IDLE;
    public float _totalAnimTimeInSeconds = 0;
    private float _passedTime = 0;
    private int _currentNumber = 0;
    private int _numberOfFrames = 0;

    public List<Sprite> _torsoSprites;
    public List<Sprite> _bodySprites;

    public SpriteRenderer torso;
    public SpriteRenderer body;

    bool playing = false;

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            int currentFrameIndex = _currentNumber % _numberOfFrames;
            Debug.Log("CURRENT FRAME INDEX! " + currentFrameIndex);
            Debug.Log("STOP ON FINAL?: " + stopOnFinalFrame);

            _currentNumber++;

            
            if (stopOnFinalFrame && _currentNumber % _numberOfFrames == 0)
            {
                _currentAction = Action.IDLE;
                playing = false;
                torso.sprite = _torsoSprites[0];
                body.sprite = _bodySprites[0];
                return;
            }

            float singleAnimTime = _totalAnimTimeInSeconds / _bodySprites.Count;
            if (_passedTime >= singleAnimTime)
            {
                torso.sprite = _torsoSprites[currentFrameIndex];
                body.sprite = _bodySprites[currentFrameIndex];
                torso.sortingOrder = 5;
                torso.sortingLayerName = "Units"; 
                _passedTime -= singleAnimTime;
            }

            _passedTime += Time.deltaTime;
        }
    }
}