using UnityEngine;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowAnswer = 10f;
    public bool loadNextQuestion = false;
    public bool isAnsweringQuestion;
    public float fillFraction; // Giá trị fillAmount của timerImage
    float timerValue;

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer(){
        timerValue = 0;
        isAnsweringQuestion = true;
    }

    void UpdateTimer(){
        timerValue -= Time.deltaTime;

        if(isAnsweringQuestion){
            if(timerValue > 0){
                fillFraction = timerValue / timeToCompleteQuestion;
            } else {
                timerValue = timeToShowAnswer;
                isAnsweringQuestion = false;
            }
        } else {
            if(timerValue > 0){
                fillFraction = timerValue / timeToShowAnswer;
            } else {
                timerValue = timeToCompleteQuestion;
                isAnsweringQuestion = true;
                loadNextQuestion = true;
            }
        }

        //Debug.Log(timerValue);
    }
}
