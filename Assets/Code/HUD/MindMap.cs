using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MindMap : UIGroup
{
    [SerializeField] private Keyword m_keywordPrefab;
    [Space]
    [SerializeField] private KeywordData[] m_initialKeywords;

    private List<Keyword> m_keywords;
    private RectTransform m_keywordsContainer;

    public bool Active { get; set; }

    private enum MapState { Grab, Drag }
    private MapState m_mapState;


    protected override void Awake()
    {
        base.Awake();

        m_keywords = new List<Keyword>();

        m_keywordsContainer = transform.GetChild(0) as RectTransform;
    }

    public override void Start()
    {
        base.Start();

        foreach (var data in m_initialKeywords)
            Fill(data);
    }

    public void Fill(KeywordData keyword)
    {
        var newGameObject = Instantiate(m_keywordPrefab.gameObject, m_keywordsContainer);
        var newKeyword = newGameObject.GetComponent<Keyword>();
        m_keywords.Add(newKeyword);

        Vector3 spawnPosition;// = new Vector3(Random.Range(-500, 500), Random.Range(-375, 375));
        spawnPosition = GetBottomLeftCorner(m_keywordsContainer) - 
            new Vector3(Random.Range(0, m_keywordsContainer.rect.x), Random.Range(0, m_keywordsContainer.rect.y));

        newKeyword.transform.position = spawnPosition;
        newKeyword.AddData(keyword);

        newKeyword.OnGrab += OnKeywordGrabbed;
        newKeyword.OnRelease += OnKeywordReleased;

    }

    private void OnKeywordGrabbed()
    {
        Empty();
    }

    private void OnKeywordReleased()
    {
        //Show();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Show();
        }
    }

    public override void Show()
    {
        base.Show();
    }

    public override void Empty()
    {
        base.Empty();
    }

    Vector3 GetBottomLeftCorner(RectTransform rt)
    {
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        return v[0];
    }
}
