
var _langugeControl;
var _ctrlsArray;

var initHindiTyping = function (ctrlsArray) {

    _ctrlsArray = ctrlsArray;

    google.load("elements", "1", { packages: "transliteration" });
    google.setOnLoadCallback(function () {
        var options = {
            sourceLanguage: google.elements.transliteration.LanguageCode.ENGLISH,       // source language
            destinationLanguage: [google.elements.transliteration.LanguageCode.HINDI],  // destination language
            shortcutKey: 'ctrl+g',
            transliterationEnabled: true
        };
        _langugeControl = new google.elements.transliteration.TransliterationControl(options);
        //control.makeTransliteratable(['txtMessage', 'txtMessage2']);
        _langugeControl.makeTransliteratable(ctrlsArray);
    });
}
var reinitHindiTyping = function () {

    //function to change the language dynamically(Google API)
    _langugeControl.setLanguagePair(
        google.elements.transliteration.LanguageCode.ENGLISH,
        google.elements.transliteration.LanguageCode["HINDI"]);

    _langugeControl.makeTransliteratable(_ctrlsArray);
};

var reinitHindiTyping_forced = function (ctrlsArrays) {
    _ctrlsArray = null;
    _ctrlsArray = ctrlsArrays;
    //function to change the language dynamically(Google API)
    _langugeControl.setLanguagePair(
        google.elements.transliteration.LanguageCode.ENGLISH,
        google.elements.transliteration.LanguageCode["HINDI"]);

    _langugeControl.makeTransliteratable(_ctrlsArray);
};





