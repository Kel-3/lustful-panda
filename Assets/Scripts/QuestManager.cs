using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;  

    private List<Quest> activeQuests = new List<Quest>();  

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    public void AddQuest(Quest newQuest)
    {
        activeQuests.Add(newQuest);
        Debug.Log("Quest: " + newQuest.questName);
        HintManager.instance.ShowHint(newQuest.questDescription);
    }

    
    public void ItemCollected(string itemName)
    {
        foreach (Quest quest in activeQuests)
        {
            if (quest.targetItem == itemName && !quest.isCompleted)
            {
                quest.CompleteQuest();
                Debug.Log("Quest Selesai: " + "Temukan Pintu Untuk Membukanya");
                HintManager.instance.ShowHint("Temukan pintu untuk membukanya.");
            }
        }
    }
}
