using UnityEngine;
using UnityEngine.Events;

public class Exp : MonoBehaviour
{
    [SerializeField] private int exp;
    [SerializeField] private int level;
    [SerializeField] private int expPerLevel = 100;

    public UnityEvent OnLevelUp;

    private void Start()
    {
        exp = 0;
        level = 1;
    }

    public void GetExp(int value)
    {
        exp += value;

        while (exp >= expPerLevel)
        {
            exp -= expPerLevel;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        OnLevelUp?.Invoke();
    }

#if UNITY_EDITOR
    [ContextMenu("Test GetExp (Add 50 EXP)")]
    private void TestGetExp()
    {
        GetExp(50);
        Debug.Log($"TestGetExp called. EXP: {exp}, Level: {level}");
    }
#endif
}
