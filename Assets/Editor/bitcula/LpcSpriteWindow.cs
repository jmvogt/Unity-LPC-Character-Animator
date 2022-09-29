using UnityEngine;
using UnityEditor;
using System.Collections;


class LpcSpriteWindow : EditorWindow
{

    private bool m_ImportEmptySprites;
    private bool m_ExpertMode;
    private int m_ColCount;
    private int m_RowCount;
    private int m_PixelsPerUnit;

    private int m_ScFrameCount;
    private int m_ThFrameCount;
    private int m_WcFrameCount;
    private int m_SlFrameCount;
    private int m_ShFrameCount;
    private int m_HuFrameCount;

    private int tab;
    
    [MenuItem("Window/Liberated Pixel Cup/Spritesheet Parser Settings")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(LpcSpriteWindow));
    }

    void OnEnable()
    {
        LoadSettings();
    }

    void OnGUI()
    {
        GUILayout.Label("LPC Spritesheet Settings", EditorStyles.boldLabel);

        string[] tabRegister;
        if (!m_ExpertMode)
        {
            tabRegister = new string[] { "Basic" };
        }
        else
        {
            tabRegister = new string[] { "Basic", "Animation", "Other" };
        }

        tab = GUILayout.Toolbar(tab, tabRegister);
        switch (tab)
        {
            case (0):
                m_ImportEmptySprites = EditorGUILayout.Toggle("Import Empty Sprites", m_ImportEmptySprites);
                m_PixelsPerUnit = EditorGUILayout.IntField("Pixels Per Unit:", m_PixelsPerUnit);
                break;

            case (1):
                m_ScFrameCount = EditorGUILayout.IntField("Spellcast Frame Count:", m_ScFrameCount);
                m_ThFrameCount = EditorGUILayout.IntField("Thrust Frame Count:", m_ThFrameCount);
                m_WcFrameCount = EditorGUILayout.IntField("Walkcycle Frame Count:", m_WcFrameCount);
                m_SlFrameCount = EditorGUILayout.IntField("Slash Frame Count", m_SlFrameCount);
                m_ShFrameCount = EditorGUILayout.IntField("Shoot Frame Count:", m_ShFrameCount);
                m_HuFrameCount = EditorGUILayout.IntField("Hurt Frame Count:", m_HuFrameCount);
                break;

            case (2):
                m_ColCount = EditorGUILayout.IntField("Total Columns:", m_ColCount);
                m_RowCount = EditorGUILayout.IntField("Total Rows:", m_RowCount);
                break;
        }

        GUILayout.FlexibleSpace();
        m_ExpertMode = EditorGUILayout.Toggle("Expert Mode", m_ExpertMode);
        if (GUILayout.Button("Restore Initial Values"))
            RestoreInitialValues();
        if (GUILayout.Button("Close"))
            Close();
    }

    void OnLostFocus()
    {
        StoreSettings();
    }

    void OnDestroy()
    {
        StoreSettings();
    }

    void RestoreInitialValues()
    {
        LpcSpriteSettings.RestoreInitialValues();
        LoadSettings();
    }

    void LoadSettings()
    {
        m_ImportEmptySprites = LpcSpriteSettings.GetImportEmptySprites();
        m_PixelsPerUnit = LpcSpriteSettings.GetPixelsPerUnit();
        m_ColCount = LpcSpriteSettings.GetColCount();
        m_RowCount = LpcSpriteSettings.GetRowCount();
        m_ScFrameCount = LpcSpriteSettings.GetScFrameCount();
        m_ThFrameCount = LpcSpriteSettings.GetThFrameCount();
        m_WcFrameCount = LpcSpriteSettings.GetWcFrameCount();
        m_SlFrameCount = LpcSpriteSettings.GetSlFrameCount();
        m_ShFrameCount = LpcSpriteSettings.GetShFrameCount();
        m_HuFrameCount = LpcSpriteSettings.GetHuFrameCount();
        m_ExpertMode = LpcSpriteSettings.GetExpertMode();
    }

    void StoreSettings()
    {
        LpcSpriteSettings.SetImportEmptySprites(m_ImportEmptySprites);
        LpcSpriteSettings.SetPixelsPerUnit(m_PixelsPerUnit);
        LpcSpriteSettings.SetColCount(m_ColCount);
        LpcSpriteSettings.SetRowCount(m_RowCount);
        LpcSpriteSettings.SetScFrameCount(m_ScFrameCount);
        LpcSpriteSettings.SetThFrameCount(m_ThFrameCount);
        LpcSpriteSettings.SetWcFrameCount(m_WcFrameCount);
        LpcSpriteSettings.SetSlFrameCount(m_SlFrameCount);
        LpcSpriteSettings.SetShFrameCount(m_ShFrameCount);
        LpcSpriteSettings.SetHuFrameCount(m_HuFrameCount);
    }
}