using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameStatsCounter : MonoBehaviour
{


    public int levelsPassedCount;

    public Text levelsPassedText;

    public Text deathsText;

    public Text maxLevelsPassedText;

    private PlayerControlls player;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerControlls>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    levelsPassedText.text = "Levels Finished: " + levelsPassedCount;
	    
	    if (player == null)
	    {
	        GameMetrics.DeathsCount += 1;
            GameMetrics.maxLevelsPassedCountList.Sort((a, b) => -1 * a.CompareTo(b));
            if (levelsPassedCount > GameMetrics.maxLevelsPassedCountList[4])
	        {
	            GameMetrics.maxLevelsPassedCountList.Add(levelsPassedCount);
	            GameMetrics.maxLevelsPassedCountList.Sort((a, b) => -1 * a.CompareTo(b));
            }
        }

	    maxLevelsPassedText.text = "Best Games: ";
	    for (int i = 0; i < 5; i++)
	    {
	        maxLevelsPassedText.text += System.Environment.NewLine + (i+1) + ":       " + GameMetrics.maxLevelsPassedCountList[i];
        }

	    deathsText.text = "Global Deaths: " + GameMetrics.DeathsCount;
	}
}
