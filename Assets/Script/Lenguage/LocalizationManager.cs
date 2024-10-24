using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LocalizationManager : MonoBehaviour
{
    public Text[] textObjectsToTranslate; // Массив текстовых объектов для локализации
    private string language = "";

    public Text ThreeText;

    void Start()
    {
        // Получаем текущий язык устройства
        language = GetDeviceLanguage();
        // language = "ENG";

        switch (language)
        {
            case "Russian":
                language = "RU";
                break;
            case "English":
                language = "ENG";
                break;
            case "Brazilian":
                language = "BR";
                break;
            default:
                language = "ENG";
                break;
        }

        print(language);
        // Получаем данные для локализации из статического конфига
        Dictionary<string, string> localizationData = LocalizationConfig.GetLocalizationData(language);

        // Проходимся по всем текстовым объектам и устанавливаем им локализованный текст
        foreach (Text textObject in textObjectsToTranslate)
        {
            if (textObject == null)
            {
                
                continue;
            }
            if (localizationData.ContainsKey(textObject.name))
            {
                textObject.text = localizationData[textObject.name];
            }
        }

        ToMenuLeng();
    }

    public void ToMenuLeng()
    {
        if (ThreeText == null)
        {
            
            return;
        }

        switch (language)
        {
            case "RU":
                ThreeText.text = "ВСЕ ЖУРНАЛЫ";
                break;
            case "ENG":
                ThreeText.text = "ALL MAGAZINES";
                break;
            case "BR":
                ThreeText.text = "TODAS AS REVISTAS";
                break;
            default:
                ThreeText.text = "ALL MAGAZINES";
                break;
        }
    }

    public void ToResultLeng()
    {
        if (ThreeText == null)
        {
            
            return;
        }

        switch (language)
        {
            case "RU":
                ThreeText.text = "РЕЗУЛЬТАТ";
                break;
            case "ENG":
                ThreeText.text = "RESULT";
                break;
            case "BR":
                ThreeText.text = "RESULTADO";
                break;
            default:
                ThreeText.text = "RESULT";
                break;
        }
    }

    public void ToFavoritLeng()
    {
        if (ThreeText == null)
        {
            
            return;
        }

        switch (language)
        {
            case "RU":
                ThreeText.text = "ИЗБРАННОЕ";
                break;
            case "ENG":
                ThreeText.text = "FAVORITES";
                break;
            case "BR":
                ThreeText.text = "FAVORITOS";
                break;
            default:
                ThreeText.text = "UNKNOWN";
                break;
        }
    }

    string GetDeviceLanguage()
    {
        // Возвращаем язык устройства
        return Application.systemLanguage.ToString();
    }
}