// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function addWeapon() {
    var weapon = $('#weapon').find(":selected").text();
    var weaponId = $('#weapon').find(":selected").val();
    $("#weapon-list").append("<label class='weapon'> - " + weapon  + "</label>" +
        "<input type='text' class='hidden' name='weapons' value = '" + weaponId + "' > <br>");
    $('#weapon option[value="' + weaponId + '"]').remove();
}

function addSpell() {
    var spell = $('#spell').find(":selected").text();
    var spellId = $('#spell').find(":selected").val();
    $("#spell-list").append("<label class='spell'> - " + spell + "</label>" +
        "<input type='text' class='hidden' name='spells' value = '" + spellId + "' > <br>");
    $('#spell option[value="' + spellId + '"]').remove();
}

