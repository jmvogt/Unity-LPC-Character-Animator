﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class AtlasManager : MonoBehaviour
{
    static AtlasManager instance;

    public double ModelsLoaded = 0;
    public double ModelsTotal = 0;

    // TODO: Omg reflection plz :(
    public List<Sprite> bodySprites = new List<Sprite>();
    public List<Sprite> facialhairSprites = new List<Sprite>();
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
    public List<Sprite> wristsSprites = new List<Sprite>();

    public List<Sprite> earsSprites = new List<Sprite>();
    public List<Sprite> eyesSprites = new List<Sprite>();
    public List<Sprite> noseSprites = new List<Sprite>();
    public List<Sprite> lefthandSprites = new List<Sprite>();
    public List<Sprite> righthandSprites = new List<Sprite>();

    private Dictionary<string, Sprite> bodyAtlas = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> facialhairAtlas = new Dictionary<string, Sprite>();
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
    private Dictionary<string, Sprite> wristsAtlas = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> earsAtlas = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> eyesAtlas = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> noseAtlas = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> lefthandAtlas = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> righthandAtlas = new Dictionary<string, Sprite>();

    public Dictionary<string, Dictionary<string,Sprite>> AtlasLookup = new Dictionary<string, Dictionary<string,Sprite>>();

    public List<string> modelList = new List<string>();

    void Start()
    {
        bodySprites.ForEach(o => bodyAtlas[o.name] = o);
        facialhairSprites.ForEach(o => facialhairAtlas[o.name] = o);
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
        wristsSprites.ForEach(o => wristsAtlas[o.name] = o);
        lefthandSprites.ForEach(o => lefthandAtlas[o.name] = o);
        righthandSprites.ForEach(o => righthandAtlas[o.name] = o);
        eyesSprites.ForEach(o => eyesAtlas[o.name] = o);
        noseSprites.ForEach(o => noseAtlas[o.name] = o);
        earsSprites.ForEach(o => earsAtlas[o.name] = o);

        AtlasLookup["body"] = bodyAtlas;
        AtlasLookup["facialhair"] = facialhairAtlas;
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
        AtlasLookup["wrists"] = wristsAtlas;
        AtlasLookup["eyes"] = eyesAtlas;
        AtlasLookup["nose"] = noseAtlas;
        AtlasLookup["ears"] = earsAtlas;
        AtlasLookup["lefthand"] = lefthandAtlas;
        AtlasLookup["righthand"] = righthandAtlas;

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