using System;
using System.Collections.Generic;
using Assets.Scripts.Types;
using UnityEngine;
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Global

public class AtlasManager : MonoBehaviour
{
    private static AtlasManager _instance;
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

        _instance = GetComponent<AtlasManager>();
    }

    public static Sprite GetSprite(string name)
    {
        if (_instance == null)
        {
            throw new Exception("Sprites not loaded into the AtlasManager! " +
                                "Add the LPCAtlasManagerEditor script to a GameObject and click Load.");
        }

        var collection = _instance._atlasLookup[name.Split('_')[0].ToUpper()];

        if (collection.ContainsKey(name))
            return collection[name];
        Debug.Log("MISSING SPRITE!");
        throw new Exception("MISSING SPRITE!");
    }

    public static List<string> GetModelList()
    {
        return _instance.ModelList;
    }

    public static void IncrementModelLoaded()
    {
        _instance.ModelsLoaded++;
    }

    public static int GetModelsTotal()
    {
        return _instance.ModelsTotal;
    }

    public static int GetModelsLoaded()
    {
        return _instance.ModelsLoaded;
    }
}
