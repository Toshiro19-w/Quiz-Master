using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] QuestionSO questions;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    [SerializeField] Sprite defaultButtonSprite;
    [SerializeField] Sprite correctButtonSprite;
    void Start()
    {
        questionText.text = questions.GetQuestion();

        for(int i = 0; i < answerButtons.Length; i++){
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = questions.GetAnswers(i);
        }
    }

    public void OnAnswerSelected(int i){
        Image buttonImage;

        if(i == questions.GetCorrectAnswerIndex()){
            questionText.text = "Correct!";
            buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = correctButtonSprite; 
        } else {
            correctAnswerIndex = questions.GetCorrectAnswerIndex();
            questionText.text = "Incorrect! The correct answer is: \n" + questions.GetAnswers(correctAnswerIndex);
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctButtonSprite;
        }
    }
}
