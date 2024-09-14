using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Game : MonoBehaviour
{
   [SerializeField] private Day[] days;
   private int currentDayIndex;
   public static Game Instance => instance;
   private static Game instance;
   private void Awake()
   {
       if(instance != null)
       {
           Destroy(gameObject);
           return;
       }
       
       instance = this;
       DontDestroyOnLoad(gameObject);
   }

   public void StartGame()
   {
       currentDayIndex = 0;
       StartDay(currentDayIndex);
   }
   
   private void StartDay(int index)
   {
       var currentDay = days[index];
       currentDay.Init();
       currentDay.Complete += OnDayComplete;
       SceneManager.LoadScene(currentDay.sceneId);
   }
   
   private void OnDayComplete()
   {
       days[currentDayIndex].Complete -= OnDayComplete;
       currentDayIndex++;
       if (currentDayIndex < days.Length)
       {
           StartDay(currentDayIndex);
       }
   }
}

[Serializable]
public class Day
{
    public event Action Complete;
    public int sceneId;
    public Rule[] rules;
    
    public void Init()
    {
        foreach (var rule in rules)
        {
            rule.Init();
            rule.Completed += OnRuleComplete;
        }
    }

    private void OnRuleComplete()
    {
        foreach (var rule in rules)
        {
            if(rule.IsCompleted == false)
            {
                return;
            }
        }
        
        Complete?.Invoke();
    }
}