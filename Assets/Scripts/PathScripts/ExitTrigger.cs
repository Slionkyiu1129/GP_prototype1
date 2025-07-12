using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{
    public string direction; // 例如 "Up", "Down", "Left", "Right"

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PathManager.Instance.AddDirection(direction);
            PathManager.Instance.entryPointDirection = GetOppositeDirection(direction);
            //Debug.Log("設定 entryDirection: " + PathManager.Instance.entryPointDirection);
            // 為了簡化測試先 reload 同一場景
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private string GetOppositeDirection(string dir)
    {
        switch (dir)
        {
            case "Up": return "Down";
            case "Down": return "Up";
            case "Left": return "Right";
            case "Right": return "Left";
            default: return "None";
        }
    }
}
