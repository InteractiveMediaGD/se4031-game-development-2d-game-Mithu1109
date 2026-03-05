using UnityEngine;

public class TunnelScore : MonoBehaviour
{
    
    private bool scored = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!scored && other.CompareTag("Player"))
        {
            FindObjectOfType<ScoreManager>().AddScore(1);
            scored = true;
        }
    }

   public void ResetScoreZone()

{
    Debug.Log("ScoreZone Reset");
    scored = false;
}
}