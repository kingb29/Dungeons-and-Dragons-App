using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CharacterViewModel
{
    public string name { get; set; }
    public string gender { get; set; }
    public int level { get; set; }
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

    public IEnumerable<SelectListItem> races { get; set; }
    public IEnumerable<SelectListItem> classes { get; set; }
    //public IEnumerable<SelectListItem> armor { get; set; }
    public IEnumerable<SelectListItem> weapons { get; set; }
    public IEnumerable<SelectListItem> spells { get; set; }

    public CharacterViewModel(IEnumerable<SelectListItem> racesIn, IEnumerable<SelectListItem> classesIn,
        IEnumerable<SelectListItem> weaponsIn, IEnumerable<SelectListItem> spellsIn)
    {
        races = racesIn;
        classes = classesIn;
        weapons = weaponsIn;
        spells = spellsIn;
    }

}

