using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int correntAnswer = 0;
    private int questionSeen = 0;

    public int GetCorrentAnswer()
    {
        return correntAnswer;
    }
    public void IncrementCorrentAnswer()
    {
        correntAnswer++;
    }
    public void SetCorrentAnswer(int correntAnswer)
    {
        this.correntAnswer = correntAnswer;
    }
    public int GetQuestionSeen()
    {
        return questionSeen;
    }
    public void IncrementQuestionSeen()
    {
        questionSeen++;
    }
    public int CalculateScore(){
        return Mathf.RoundToInt(correntAnswer / (float)questionSeen * 100);
    }
}
