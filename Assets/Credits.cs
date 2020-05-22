using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    private GameObject m_ActivePage;

    private int m_Page;
    public int Page
    {
        get { return m_Page; }
        set
        {
            m_Page = value;
            if (Page < 0)
                Page = Pages.Length - 1;
            else if (Page > Pages.Length - 1)
                Page = 0;
            SetActivePage();
        }
    }

    public GameObject[] Pages;

    private void Start()
    {
        if (Pages.Length == 0)
            return;

        m_ActivePage = Pages[Page];
        SetActivePage();
    }

    private void Update()
    {

    }

    public void Prev()
    {
        Page--;
    }

    public void Next()
    {
        Page++;
    }

    private void SetActivePage()
    {
        m_ActivePage.SetActive(false);
        m_ActivePage = Pages[Page];
        m_ActivePage.SetActive(true);
    }
}
