using System;
using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.Types;
using UnityEngine;
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Global

public class AtlasManager : MonoBehaviour
{
    public static AtlasManager Instance;
    private readonly Dictionary<string, Dictionary<string, Sprite>> _atlasLookup = new Dictionary<string, Dictionary<string, Sprite>>();
    public List<string> ModelList = new List<string>();
    public int ModelsLoaded;
    public int ModelsTotal = 0;

    [HideInInspector]
    public List<Sprite> SpriteList = new List<Sprite>();

    private void Start()
    {
        // Initialize atlas dictionaries for all block types
        foreach (var bodyKey in DNABlockType.TypeList)
        {
            _atlasLookup[bodyKey] = new Dictionary<string, Sprite>();
        }

        // Sort each sprite in the spritelist into respective dictionary
        foreach (var sprite in SpriteList)
        {
            var atlasKey = sprite.name.Split('_')[0].ToUpper();
            try
            {
                _atlasLookup[atlasKey][sprite.name] = sprite;
            }
            catch (Exception)
            {
                Debug.Log($"Failed to load sprite for atlas key; {atlasKey} sprite name {sprite.name}");
            }
        }

        Instance = GetComponent<AtlasManager>();
    }

    public Sprite GetSprite(string nameSprite)
    {
        if (Instance == null)
        {
            throw new Exception("Sprites not loaded into the AtlasManager! " +
                                "Add the LPCAtlasManagerEditor script to a GameObject and click Load.");
        }

        var collection = Instance._atlasLookup[nameSprite.Split('_')[0].ToUpper()];
        var item = collection.SafeGet(nameSprite);

        if (item == null)
        {
            Debug.Log("MISSING SPRITE!");
            throw new Exception("MISSING SPRITE!");
        }

        return collection[nameSprite];
    }

    public void IncrementModelLoaded()
    {
        Instance.ModelsLoaded++;
    }
}
