using System;
using System.Collections.Generic;
using UnityEngine;

public class LPCAtlasManager : MonoBehaviour
{
    static LPCAtlasManager instance;

    public double ModelsLoaded = 0;
    public double ModelsTotal = 0;

    // TODO: Omg reflection plz :(
    public List<Sprite> bodySprites = new List<Sprite>();
    public List<Sprite> facialSprites = new List<Sprite>();
    public List<Sprite> hairSprites = new List<Sprite>();
    public List<Sprite> armsSprites = new List<Sprite>();
    public List<Sprite> backSprites = new List<Sprite>();
    public List<Sprite> back2Sprites = new List<Sprite>();
    public List<Sprite> chestSprites = new List<Sprite>();
    public List<Sprite> feetSprites = new List<Sprite>();
    public List<Sprite> headSprites = new List<Sprite>();
    public List<Sprite> handsSprites = new List<Sprite>();
    public List<Sprite> legsSprites = new List<Sprite>();
    public List<Sprite> neckSprites = new List<Sprite>();
    public List<Sprite> shoulderSprites = new List<Sprite>();
    public List<Sprite> waistSprites = new List<Sprite>();
    public List<Sprite> weaponsSprites = new List<Sprite>();
    public List<Sprite> wristsSprites = new List<Sprite>();

    private Dictionary<string, Sprite> bodyAtlas = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> facialAtlas = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> hairAtlas = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> armsAtlas = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> backAtlas = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> back2Atlas = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> chestAtlas = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> feetAtlas = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> headAtlas = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> handsAtlas = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> legsAtlas = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> neckAtlas = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> shoulderAtlas = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> waistAtlas = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> weaponsAtlas = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> wristsAtlas = new Dictionary<string, Sprite>();

    public Dictionary<string, Dictionary<string,Sprite>> AtlasLookup = new Dictionary<string, Dictionary<string,Sprite>>();

    public List<string> modelList = new List<string>();

    void Start()
    {
        bodySprites.ForEach(o => bodyAtlas[o.name] = o);
        facialSprites.ForEach(o => facialAtlas[o.name] = o);
        hairSprites.ForEach(o => hairAtlas[o.name] = o);
        armsSprites.ForEach(o => armsAtlas[o.name] = o);
        backSprites.ForEach(o => backAtlas[o.name] = o);
        back2Sprites.ForEach(o => back2Atlas[o.name] = o);
        chestSprites.ForEach(o => chestAtlas[o.name] = o);
        feetSprites.ForEach(o => feetAtlas[o.name] = o);
        headSprites.ForEach(o => headAtlas[o.name] = o);
        handsSprites.ForEach(o => handsAtlas[o.name] = o);
        legsSprites.ForEach(o => legsAtlas[o.name] = o);
        neckSprites.ForEach(o => neckAtlas[o.name] = o);
        shoulderSprites.ForEach(o => shoulderAtlas[o.name] = o);
        waistSprites.ForEach(o => waistAtlas[o.name] = o);
        weaponsSprites.ForEach(o => weaponsAtlas[o.name] = o);
        wristsSprites.ForEach(o => wristsAtlas[o.name] = o);

        AtlasLookup["body"] = bodyAtlas;
        AtlasLookup["facial"] = facialAtlas;
        AtlasLookup["hair"] = hairAtlas;
        AtlasLookup["arms"] = armsAtlas;
        AtlasLookup["back"] = backAtlas;
        AtlasLookup["back2"] = back2Atlas;
        AtlasLookup["chest"] = chestAtlas;
        AtlasLookup["feet"] = feetAtlas;
        AtlasLookup["head"] = headAtlas;
        AtlasLookup["hands"] = handsAtlas;
        AtlasLookup["legs"] = legsAtlas;
        AtlasLookup["neck"] = neckAtlas;
        AtlasLookup["shoulder"] = shoulderAtlas;
        AtlasLookup["waist"] = waistAtlas;
        AtlasLookup["weapons"] = weaponsAtlas;
        AtlasLookup["wrists"] = wristsAtlas;

        instance = this;
    }

    static public Sprite GetSprite(string name)
    {
        if (instance == null)
        {
            Debug.LogError("Atlas not ready yet!");
        }

        Dictionary<string, Sprite> collection = instance.AtlasLookup[name.Split('_')[0]];

        if (collection.ContainsKey(name))
        {
            return collection[name];
        } else {
            Debug.Log("MISSING SPRITE!");
            throw new Exception("MISSING SPRITE!");
        }
    }

    public static Dictionary<string, Dictionary<string,Sprite>> GetCacheLookup() {
        return instance.AtlasLookup;
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