using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Coctail", menuName = "", order = 1)]
public class Drink : ScriptableObject, IComparable<Drink>
{
    public enum Alkohol
    {
        Aperol,
        //Brandy,
        Gin,
        Rum,
        Tequila,
        Vodka
        //Wiskey,
        //Jagermeister
    }

    public enum Ingredient
    {
        Absinthe,
        Amaretto,
        Angostura,
        AgaveSyrup,
        AlmondSyrup,
        AppleJuice,
        Benedictine,
        Bitters,
        Campari,
        BlueCuracao,
        Champagne,
        CherryLiqueur,
        ChocolateLiqueur,
        CocchiAmericano,
        Cointreau,
        CoffeeLiqueur,
        CrèmeDeMure,
        CrèmeDeViolette,
        CoconutCream,
        Coffee,
        Cola,
        CranberryJuice,
        Drambuie,
        DryVermouth,
        Egg,
        ElderflowerLiqueur,
        Basil,
        GingerBeer,
        GrapefruitJuice,
        Grenadine,
        GreenChartreuse,
        HoneySyrup,
        LemonJuice,
        LemonLimeSoda,
        Lime,
        LimeJuice,
        MapleSyrup,
        MintLeaves,
        MaraschinoLiqueur,
        Milk,
        Orange,
        OrangeCuracao,
        PeachLiqueur,
        PeychaudsBitters,
        Prosecco,
        PassionfruitPuree,
        PineappleJuice,
        PouringCream,
        RaspberrySyrup,
        Salt,
        SimpleSyrup,
        Soda,
        SourMix,
        Sugar,
        SweetVermouth,
        TripleSec,
        OrangeJuice,
        HeavyCream,
        CinnamonSyrup,
        CremeDeCacao,
        ChocolateBitters,
        GrapefruitSoda,
        LilletBlanc,
        ApricotJam,
        YellowChartreuse,
        BananaLiqueur,
        MidoriMelonLiqueur,
        LilletRouge,
        OrangeBitters,
        Pineapple,
        Strawberries,
        OrangeMarmalade,
        BlackcurrantLiquer,
        Raspberries,
        PassionfruitSyrup,
        Bayleys,
        Suze,
        GingerSyrup,
        EarlyGreyTea,
        PassionfruitLiqueur,
        Cucumber,
        DonsMix,
        SageLeaves,
        MontenegroAmaro,
        VelvetFalernum
    }

    public List<Alkohol> alkohols;
    public List<Ingredient> ingredients;

    [TextArea(20, 10)]
    public string recipe;

    public int CompareTo(Drink other)
    {
        return ((new CaseInsensitiveComparer()).Compare(name, other.name));
    }
}
