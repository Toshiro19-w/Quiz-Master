using UnityEngine;

[CreateAssetMenu(fileName = "Quiz Question", menuName = "New Question", order = 0)]
public class QuestionSO : ScriptableObject {
    [TextArea(2, 6)] 
    [SerializeField] private string question = "Enter new question here";
    [SerializeField] private string[] answers = new string[4];
    [SerializeField] private int correctAnswerIndex = 0;

    public string GetQuestion() {
        return question;
    }

    public string GetAnswers(int index) {
        return answers[index];
    }

    public int GetCorrectAnswerIndex() {
        return correctAnswerIndex;
    }
    
}
