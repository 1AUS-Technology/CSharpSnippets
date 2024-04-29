namespace HeadFirstDesignPatterns.BehaviorialPatterns.ChainOfResponsibility;

public class Query
{
    public enum Argument
    {
        Attack,
        Defense
    }

    public string CreatureName;
    public int Value;

    public Argument WhatToQuery;
}