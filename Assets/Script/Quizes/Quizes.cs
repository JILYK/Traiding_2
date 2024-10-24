using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quizes : MonoBehaviour
{
    public GameObject[] questions; // массив вопросов
    public Button[] BButton;
    public GameObject[] result;
    public Button retryButton; // кнопка для перепрохождения теста
    public Text resultText; // текстовый элемент для вывода результата
    public GameObject resultPanel; // объект, который активируется в конце теста
    private int currentQuestionIndex = 0;
    private int correctAnswers = 0;
    private const int totalQuestions = 3;
    private const int requiredCorrectAnswers = 5;
    private int clickCount = 0; // количество нажатий
    public Outline outline;
    public GameObject Complete;

    
  
    private float rCorrect = 0;
    private float gCorrect = 128;
    private float bCorrect = 0;

    private float rIncorrect = 128;
    private float gIncorrect = 0;
    private float bIncorrect = 0;
    void Start()
    {
        retryButton.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);
        resultPanel.SetActive(false);
        ShowQuestion(currentQuestionIndex);
    }

    void ShowQuestion(int index)
    {
        // Скрываем все вопросы
        foreach (var question in questions)
        {
            question.SetActive(false);
        }

        // Показываем текущий вопрос
        if (index < questions.Length)
        {
            questions[index].SetActive(true);
        }
        else
        {
            // Все вопросы завершены
            PlayerPrefs.SetInt("TestPassed", 1);
            ShowResult();
        }
    }

    public void OnAnswerSelected(bool isCorrect)
    {
        result[currentQuestionIndex].SetActive(true);

        if (isCorrect)
        {
            correctAnswers++;
        }

        StartCoroutine(SwitchQuestionAfterDelay());
    }
    private IEnumerator SwitchQuestionAfterDelay()
    {
        SetButtonsInteractable(false);
        yield return new WaitForSeconds(3f);

        SetButtonsInteractable(true);
        result[currentQuestionIndex].SetActive(false);
        currentQuestionIndex++;
        print(currentQuestionIndex);
        ShowQuestion(currentQuestionIndex);
    }
    private void SetButtonsInteractable(bool interactable)
    {
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            button.enabled = interactable;
            BButton[0].enabled = interactable;
            BButton[1].enabled = interactable;
            BButton[2].enabled = interactable;
        }
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
                Debug.Log($"Outline component disabled on GameObject: {outline.gameObject.name}");
            }
            else
            {
                Debug.LogWarning("Found a null Outline component reference.");
            }
        }
    }

    public GameObject Goodpanel;
    public GameObject Badpanel;
    void ShowResult()
    {
        
        resultPanel.SetActive(true);
        resultText.gameObject.SetActive(true);
        resultText.text = correctAnswers + " / 10" ;
        
        if (correctAnswers < requiredCorrectAnswers)
        {
            Complete.SetActive(false);
            var color = new Color(rIncorrect / 255f, gIncorrect / 255f, bIncorrect / 255f);
            outline.effectColor = color;
            outline.enabled = true;
            Goodpanel.SetActive(false);
            Badpanel.SetActive(true);
            retryButton.gameObject.SetActive(true);
        }
        else
        {
            var color = new Color(rCorrect / 255f, gCorrect / 255f, bCorrect / 255f);
            outline.effectColor = color;
            outline.enabled = true;
            Complete.SetActive(true);
            Goodpanel.SetActive(true);
            Badpanel.SetActive(false);
        }
        
    }

    public void Retry()
    {
        currentQuestionIndex = 0;
        correctAnswers = 0;
        retryButton.gameObject.SetActive(true);
        resultText.gameObject.SetActive(true);
        resultPanel.SetActive(false);
        ShowQuestion(currentQuestionIndex);
    }

    public void Null()
    {
        currentQuestionIndex = 0;
        correctAnswers = 0;
    }
}
