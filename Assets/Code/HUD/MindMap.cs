using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MindMap : UIGroup
{
    [SerializeField] private Keyword m_keywordPrefab;
    [Space]
    [SerializeField] private UIFade m_mindBackground;
    [Space]
    [SerializeField] private KeywordData[] m_initialKeywords;

    private List<Keyword> m_keywords;
    private RectTransform m_keywordsContainer;

    public Action<KeywordData> OnKeywordDropped;

    public bool Active => Visible || HasKeyword;
    public bool Visible { get; set; }
    public bool HasKeyword { get; set; }
    public KeywordData SelectedKeyword { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        m_keywords = new List<Keyword>();

        m_keywordsContainer = transform.GetChild(0) as RectTransform;

    }

    public override void Start()
    {
        base.Start();

        m_mindBackground.SetOpacity(0);

        foreach (var data in m_initialKeywords)
            Fill(data);
    }

    public void Fill(KeywordData keyword)
    {
        var newGameObject = Instantiate(m_keywordPrefab.gameObject, m_keywordsContainer);
        var newKeyword = newGameObject.GetComponent<Keyword>();
        m_keywords.Add(newKeyword);

        Vector3 spawnPosition;
        spawnPosition = GetBottomLeftCorner(m_keywordsContainer) - 
            new Vector3(Random.Range(0, m_keywordsContainer.rect.x), Random.Range(0, m_keywordsContainer.rect.y));

        newKeyword.transform.position = spawnPosition;
        newKeyword.AddData(keyword);

        newKeyword.OnGrab += OnKeywordGrabbed;
        newKeyword.OnRelease += OnKeywordReleased;

    }

    /*private void Shuffle()
    {
        foreach (var keyword in m_keywords)
        {
            Vector3 spawnPosition;
            spawnPosition = GetBottomLeftCorner(m_keywordsContainer) -
                new Vector3(Random.Range(0, m_keywordsContainer.rect.x), Random.Range(0, m_keywordsContainer.rect.y));

            keyword.transform.position = spawnPosition;
        }
    }*/

    private void OnKeywordGrabbed(KeywordData data)
    {
        SelectedKeyword = data;
        HasKeyword = true;
        Empty();
    }

    private void OnKeywordReleased()
    {
        OnKeywordDropped?.Invoke(SelectedKeyword);
        SelectedKeyword = null;
        HasKeyword = false;
        //Show();
    }

    public override void Show()
    {
        base.Show();
        Visible = true;
        //Shuffle();
        m_mindBackground.Show();
        SFXManager.PlayOpenInventorySFX();
    }

    public override void Empty()
    {
        base.Empty();
        Visible = false;
        m_mindBackground.Hide();
        SFXManager.PlayCloseInventorySFX();
    }


    Vector3 GetBottomLeftCorner(RectTransform rt)
    {
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        return v[0];
    }

    public void Toggle()
    {
        if (Active)
            Empty();
        else
            Show();
    }
}
