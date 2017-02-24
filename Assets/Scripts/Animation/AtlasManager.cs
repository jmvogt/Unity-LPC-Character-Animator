using Assets.Scripts.Types;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AtlasManager : MonoBehaviour
{
    static AtlasManager instance;

    public double SpritesLoaded = 0;
    public double SpritesTotal = 0;
    public double ModelsLoaded = 0;
    public double ModelsTotal = 0;

    [HideInInspector]
    public List<Sprite> spriteList = new List<Sprite>();

    public Dictionary<string, Dictionary<string,Sprite>> AtlasLookup = new Dictionary<string, Dictionary<string,Sprite>>();

    public List<string> modelList = new List<string>();

    void Start()
    {
        // Initialize atlas dictionaries for all block types
        foreach (string bodyKey in DNABlockType.GetTypeList()) {
            AtlasLookup[bodyKey] = new Dictionary<string, Sprite>();
        }

        // Sort each sprite in the spritelist into respective dictionary
        foreach (Sprite sprite in spriteList) {
            string atlasKey = sprite.name.Split('_')[0].ToUpper();
            AtlasLookup[atlasKey][sprite.name] = sprite;
        }

        instance = this;
    }

    static public Sprite GetSprite(string name)
    {
        if (instance == null) {
            throw new Exception("Sprites not loaded into the AtlasManager! " +
                "Add the LPCAtlasManagerEditor script to a GameObject and click Load.");
        }

        Dictionary<string, Sprite> collection = instance.AtlasLookup[name.Split('_')[0].ToUpper()];

        if (collection.ContainsKey(name))
        {
            return collection[name];
        } else {
            Debug.Log("MISSING SPRITE!");
            throw new Exception("MISSING SPRITE!");
        }
    }

    public static List<string> GetModelList() {
        return instance.modelList;
    }

    public static void IncrementModelLoaded() {
        instance.ModelsLoaded++;
    }

    public static double GetModelsTotal() {
        return instance.ModelsTotal;
    }

    public static double GetModelsLoaded() {
        return instance.ModelsLoaded;
    }
}