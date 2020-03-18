using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteDatabase : MonoBehaviour {

    [System.Serializable]
    private struct CharacterNameAndItsSprites
    {
        public CharacterName Character;

        public Sprite SpriteNW;
        public Sprite SpriteNE;
        public Sprite SpriteSE;
        public Sprite SpriteSW;
    }

    [SerializeField]
    private List<CharacterNameAndItsSprites> characterNameAndItsSprites;

    private static readonly object simpleLock = new object();
    private static CharacterSpriteDatabase instance;
    public static CharacterSpriteDatabase Instance
    {
        get
        {
            lock (simpleLock)
            {
                if (instance == null)
                {
                    // Search for existing instance
                    instance = FindObjectOfType<CharacterSpriteDatabase>();

                    if (instance == null)
                        Debug.Log("Não há " + typeof(CharacterSpriteDatabase) + " nesta cena!");
                    else
                        DontDestroyOnLoad(instance.gameObject);
                }
                return instance;
            }
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static Sprite SpriteNW(CharacterName character)
    {
        var list = Instance.characterNameAndItsSprites;
        var element = list.Find(x => x.Character == character);
        return element.SpriteNW;
    }

    public static Sprite SpriteNE(CharacterName character)
    {
        var list = Instance.characterNameAndItsSprites;
        var element = list.Find(x => x.Character == character);
        return element.SpriteNE;
    }

    public static Sprite SpriteSE(CharacterName character)
    {
        var list = Instance.characterNameAndItsSprites;
        var element = list.Find(x => x.Character == character);
        return element.SpriteSE;
    }

    public static Sprite SpriteSW(CharacterName character)
    {
        var list = Instance.characterNameAndItsSprites;
        var element = list.Find(x => x.Character == character);
        return element.SpriteSW;
    }
}
