using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

public class CharacterViewModel
{
    public int id { get; set; }
    [Required]
    [StringLength(60, MinimumLength = 3)]
    public string name { get; set; }

    public string gender { get; set; }

    [Required]
    [RegularExpression(@"^[1-9]\d*$")]
    public string level { get; set; }

    public Alignment alignment { get; set; }

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

    public Race race { get; set; }

    public int racePos { get; set; }

    public CharacterClass charClass { get; set; }

    public int classPos { get; set; }

    public string weapon { get; set; }

    public string spell { get; set; }

    public List<Spell> spells { get; set; }

    public int[] spellInputs { get; set; }

    public int[] weaponInputs { get; set; }

    public List<Weapon> weapons { get; set; }

    public IEnumerable<SelectListItem> raceList { get; set; }
    public IEnumerable<SelectListItem> classList { get; set; }
    public IEnumerable<SelectListItem> alignmentList { get; set; }
    public IEnumerable<SelectListItem> weaponList { get; set; }
    public IEnumerable<SelectListItem> spellList { get; set; }

    public CharacterViewModel()
    {
        race = new Race();
        alignment = new Alignment();
        charClass = new CharacterClass();
        spells = new List<Spell>();
        weapons = new List<Weapon>();
    }

    public CharacterViewModel(AppSetting appSetting)
    {
        DataObj dbo = new DataObj(appSetting);
        raceList = new SelectList(dbo.getAllRaces(), "id", "name");
        classList = new SelectList(dbo.getAllClasses(), "id", "name");
        alignmentList = new SelectList(dbo.getAllAlignments(), "id", "name");
        weaponList = new SelectList(dbo.getAllWeapons(), "id", "name");
        spellList = new SelectList(dbo.getAllSpells(), "id", "name");
    }

    public CharacterViewModel(int characterId, AppSetting appSetting)
    {
        DataObj dbo = new DataObj(appSetting);

        Character character = dbo.pullCharInfoById(characterId);

        id = character.id;

        name = character.name;

        level = character.level.ToString();

        gender = character.gender;

        alignment = character.alignment;

        race = character.race;

        charClass = character.charClass;

        str = character.str.ToString();
        con = character.con.ToString();
        dex = character.dex.ToString();
        inte = character.inte.ToString();
        cha = character.cha.ToString();
        wis = character.wis.ToString();

        spells = character.spells;
        weapons = character.weapons;

        raceList = new SelectList(dbo.getAllRaces(), "id", "name", race);
        classList = new SelectList(dbo.getAllClasses(), "id", "name");
        alignmentList = new SelectList(dbo.getAllAlignments(), "id", "name");
        weaponList = new SelectList(dbo.getAllWeapons(), "id", "name");
        spellList = new SelectList(dbo.getAllSpells(), "id", "name");

    }
}

