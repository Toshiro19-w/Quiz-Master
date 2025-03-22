using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText; // Text hiển thị câu hỏi
    [SerializeField] List<QuestionSO> questions; // Danh sách các câu hỏi
    QuestionSO currentQuestion; // ScriptableObject chứa câu hỏi và đáp án

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons; // Mảng chứa các button đáp án
    int correctAnswerIndex; // Index của đáp án đúng
    bool hasAnsweredEarly = true; // Biến kiểm tra xem người chơi đã chọn đáp án chưa

    [Header("Button")]
    [SerializeField] Sprite defaultButtonSprite; // Sprite mặc định của button
    [SerializeField] Sprite correctButtonSprite; // Sprite của button khi chọn đúng

    [Header("Timer")]
    [SerializeField] Image timerImage; // Image hiển thị thanh thời gian
    Timer timer;
    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText; // Text hiển thị điểm
    ScoreKeeper scoreKeeper;
    [Header("ProgressBar")]
    [SerializeField] Slider progressBar; // Slider hiển thị tiến độ

    public bool isComplete; // Biến kiểm tra xem đã hoàn thành game chưa

    [System.Obsolete]
    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion){
            if(progressBar.value == progressBar.maxValue){
                isComplete = true;
                return;
            }
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        } else if(!hasAnsweredEarly && !timer.isAnsweringQuestion){
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    // Hiển thị kết quả đúng hoặc sai
    void DisplayAnswer(int i){
        Image buttonImage;

        if(i == currentQuestion.GetCorrectAnswerIndex()){
            questionText.text = "Chính xác!";
            buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = correctButtonSprite;
            scoreKeeper.IncrementCorrentAnswer();
        } else {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            questionText.text = "Không chính xác! Câu trả lời đúng là: \n" + currentQuestion.GetAnswers(correctAnswerIndex);
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctButtonSprite;
        }
    }

    // Xử lý khi người chơi chọn đáp án
    public void OnAnswerSelected(int i){
        hasAnsweredEarly = true;
        DisplayAnswer(i);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Điểm: " + scoreKeeper.CalculateScore();
    }

    // Lấy câu hỏi tiếp theo
    void GetNextQuestion(){
        if(questions.Count > 0){
            SetButtonState(true);
            SetDefaultButtonSprite();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionSeen();
        }
    }

    // Reset trạng thái của các button
    public void SetButtonState(bool state){
        for(int i = 0; i < answerButtons.Length; i++){
            answerButtons[i].GetComponent<Button>().interactable = state;
        }
    }

    // Reset sprite của các button về sprite mặc định
    void SetDefaultButtonSprite(){
        for(int i = 0; i < answerButtons.Length; i++){
            answerButtons[i].GetComponent<Image>().sprite = defaultButtonSprite;
        }
    }

    // Lấy ngẫu nhiên một câu hỏi từ danh sách câu hỏi
    void GetRandomQuestion(){
        int randomIndex = Random.Range(0, questions.Count);
        currentQuestion = questions[randomIndex];
        if(questions.Contains(currentQuestion)) questions.Remove(currentQuestion);
    }

    // Hiển thị câu hỏi và các đáp án
    public void DisplayQuestion(){
        questionText.text = currentQuestion.GetQuestion();
        for(int i = 0; i < answerButtons.Length; i++){
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.GetAnswers(i);
        }
    }
}
