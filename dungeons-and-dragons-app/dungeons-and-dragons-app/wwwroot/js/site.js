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
});

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

function roll(damageInfo) {
    var damageInfoArray = damageInfo.split("d");
    var numRolls = damageInfoArray[0];
    var numSides = damageInfoArray[1];
    var sum = 0;
    for (var i = 0; i < numRolls; i++) {
        sum += Math.floor(Math.random() * numSides) + 1;
    }
    rollToHit = Math.floor(Math.random() * 20) + 1;
    alert("You roll a 1d20 to hit: " + rollToHit);
    alert("You roll " + damageInfo + ": " + sum);
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