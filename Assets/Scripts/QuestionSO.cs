using UnityEngine;

[CreateAssetMenu(fileName = "Quiz Question", menuName = "New Question", order = 0)]
public class QuestionSO : ScriptableObject {
    [TextArea(2, 6)] // Hiển thị ô nhập dữ liệu lớn hơn trong Unity Editor
    [SerializeField] private string question = "Enter new question here"; // Câu hỏi
    [SerializeField] private string[] answers = new string[4]; // Mảng chứa các đáp án
    [SerializeField] private int correctAnswerIndex = 0; // Index của đáp án đúng

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
