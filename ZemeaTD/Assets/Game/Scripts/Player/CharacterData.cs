using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Data", menuName = "CharacterData/DataTest", order = 1)]
public class CharacterData : ScriptableObject
{
    public float floorSpeed;
    public float airSpeed;
    public float attackSpeed;
    public float jumpForce;
    public int minDamage;
    public int maxDamage;

    private float initialFloorSpeed;
    private float initialAirSpeed;
    private float initialAttackSpeed;
    private float initialJumpForce;
    private int initialMinDamage;
    private int initialMaxDamage;

    public void SaveInitialValues()
    {
        initialFloorSpeed = floorSpeed;
        initialAirSpeed = airSpeed;
        initialAttackSpeed = attackSpeed;
        initialJumpForce = jumpForce;
        initialMinDamage = minDamage;
        initialMaxDamage = maxDamage;
    }

    public void LevelUp()
    {
        attackSpeed += 0.5f;
        minDamage += 1;
        maxDamage += 1;
    }

    public void ResetStats()
    {
        floorSpeed = initialFloorSpeed;
        airSpeed = initialAirSpeed;
        attackSpeed = initialAttackSpeed;
        jumpForce = initialJumpForce;
        minDamage = initialMinDamage;
        maxDamage = initialMaxDamage;
    }
}