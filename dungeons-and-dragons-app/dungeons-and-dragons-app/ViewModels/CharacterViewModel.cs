using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CharacterViewModel
{
    public string name { get; set; }
    public string gender { get; set; }
    public string level { get; set; }
    public string alignment { get; set; }
    public string str { get; set; }
    public string dex { get; set; }
    public string con { get; set; }
    public string inte { get; set; }
    public string wis { get; set; }
    public string cha { get; set; }
    public string race { get; set; }
    public string charClass { get; set; }
    public string weapon { get; set; }
    public string spell { get; set; }

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

