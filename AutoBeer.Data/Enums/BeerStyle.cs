using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AutoBeer.Data.Enums
{
    public enum BeerStyle
    {
        [Display(Name ="Altbier")]
        Altbier,
        [Display(Name = "Amber ale")]
        AmberAle,
        [Display(Name = "Barley wine")]
        BarleyWine,
        [Display(Name = "Berliner Weisse")]
        BerlinerWeisse,
        [Display(Name = "Bière de Garde")]
        BiereDeGarde,
        [Display(Name = "Bitter")]
        Bitter,
        [Display(Name = "Bock")]
        Bock,
        [Display(Name = "Brown ale")]
        BrownAle,
        [Display(Name = "Steam beer")]
        SteamBeer,
        [Display(Name = "Cream ale")]
        CreamAle,
        [Display(Name = "Dortmunder Export")]
        Export,
        [Display(Name = "Doppelbock")]
        Doppelbock,
        [Display(Name = "Dunkel")]
        Dunkel,
        [Display(Name = "Dunkles Weissbier")]
        DunkelWeizen,
        [Display(Name = "Eisbock")]
        Eisbock,
        [Display(Name = "Red beers")]
        RedBeers,
        [Display(Name = "Geuze")]
        Geuze,
        [Display(Name = "Hefeweizen")]
        Hefeweizen,
        [Display(Name = "Helles")]
        Hell,
        [Display(Name = "India Pale Ale (IPA)")]
        IndiaPaleAle,
        [Display(Name = "Kölsch")]
        Kolsch,
        [Display(Name = "Lambic")]
        Lambic,
        [Display(Name = "Light ale")]
        LightAle,
        [Display(Name = "Maibock")]
        Maibock,
        [Display(Name = "Malt liquor")]
        MaltLiquor,
        [Display(Name = "Mild ale")]
        Mild,
        [Display(Name = "Märzen")]
        Marzen,
        [Display(Name = "Old ale")]
        OldAle,
        [Display(Name = "Brown beers")]
        BrownBeers,
        [Display(Name = "Pale ale")]
        PaleAle,
        [Display(Name = "Pilsener")]
        Pilsener,
        [Display(Name = "Porter")]
        Porter,
        [Display(Name = "Red ale")]
        RedAle,
        [Display(Name = "Roggenbier (historical)")]
        RyeAle,
        [Display(Name = "Saison")]
        Saison,
        [Display(Name = "Scotch ale")]
        ScotchAle,
        [Display(Name = "Sweet Stout")]
        SweetStout,
        [Display(Name = "Dry Stout")]
        DryStout,
        [Display(Name = "Imperial Stout")]
        ImperialStout,
        [Display(Name = "Schwarzbier")]
        Schwarzbier,
        [Display(Name = "Vienna Lager")]
        Vienna,
        [Display(Name = "Witbier")]
        Witbier,
        [Display(Name = "Weissbier")]
        Weissbier,
        [Display(Name = "Weizenbock")]
        Weizenbock
    }
}
