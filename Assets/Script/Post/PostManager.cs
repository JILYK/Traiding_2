using System;
using UnityEngine;
using UnityEngine.UI;

public class PostManager : MonoBehaviour
{
    public PostData postData;
    public Text textTitle1;  // Drag and drop the first Text here in the Inspector
    public Text textContent;  // Drag and drop the second Text here in the Inspector

    private string language;

    void Start()
    {
        // Проверка на назначение полей в инспекторе
        if (textTitle1 == null)
        {
            return;
        }
        if (textContent == null)
        {
            return;
        }
        if (postData == null)
        {
            return;
        }

        language = PlayerPrefs.GetString("Language", "ENG");  // Default to English if not set
        UpdateText();
    }

    public void OnPostClicked()
    {
        // Проверка на null для textContent и postData
        if (textContent == null)
        {
            return;
        }

        if (postData == null)
        {
            return;
        }

        if (Localization.Lang)
        {
            textContent.text = postData.ENG_postContent;
        }
        else
        {
            textContent.text = postData.RU_postContent;
        }
    }

    private void UpdateText()
    {
        // Проверка на null для textTitle1 и postData
        if (textTitle1 == null)
        {
            return;
        }

        if (postData == null)
        {
            return;
        }

        if (Localization.Lang)
        {
            string postTitle = postData.ENG_postTitle;
            textTitle1.text = postTitle;
        }
        else
        {
            string postTitle = postData.RU_postTitle;
            textTitle1.text = postTitle;
        }
    }

    // This method can be called to change the language dynamically
    public void SetLanguage(string newLanguage)
    {
        language = newLanguage;
        PlayerPrefs.SetString("Language", newLanguage);
        UpdateText();
    }

    private void Update()
    {
        UpdateText();
        OnPostClicked();
    }

    public void ResetOutline()
    {
        // Получаем все компоненты Outline в дочерних объектах на всех уровнях вложенности
        Outline[] outlines = GetComponentsInChildren<Outline>(true);

        // Проходимся по всем полученным компонентам и отключаем их
        foreach (Outline outline in outlines)
        {
            if (outline != null)
            {
                outline.enabled = false;
                // Добавляем отладочное сообщение для проверки доступа
            }
            else
            {
                Debug.LogWarning("Found a null Outline component reference.");
            }
        }
    }
}
