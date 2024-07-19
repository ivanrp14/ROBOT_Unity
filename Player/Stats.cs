using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    int coins;
    int level;
    float jumpForce;
    float forwardSpeed;
    const float MAX_JUMP_FORCE = 20f;
    const float MAX_FORWARD_SPEED = 20f;
    const float MIN_JUMP_FORCE = 5f;
    const float MIN_FORWARD_SPEED = 5f;
    const int MAX_LEVEL = 1;
    public Stats(int coins, int level, float jumpForce, float forwardSpeed)
    {
        this.coins = coins;
        this.level = level;
        this.jumpForce = jumpForce;
        this.forwardSpeed = forwardSpeed;
    }
    public Stats()
    {
        coins = 0;
        level = 0;
        jumpForce = MIN_JUMP_FORCE;
        forwardSpeed = MIN_FORWARD_SPEED;
    }
    public int Coins
    {
        get { return coins; }
        set { coins = value; }
    }
    public int Level
    {
        get { return level; }
        set
        {
            if (value >= 0 && value <= MAX_LEVEL)
            {
                level = value;

            }
        }
    }
    public float JumpForce
    {
        get { return jumpForce; }
        set
        {
            if (value >= MIN_JUMP_FORCE && value <= MAX_JUMP_FORCE)
            {
                jumpForce = value;
            }
        }
    }
    public float ForwardSpeed
    {
        get { return forwardSpeed; }
        set
        {
            if (value >= MIN_FORWARD_SPEED && value <= MAX_FORWARD_SPEED)
            {
                forwardSpeed = value;
            }
        }
    }
    public void IncreaseLevel()
    {
        Level++;
        JumpForce += 5f;
        ForwardSpeed += 5f;
    }
    public void IncreaseCoins()
    {
        Coins++;
    }
    public void DecreaseCoins()
    {
        Coins--;
    }
    public void ResetStats()
    {
        coins = 0;
        level = 0;
        jumpForce = MIN_JUMP_FORCE;
        forwardSpeed = MIN_FORWARD_SPEED;
    }
    public void ResetCoins()
    {
        coins = 0;
    }


}
