using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Localization : MonoBehaviour
{
    public static bool Lang;
    public List<Text> EngText;
    public List<Text> RusText;

    public Sprite TSprite;
    public Sprite FSprite;
    public GameObject RUgo;
    public GameObject ENGgo;

    public static void SaveLang()
    {
        PlayerPrefs.SetInt("Lang", Lang ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Загрузка переменной Lang
    public static void LoadLang()
    {
        if (PlayerPrefs.HasKey("Lang"))
        {
            Lang = PlayerPrefs.GetInt("Lang") == 1;
        }
        else
        {
            Lang = false; // Значение по умолчанию, если ключ не найден
        }
    }

    private void Start()
    {
        LoadLang();
        // Проверка на наличие назначенных объектов
        if (RUgo == null || ENGgo == null)
        {
            Debug.LogError("RUgo or ENGgo is not assigned in the inspector");
            return;
        }
    }

    public void SwitchLanguage(int index)
    {
        switch (index)
        {
            case 0:
                Lang = false;
                SaveLang();
                break;
            case 1:
                Lang = true;
                SaveLang();
                break;
        }
    }

    private void Update()
    {
        // Проверка на наличие назначенных объектов
        if (RUgo == null || ENGgo == null)
        {
            Debug.LogError("RUgo or ENGgo is not assigned in the inspector");
            return;
        }

        if (Lang)
        {
            ENGgo.GetComponent<Image>().sprite = TSprite;
            RUgo.GetComponent<Image>().sprite = FSprite;
            for (int i = 0; i < EngText.Count; i++)
            {
                EngText[i].enabled = true;
                RusText[i].enabled = false;
            }
        }
        else
        {
            RUgo.GetComponent<Image>().sprite = TSprite;
            ENGgo.GetComponent<Image>().sprite = FSprite;
            for (int i = 0; i < EngText.Count; i++)
            {
                EngText[i].enabled = false;
                RusText[i].enabled = true;
            }
        }
    }
}
