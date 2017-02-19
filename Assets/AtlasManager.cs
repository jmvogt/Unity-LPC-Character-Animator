using System.Collections.Generic;
using UnityEngine;

public class AtlasManager : MonoBehaviour
{
    static AtlasManager instance;

    public List<Sprite> tileSprites = new List<Sprite>();
    public Dictionary<string, Sprite> collection = new Dictionary<string, Sprite>();
    // Use this for initialization 
    void Start()
    {
        tileSprites.ForEach(o => collection[o.name] = o);
        instance = this;
    }

    static public Sprite GetSprite(string name)
    {
        if (instance == null)
        {
            Debug.LogError("Atlas not ready yet!");
        }

        Debug.Log("Loading Sprite from Atlas: " + name); 

        if (instance.collection.ContainsKey(name))
        {
            return instance.collection[name];
        }
        return null;
    }
}