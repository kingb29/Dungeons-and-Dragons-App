using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

public class CharacterViewModel
{
    [Required]
    [StringLength(60, MinimumLength = 3)]
    public string name { get; set; }

    public string gender { get; set; }

    [Required]
    [RegularExpression(@"^[1-9]\d*$")]
    public string level { get; set; }

    public string alignment { get; set; }

    [Required]
    [RegularExpression(@"^[1-9]\d*$")]
    public string str { get; set; }

    [Required]
    [RegularExpression(@"^[1-9]\d*$")]
    public string dex { get; set; }

    [Required]
    [RegularExpression(@"^[1-9]\d*$")]
    public string con { get; set; }

    [Required]
    [RegularExpression(@"^[1-9]\d*$")]
    public string inte { get; set; }

    [Required]
    [RegularExpression(@"^[1-9]\d*$")]
    public string wis { get; set; }

    [Required]
    [RegularExpression(@"^[1-9]\d*$")]
    public string cha { get; set; }

    public string race { get; set; }

    public string charClass { get; set; }

    public string weapon { get; set; }

    public string spell { get; set; }

    public int[] spells { get; set; }

    public int[] weapons { get; set; }

    public IEnumerable<SelectListItem> raceList { get; set; }
    public IEnumerable<SelectListItem> classList { get; set; }
    public IEnumerable<SelectListItem> alignmentList { get; set; }
    public IEnumerable<SelectListItem> weaponList { get; set; }
    public IEnumerable<SelectListItem> spellList { get; set; }

    public CharacterViewModel()
    {
    }

    public CharacterViewModel(IEnumerable<SelectListItem> racesIn, IEnumerable<SelectListItem> classesIn,
        IEnumerable<SelectListItem> alignmentsIn, IEnumerable<SelectListItem> weaponsIn, IEnumerable<SelectListItem> spellsIn)
    {
        raceList = racesIn;
        classList = classesIn;
        alignmentList = alignmentsIn;
        weaponList = weaponsIn;
        spellList = spellsIn;
    }

}

