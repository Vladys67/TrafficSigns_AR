using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public GameObject quizPanel;
    public GameObject resultPanel;
    public GameObject studyButton;
    public GameObject startQuizButton;

    public StudyModeManager studyModeManager;

    public TMP_Text questionText;
    public TMP_Text questionCounter;
    public TMP_Text feedbackText;
    public TMP_Text scoreText;

    public Button[] answerButtons;
    public Button nextButton;

    [Header("Setări Quiz Random")]
    public int questionsPerQuiz = 7;

    private struct QuestionItem
    {
        public string question;
        public string[] options;
        public int correctIndex;

        public QuestionItem(string q, string a, string b, string c, int correct)
        {
            question = q;
            options = new string[] { a, b, c };
            correctIndex = correct;
        }
    }

    // Banca completa de 25 de intrebari (cele 7 initiale + 18 suplimentare despre indicatoare)
    private readonly List<QuestionItem> allQuestions = new List<QuestionItem>
    {
        // --- Cele 7 intrebari initiale ---
        new QuestionItem("Ce semnifică indicatorul STOP?", "Oprire obligatorie", "Parcare permisă", "Drum cu prioritate", 0),
        new QuestionItem("Ce indică Trecere pentru pietoni?", "Loc de traversare", "Acces interzis", "Parcare", 0),
        new QuestionItem("Ce semnifică indicatorul Parcare?", "Parcare permisă", "Oprire obligatorie", "Viteză maximă", 0),
        new QuestionItem("Ce înseamnă Drum cu prioritate?", "Ai prioritate", "Acces interzis", "Cedezi mereu trecerea", 0),
        new QuestionItem("Ce reprezintă indicatorul 50 km/h?", "Viteză minimă", "Viteză maximă", "Viteză recomandată", 1),
        new QuestionItem("Ce înseamnă Acces interzis?", "Intrarea interzisă", "Parcare permisă", "Drum prioritar", 0),
        new QuestionItem("Ce trebuie să faci la Cedează trecerea?", "Accelerezi", "Ai prioritate", "Acorzi prioritate", 2),

        // --- 18 intrebari suplimentare legate de cele 7 indicatoare si regulile lor ---
        new QuestionItem("Ce formă geometrică are indicatorul STOP?", "Octogon (8 laturi)", "Triunghi echilateral", "Cerc albastru", 0),
        new QuestionItem("Cum este orientat triunghiul indicatorului Cedează trecerea?", "Cu vârful în jos", "Cu vârful în sus", "Cu vârful spre dreapta", 0),
        new QuestionItem("Din ce categorie face parte indicatorul Acces interzis?", "Interzicere sau restricție", "Informare", "Avertizare", 0),
        new QuestionItem("Din ce categorie face parte indicatorul Parcare?", "Informare", "Obligare", "Prioritate", 0),
        new QuestionItem("Ce culoare predomină pe fundalul indicatorului Parcare?", "Albastru", "Roșu", "Galben", 0),
        new QuestionItem("Ce formă are indicatorul Drum cu prioritate?", "Romb galben cu margine albă", "Cerc roșu", "Dreptunghi verde", 0),
        new QuestionItem("Ce obligație ai la întâlnirea indicatorului STOP?", "Oprești complet roțile", "Reduci viteza fără a opri", "Claxonezi și treci", 0),
        new QuestionItem("Ce trebuie să faci la apropierea de Trecerea pentru pietoni?", "Reduci viteza și acorzi prioritate", "Oprești obligatoriu chiar fără pietoni", "Mărești viteza", 0),
        new QuestionItem("Ce indică cifra 50 dintr-un cerc cu margine roșie?", "Limita maximă de 50 km/h", "Distanța de 50 m", "Viteză minimă obligatorie", 0),
        new QuestionItem("Cui i se adresează indicatorul Acces interzis?", "Tuturor vehiculelor din acel sens", "Doar pietonilor", "Doar camioanelor", 0),
        new QuestionItem("Când trebuie să oprești la indicatorul Cedează trecerea?", "Când circulă vehicule pe drumul prioritar", "Niciodată", "Doar noaptea", 0),
        new QuestionItem("Unde se amplasează de regulă indicatorul STOP?", "În intersecții fără vizibilitate", "Pe autostrăzi", "În parcări subterane", 0),
        new QuestionItem("Ce semnifică dungă albă orizontală pe fundal roșu?", "Acces interzis", "Sens unic", "Sfârșitul tuturor restricțiilor", 0),
        new QuestionItem("Din ce categorie fac parte STOP și Cedează trecerea?", "De prioritate", "De orientare", "Temporare", 0),
        new QuestionItem("Ce litera apare pe indicatorul de Parcare?", "Litera P albă", "Litera S roșie", "Litera A galbenă", 0),
        new QuestionItem("Cum procedeze șoferul pe un Drum cu prioritate într-o intersecție?", "Trece primul față de drumurile fără prioritate", "Cedează dreapta", "Oprește obligatoriu", 0),
        new QuestionItem("Ce se întâmplă dacă depășești 50 km/h în zona indicatorului 50?", "Încalci limita legală de viteză", "Primești prioritate", "Este permis în oraș", 0),
        new QuestionItem("Ce simbol este desenat pe indicatorul Trecere pentru pietoni?", "Un pieton pe marcaj zebră", "O mașină", "Un semafor", 0)
    };

    private List<QuestionItem> activeQuestions = new List<QuestionItem>();
    private int currentQuestion;
    private int score;
    private bool answered;

    void Start()
    {
        quizPanel.SetActive(false);
        resultPanel.SetActive(false);
    }

    public void StartQuiz()
    {
        if (studyModeManager != null)
            studyModeManager.DeactivateStudyMode();

        studyButton.SetActive(false);
        startQuizButton.SetActive(false);

        resultPanel.SetActive(false);
        quizPanel.SetActive(true);

        SelectRandomQuestions(questionsPerQuiz);

        currentQuestion = 0;
        score = 0;

        ShowQuestion();
    }

    private void SelectRandomQuestions(int count)
    {
        List<QuestionItem> pool = new List<QuestionItem>(allQuestions);
        activeQuestions.Clear();

        int totalToPick = Mathf.Min(count, pool.Count);

        for (int i = 0; i < totalToPick; i++)
        {
            int randomIndex = Random.Range(0, pool.Count);
            activeQuestions.Add(pool[randomIndex]);
            pool.RemoveAt(randomIndex);
        }
    }

    void ShowQuestion()
    {
        answered = false;

        questionText.gameObject.SetActive(true);
        questionCounter.gameObject.SetActive(true);
        feedbackText.gameObject.SetActive(true);

        QuestionItem q = activeQuestions[currentQuestion];

        questionText.text = q.question;

        questionCounter.text =
            "Întrebarea " + (currentQuestion + 1) +
            " din " + activeQuestions.Count;

        feedbackText.text = "Alege un răspuns";
        feedbackText.color = Color.green;

        nextButton.gameObject.SetActive(false);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;

            answerButtons[i].gameObject.SetActive(true);
            answerButtons[i].interactable = true;

            answerButtons[i].GetComponentInChildren<TMP_Text>().text =
                q.options[i];

            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(
                () => CheckAnswer(index)
            );
        }
    }

    void CheckAnswer(int selectedAnswer)
    {
        if (answered)
            return;

        answered = true;

        QuestionItem q = activeQuestions[currentQuestion];

        if (selectedAnswer == q.correctIndex)
        {
            score++;

            feedbackText.text = "Corect!";
            feedbackText.color = Color.green;

            // Sunet pentru răspuns corect
            if (UIAudioManager.Instance != null)
                UIAudioManager.Instance.PlayQuizCorrect();
        }
        else
        {
            feedbackText.text = "Greșit! Răspuns corect: " +
                q.options[q.correctIndex];

            feedbackText.color = Color.red;

            // Sunet pentru răspuns greșit
            if (UIAudioManager.Instance != null)
                UIAudioManager.Instance.PlayQuizWrong();
        }

        foreach (Button button in answerButtons)
            button.interactable = false;

        nextButton.gameObject.SetActive(true);
    }

    public void NextQuestion()
    {
        if (!answered)
            return;

        currentQuestion++;

        if (currentQuestion >= activeQuestions.Count)
        {
            ShowResult();
            return;
        }

        ShowQuestion();
    }

    void ShowResult()
    {
        scoreText.text = "Quiz finalizat!\nScor: " +
                         score + " / " + activeQuestions.Count;

        questionText.gameObject.SetActive(false);
        questionCounter.gameObject.SetActive(false);
        feedbackText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);

        foreach (Button button in answerButtons)
            button.gameObject.SetActive(false);

        resultPanel.SetActive(true);
        resultPanel.transform.SetAsLastSibling();

        // Sunet pentru finalizarea quiz-ului
        if (UIAudioManager.Instance != null)
            UIAudioManager.Instance.PlayQuizFinish();
    }

    public void ExitQuiz()
    {
        resultPanel.SetActive(false);
        quizPanel.SetActive(false);

        if (studyModeManager != null)
            studyModeManager.DeactivateStudyMode();
        else
            Debug.LogError("StudyModeManager nu este conectat in QuizManager!");

        studyButton.SetActive(true);
        startQuizButton.SetActive(true);
    }
}