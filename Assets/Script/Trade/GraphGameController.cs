using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GraphGameController : MonoBehaviour
{
    public Image graphImage; // UI элемент для отображения графика
    public Text correctText;
    public Text wrongText;
    public Button callButton;
    public Button inputButton;
    public Button[] BButton;

    public List<Sprite> initialGraphs; // Список начальных графиков
    public List<Sprite> callGraphs;    // Список графиков для состояния Call
    public List<Sprite> inputGraphs;   // Список графиков для состояния Input

    private int correctCount = 0;
    private int wrongCount = 0;
    private Sprite currentGraph;
    private string currentState;
    private List<Sprite> displayedInitialGraphs = new List<Sprite>();

    void Start()
    {
        callButton.onClick.AddListener(() => OnGuessButtonClicked("call"));
        inputButton.onClick.AddListener(() => OnGuessButtonClicked("input"));
        ShowNextInitialGraph();
    }

    void ShowNextInitialGraph()
    {
        // Если показаны все графики, сбросим список показанных графиков
        if (displayedInitialGraphs.Count == initialGraphs.Count)
        {
            displayedInitialGraphs.Clear();
        }

        // Выбираем случайный график, который еще не был показан
        Sprite newGraph;
        do
        {
            newGraph = initialGraphs[Random.Range(0, initialGraphs.Count)];
        } while (displayedInitialGraphs.Contains(newGraph));

        displayedInitialGraphs.Add(newGraph);
        currentGraph = newGraph;
        graphImage.sprite = newGraph;
        currentState = Random.Range(0, 2) == 0 ? "call" : "input";

        callButton.gameObject.SetActive(true);
        inputButton.gameObject.SetActive(true);
    }

    void OnGuessButtonClicked(string guess)
    {
        bool isCorrect = false;
        if ((guess == "call" && IsCorrectGraph(callGraphs)) || (guess == "input" && IsCorrectGraph(inputGraphs)))
        {
            isCorrect = true;
        }

        if (isCorrect)
        {
            correctCount++;
            print(correctCount);
            if (Localization.Lang == true)
            {
                correctText.text = "Correct: " + correctCount;
            }
            else
            {
                correctText.text = "Верно: " + correctCount;
            }
        }
        else
        {
            wrongCount++;
            print(wrongCount);
            if (Localization.Lang == true)
            {
                wrongText.text = "Wrong: " + wrongCount;
            }
            else
            {
                wrongText.text = "Неверно: " + wrongCount;
            }
        }

        DisplayNextCorrectGraph();
    }

    bool IsCorrectGraph(List<Sprite> graphList)
    {
        string currentGraphName = currentGraph.name[0].ToString();
        foreach (var graph in graphList)
        {
            if (graph.name[0].ToString() == currentGraphName)
            {
                return true;
            }
        }
        return false;
    }

    void DisplayNextCorrectGraph()
    {
        string currentGraphName = currentGraph.name[0].ToString();
        Sprite nextGraph = null;

        // Ищем правильный график в списке callGraphs
        foreach (var graph in callGraphs)
        {
            if (graph.name[0].ToString() == currentGraphName)
            {
                nextGraph = graph;
                break;
            }
        }

        // Если не нашли в callGraphs, ищем в inputGraphs
        if (nextGraph == null)
        {
            foreach (var graph in inputGraphs)
            {
                if (graph.name[0].ToString() == currentGraphName)
                {
                    nextGraph = graph;
                    break;
                }
            }
        }

        if (nextGraph != null)
        {
            currentGraph = nextGraph;
            graphImage.sprite = nextGraph;
        }

        callButton.gameObject.SetActive(false);
        inputButton.gameObject.SetActive(false);

        StartCoroutine(WaitAndShowNextInitialGraph());
    }
    
    IEnumerator WaitAndShowNextInitialGraph()
    {
        SetButtonsInteractable(false);
        yield return new WaitForSeconds(3f);
        ShowNextInitialGraph();
        SetButtonsInteractable(true);
    }
    private void SetButtonsInteractable(bool interactable)
    {
        BButton[0].enabled = interactable;
        BButton[1].enabled = interactable;
        BButton[2].enabled = interactable;
    }

    public void UpdateText()
    {
        if (Localization.Lang == true)
        {
            wrongText.text = "Wrong: " + wrongCount;
            correctText.text = "Correct: " + correctCount;
        }
        else
        {
            wrongText.text = "Неверно: " + wrongCount;
            correctText.text = "Верно: " + correctCount;
        }
    }
}