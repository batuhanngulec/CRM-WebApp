var questionSelect = $("#questionSelect");//Soru seçilen dropdown bulundu

var count = $('#questionSelect option').length;//Soru seçilen dropdown'da kaç adet soru var

function removeSelectedQuestion() {
    var selectedQuestion = questionSelect.find("option:selected");//Soru seçilen dropdowndaki seçili soru alındı

    if (count > 0) {

        //calculate index

        var newEntry = $("#entry").clone();//Soru oluşturuldu

        newEntry.attr("id", "entry" + count);

        newEntry.find("#question").val(selectedQuestion.html());//soru inputa yazıldı

        newEntry.removeAttr('hidden');// ;)

        $("#body").append(newEntry);//Soru aşağı eklendi
        selectedQuestion.remove();//Dropdown'dan seçili soru kaldırıldı
        count = $('#questionSelect option').length;;
    }


}
function buttonDelete(button) {
    var entry = button.parents('.form-group').first();
    var relatedInput = entry.find("input");
    var relatedOption = document.createElement("option");

    relatedOption.text = relatedInput.val();

    questionSelect.append(relatedOption);

    entry.remove();

    count = $('#questionSelect option').length;;
}

//Page Functionality

//Şube seçince
$("#sube").on("change", function () {
   //burada
});

//Şube seçince
$("#tip").on("change", function () {
    //burada
});
function loadQuestions() {

}

