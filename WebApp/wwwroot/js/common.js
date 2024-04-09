
let SaveCheckup = () => {
    debugger;
    var medicineDetails = [];
    $('#tblMedicineDetails tbody tr').each(function () {
        var medicineDetail = {
            Id: $(this).find('input[name="CheckUPId"]').val(),
            //Medicine: $(this).find('select[name="Medicine"]').val(),
            Medicine: $("#tblAddMedicinefoot select[name='Medicine']").val(),
            NoOfDays: $("#tblAddMedicinefoot input[name='NoOfDays']").val(),
            WhenToTake: $("#tblAddMedicinefoot input[name='WhenToTake']").val(),
            IsBeforeMeal: $("#tblAddMedicinefoot input[name='IsBeforeMeal']").prop('checked'),
        };
        medicineDetails.push(medicineDetail);
    });
    let obj = {
        Id: $("#Id").val(),
        VisitId: $("#VisitId").val(),
        PatientId: $("#PatientId").val(),
        PatientType: $("#PatientType").val(),
        DoctorId: $("#DoctorId").val(),
        CheckupDate: $("#CheckupDate").val(),
        NextVisitDate: $("#NextVisitDate").val(),
        Advice: $("#Advice").val(),
        Comments: $("#Comments").val(),
        Symptoms: $("#Symptoms").val(),
        Diagnosis: $("#Diagnosis").val(),
        VitalSigns: $("#VitalSigns").val(),
        HPI: $("#HPI").val(),
        PhysicalExamination: $("#PhysicalExamination").val(),
        Medicine: medicineDetails,
        //Medicine: $("#Medicine").val(),
        //NoOfDays: $("#NoOfDays").val(),
        //WhenToTake: $("#WhenToTake").val(),
        //IsBeforeMeal: $("#IsBeforeMeal").is(":checked"),
        //LabTestsId: $("#LabTestsId").val(),
        //UnitPrice: $("#UnitPrice").val(),
        BPSystolic: $("#BPSystolic").val(),
        BPDiastolic: $("#BPDiastolic").val(),
        RespirationRate: $("#RespirationRate").val(),
        Temperature: $("#Temperature").val(),
        NursingNotes: $("#NursingNotes").val(),
    };
    $.post('/CheckUP/SaveCheckup', obj).done((res) => {
        Q.notify(res.statusCode, res.msg);
        if (res.statusCode == 1) {
            $('.ui-dialog-titlebar-close').click();
            loadCheckUpList();
        }
    }).fail((xhr) => {
        Q.notify(-1, 'server error');
    });
};