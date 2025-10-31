using UnityEngine;

[CreateAssetMenu(fileName = "SFX Collection", menuName = "Scriptables/SFX/Collection", order = 1)]
public class SFXCollection : ScriptableObject
{
    [SerializeField] protected SFX[] _collection = null;
    public SFX[] SFXs => _collection;
    public SFX this[int i] => _collection[i];

    public static implicit operator SFX(SFXCollection sfx)
    {
        return sfx._collection != null && sfx._collection.Length > 0 ? sfx._collection[0] : null;
    }
}

[System.Serializable]
public class SFX
{
    public AudioClip Clip = null;
    public float Volume = 1f;
    public bool RandomizePitch = false;
}
