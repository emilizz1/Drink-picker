using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shot", menuName = "", order = 2)]
public class Shot : ScriptableObject, IComparable<Shot>
{
    public enum Ingredient
    {
        Amaretto,
        Baileys,
        BananaLiqueur,
        BlueCuracao,
        ButterscotchSchnapps,
        Chambord,
        Cognac,
        CranberryJuice,
        DryVermouth,
        Frangelico,
        GrandMarnier,
        Guinness,
        Kahlua,
        Jagermeister,
        Lemon,
        LimeJuice,
        PeachLiqueur,
        RedBull,
        Sugar,
        Tequila,
        TripleSec,
        Vodka,
        Whiskey,
    }

    public List<Ingredient> ingredients;

    [TextArea(20, 10)]
    public string recipe;

    public int CompareTo(Shot other)
    {
        return ((new CaseInsensitiveComparer()).Compare(name, other.name));
    }
}
