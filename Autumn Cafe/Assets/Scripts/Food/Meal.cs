using UnityEngine;

[RequireComponent(typeof(Pickupable))]
public class Meal : MonoBehaviour
{
    public MealType mealType;
    
    [SerializeField] private int maxScore = 100;
    
    [Range(0f, 1f), SerializeField] private float scoreMultiplier = 1f;

    public int GetCalculatedScore() => Mathf.RoundToInt(maxScore * scoreMultiplier);
}