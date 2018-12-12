// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $(".weapon-list-item").each(function () {
        var weaponId = $(this).find('input').val()
        $('#weapon option[value="' + weaponId + '"]').remove();
    });

    $(".spell-list-item").each(function () {
        var spellId = $(this).find('input').val()
        $('#spell option[value="' + spellId + '"]').remove();
    });

    $(".char-stat-modifier").each(function () {
        var stat = parseInt($(this).siblings(".char-stat-number").text());
        var modifier = "+0";
        if (stat > 10) {
            stat = stat - 10;
            modifier = "+" + Math.floor(stat / 2);
        }
        else {
            stat = stat - 10;
            modifier = Math.floor(stat / 2);
            if (modifier == 0) {
                modifier = "+0";
            }
        }
        $(this).text(modifier);
    });

    $(".char-init-number").text($(".char-stat-name:contains('DEX')").siblings(".char-stat-modifier").text());
});


$(function () {
    $("#dialog-tohit").dialog({
        autoOpen: false,
        resizable: false,
        height: "auto",
        width: 400,
        modal: true,
        buttons: {
            Yes: function () {
                $(this).dialog("close");
                $("#dialog-damage").dialog("open");
            },
            No: function () {
                $(this).dialog("close");
            }
        }
    });
});

    $(function () {
        $("#dialog-damage").dialog({
            autoOpen: false,
            resizable: false,
            height: "auto",
            width: 400,
            modal: true,
            buttons: {
                Thanks: function () {
                    $(this).dialog("close");
                }
            }
        });
});

$(function () {
    $("#dialog-initiative").dialog({
        autoOpen: false,
        resizable: false,
        height: "auto",
        width: 400,
        modal: true,
        buttons: {
            Thanks: function () {
                $(this).dialog("close");
            }
        }
    });
});


$(".weapon").click(function () {
    
    var damageInfo = "1d20";
    if ($(".char-stat-name:contains('STR')").siblings(".char-stat-modifier").text().includes("+")) {
        var modifier = $(".char-stat-name:contains('STR')").siblings(".char-stat-modifier").text().replace("+", "");
    }
    else {
        var modifier = $(".char-stat-name:contains('STR')").siblings(".char-stat-modifier").text();
    }
    if (modifier >= 0) {
        $("#equation-tohit").text(damageInfo + " + " + modifier + " =");
    }
    else {
        if (modifier.includes("-")) {
            $("#equation-tohit").text(damageInfo + " - " + modifier.replace("-", "") + " =");
        }
        else {
            $("#equation-tohit").text(damageInfo + " - " + modifier + " =")
        }
    }

    $("#roll-tohit").text(roll(damageInfo, modifier));

    damageInfo = $(this).attr("damage");

    $("#equation-damage").text(damageInfo + " + " + modifier + " =");

    $("#roll-damage").text(roll(damageInfo, modifier));

    $("#dialog-tohit").dialog("open");     
});

$(".spell").click(function () {

    var damageInfo = "1d20";
    var modifier = spellModifier();

    if (modifier >= 0) {
        $("#equation-tohit").text(damageInfo + " + " + modifier + " =");
    }
    else {
        if (modifier.includes("-")) {
            $("#equation-tohit").text(damageInfo + " - " + modifier.replace("-", "") + " =");
        }
        else {
            $("#equation-tohit").text(damageInfo + " - " + modifier + " =")
        }
    }

    $("#roll-tohit").text(roll(damageInfo, modifier));

    damageInfo = $(this).attr("damage");

    if (modifier >= 0) {
        $("#equation-damage").text(damageInfo + " + " + modifier + " =");
    }
    else {
        if (modifier.includes("-")) {
            $("#equation-damage").text(damageInfo + " - " + modifier.replace("-", "") + " =");
        }
        else {
            $("#equation-damage").text(damageInfo + " - " + modifier + " =")
        }
    }

    $("#roll-damage").text(roll(damageInfo, modifier));

    $("#dialog-tohit").dialog("open"); 

});


// TEST THIS
function spellModifier() {
    var charClass = $(".class").text();
    if (charClass == "Bard" || charClass == "Sorcerer" || charClass == "Warlock") {
        if ($(".char-stat-name:contains('CHA')").siblings(".char-stat-modifier").text().includes("+")) {
            return $(".char-stat-name:contains('CHA')").siblings(".char-stat-modifier").text().replace("+", "");
        }
        else {
            return $(".char-stat-name:contains('CHA')").siblings(".char-stat-modifier").text();
        }
    }
    else if (charClass == "Wizard") {
        if ($(".char-stat-name:contains('INT')").siblings(".char-stat-modifier").text().includes("+")) {
            return $(".char-stat-name:contains('INT')").siblings(".char-stat-modifier").text().replace("+", "");
        }
        else {
            return $(".char-stat-name:contains('INT')").siblings(".char-stat-modifier").text();
        }
    }
    else if (charClass == "Cleric" || charClass == "Druid" || charClass == "Paladin" || charClass == "Ranger") {
        if ($(".char-stat-name:contains('WIS')").siblings(".char-stat-modifier").text().includes("+")) {
            return $(".char-stat-name:contains('WIS')").siblings(".char-stat-modifier").text().replace("+", "");
        }
        else {
            return $(".char-stat-name:contains('WIS')").siblings(".char-stat-modifier").text();
        }
    }
    else {
        return 0;
    }
}

function addWeapon() {
    var weapon = $('#weapon').find(":selected").text();
    var weaponId = $('#weapon').find(":selected").val();
    $("#weapon-list").append("<div class='weapon-list-item'><label class='weapon'>" + weapon  + "</label>" +
        "<input type='text' class='hidden' name='weaponInputs' value = '" + weaponId + "' > <span onclick='removeWeapon(this)' class='glyphicon glyphicon-remove weapon-remove'></span> <br></div>");
    $('#weapon option[value="' + weaponId + '"]').remove();
}

function addSpell() {
    var spell = $('#spell').find(":selected").text();
    var spellId = $('#spell').find(":selected").val();
    $("#spell-list").append("<div class='spell-list-item'><label class='spell'>" + spell + "</label>" +
        "<input type='text' class='hidden' name='spellInputs' value = '" + spellId +
        "' > <span onclick = 'removeSpell(this)' class= 'glyphicon glyphicon-remove spell-remove' ></span > <br></div>");
    $('#spell option[value="' + spellId + '"]').remove();
}

function roll(rollInfo, modifier) {
    var rollInfoArray = rollInfo.split("d");
    var numRolls = rollInfoArray[0];
    var numSides = rollInfoArray[1];
    var sum = 0;
    for (var i = 0; i < numRolls; i++) {
        sum += Math.floor(Math.random() * numSides) + 1;
    }
    sum = sum + parseInt(modifier);

    if (sum < 1) {
        sum = 1;
    }
    return sum;
}

function removeSpell(spell) {
    var spellName = $(spell).parent().find('label').text();
    var spellValue = $(spell).parent().find('input').val();
    $("#spell > option").each(function (index) {
        if (spellName.toLowerCase() < $(this).text().toLowerCase()) {
            $(this).before("<option value='" + spellValue + "'>" + spellName + "</option>");
            return false;
        }
    });
    $(spell).parent().remove();
}

function removeWeapon(weapon) {
    var weaponName = $(weapon).parent().find('label').text();
    var weaponValue = $(weapon).parent().find('input').val();
    $("#weapon > option").each(function (index) {
        if (weaponName.toLowerCase() < $(this).text().toLowerCase()) {
            $(this).before("<option value='" + weaponValue + "'>" + weaponName + "</option>");
            return false;
        }
    });
    $(weapon).parent().remove();
}

// adds hit points
function addHitPoint() {
    if ($(".char-hp-total").hasClass("red")) {
        $(".char-hp-total").removeClass("red");
        $(".char-hp-current").removeClass("red");
        $(".char-hp-bonus").removeClass("red");
    }
    
    var total = $(".char-hp-total").text();
    var current = $(".char-hp-current").text();
    var bonus = $(".char-hp-bonus").text();
    if (total == current) { // if hp is full, give bonus hp
        if (!$(".char-hp-bonus").hasClass("gold")) {
            $(".char-hp-bonus").addClass("gold");
        }
        bonus++;
        $(".char-hp-bonus").text(bonus);
    }
    else {
        current++;
        $(".char-hp-current").text(current);
    }
}

// removes hit points
function removeHitPoint() {
    var total = parseInt($(".char-hp-total").text());
    var current = parseInt($(".char-hp-current").text());
    var bonus = parseInt($(".char-hp-bonus").text());
    console.log(total + " " + current + " " + bonus);
    if (total == current && bonus != 0) { // removes bonus hp first
        bonus--;

        if (bonus == 0) {
            $(".char-hp-bonus").removeClass("gold");
        }

        $(".char-hp-bonus").text(bonus);
    }
    else if (bonus == 0 && current == 0) { // you are dead!
        alert("You are dead!");
        $(".char-hp-total").addClass("red");
        $(".char-hp-current").addClass("red");
        $(".char-hp-bonus").addClass("red");
    }
    else { // removes current hp if no bonus hp exists
        current = current - 1;
        console.log(current);
        $(".char-hp-current").text(current);
    }
}

$(".char-init-number").click(function () {
    var damageInfo = "1d20";
    if ($(".char-stat-name:contains('DEX')").siblings(".char-stat-modifier").text().includes("+")) {
        var modifier = $(".char-stat-name:contains('DEX')").siblings(".char-stat-modifier").text().replace("+", "");
    }
    else {
        var modifier = $(".char-stat-name:contains('DEX')").siblings(".char-stat-modifier").text();
    }

    $("#roll-initiative").text(roll(damageInfo, modifier));

    $("#dialog-initiative").dialog("open");
});