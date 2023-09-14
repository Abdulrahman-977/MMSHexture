
    const myModal = document.getElementById('myModal')
    const myInput = document.getElementById('myInput')

    myModal.addEventListener('shown.bs.modal', () => {
        myInput.focus()
    })
    $(document).ready(function () {
        $('.i-checks').iCheck({
            checkboxClass: 'icheckbox_square-green',
            radioClass: 'iradio_square-green',
        });
    });
    $(document).ready(function () {

        $('.i-checks').iCheck({
            checkboxClass: 'icheckbox_square-green',
            radioClass: 'iradio_square-green',
        });
        $('#external-events div.external-event').each(function () {
            $(this).data('event', {
                title: $.trim($(this).text()),
                stick: true
            });

            $(this).draggable({
                zIndex: 1111999,
                revert: true,
                revertDuration: 0
            });
        });




        $('#external-events div.external-event').each(function () {

            $(this).data('event', {
                title: $.trim($(this).text()),
                stick: true // maintain when user navigates (see docs on the renderEvent method)
            });

            $(this).draggable({
                zIndex: 1111999,
                revert: true,
                revertDuration: 0
            });

        });


        /* initialize the calendar
        -----------------------------------------------------------------*/

        var date = new Date();
        var d = date.getDate();
        var m = date.getMonth();
        var y = date.getFullYear();

        $('#calendar').fullCalendar({
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            editable: true,
            droppable: true, // this allows things to be dropped onto the calendar
            drop: function () {
                // is the "remove after drop" checkbox checked?
                if ($('#drop-remove').is(':checked')) {
                    // if so, remove the element from the "Draggable Events" list
                    $(this).remove();
                }
            },
            events: [

            ],
        });
    });


    $('.burger, .overlay').click(function () {
        $('.burger').toggleClass('clicked');
        $('.overlay').toggleClass('show');
        $('navbarID').toggleClass('show');
        $('body').toggleClass('overflow');
    });







// file meeting
    $(".dial").knob();
    $("#basic_slider").noUiSlider({
        start: 40,
        behaviour: 'tap',
        connect: 'upper',
        range: {
            'min': 20,
            'max': 80
        }
    });
    $("#range_slider").noUiSlider({
        start: [40, 60],
        behaviour: 'drag',
        connect: true,
        range: {
            'min': 20,
            'max': 80
        }
    });
    $("#drag-fixed").noUiSlider({
        start: [40, 60],
        behaviour: 'drag-fixed',
        connect: true,
        range: {
            'min': 20,
            'max': 80
        }
    });




    var elem = document.querySelector('.js-switch');
        var switchery = new Switchery(elem, { color: '#1AB394' });
        var elem_2 = document.querySelector('.js-switch_2');
        var switchery_2 = new Switchery(elem_2, { color: '#ED5565' });
        var elem_3 = document.querySelector('.js-switch_3');
        var switchery_3 = new Switchery(elem_3, { color: '#1AB394' });

        $('.i-checks').iCheck({
            checkboxClass: 'icheckbox_square-green',
            radioClass: 'iradio_square-green',
        });






// <!-- archives -->
    //  Page-Level Scripts 
    $(document).ready(function() {
        $('.dataTables-example').dataTable({
            responsive: true,
            "dom": 'T<"clear">lfrtip',
            "tableTools": {
                "sSwfPath": "js/plugins/dataTables/swf/copy_csv_xls_pdf.swf"
            }
        });
    });
// <!-- END archives -->

// <!-- creatMeeting -->

    $(document).ready(function(){
        var elem = document.querySelector('.js-switch');
        var switchery = new Switchery(elem, { color: '#1AB394' });

        var elem_2 = document.querySelector('.js-switch_2');
        var switchery_2 = new Switchery(elem_2, { color: '#ED5565' });

        var elem_3 = document.querySelector('.js-switch_3');
        var switchery_3 = new Switchery(elem_3, { color: '#1AB394' });
    });
    var config = {
            '.chosen-select'           : {},
            '.chosen-select-deselect'  : {allow_single_deselect:true},
            '.chosen-select-no-single' : {disable_search_threshold:10},
            '.chosen-select-no-results': {no_results_text:'Oops, nothing found!'},
            '.chosen-select-width'     : {width:"95%"}
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }
    
    function addDataInvitedEmail() {
    var Invited_Email = document.getElementById("InvitedEmail").value;
    var Invited_Number = document.getElementById("InvitedNumber").value;
    var DropList_UserLevel = document.getElementById("DropListUserLevel").value;
    var Message_Type = document.querySelector('input[name="MessageType"]:checked').value;

    var Tabel_Invited= document.getElementById("TabelInvited");
    var row = Tabel_Invited.insertRow(); 
    var cell1 = row.insertCell(0); 
    var cell2 = row.insertCell(1);
    var cell3 = row.insertCell(2); 
    var cell4 = row.insertCell(3);
    var cell5 = row.insertCell(4);
    var cell6 = row.insertCell(5);


    cell1.innerHTML = Tabel_Invited.rows.length - 1;
    cell2.innerHTML = Invited_Email;
    cell3.innerHTML = Invited_Number;
    cell4.innerHTML = DropList_UserLevel;
    cell5.innerHTML = Message_Type;
    cell6.innerHTML = '<button onclick="deleteRow(this)">Delete</button>';

    document.getElementById("InvitedEmail").value = '';
    document.getElementById("InvitedNumber").value = '';
    document.getElementById("DropListUserLevel").value = '';
    document.querySelector('input[name="MessageType"]:checked').checked = false;
    }

  
    let countList = 0; // Initialize count to 0
    function addDataListSpeakers() {
    const DropListSpeaker_Name = document.getElementById("DropListSpeaker'sName").value;
    const Subject = document.getElementById("subject").value;
    const Duration = document.getElementById("duration").value;
    
    if (DropListSpeaker_Name && Subject && Duration) { // Check if both fields are filled
    countList++; // Increment count
    const tableBody = document.getElementById("ListSpeakers");
    const newRow = tableBody.insertRow(); 
    const cell1 = newRow.insertCell(0); 
    const cell2 = newRow.insertCell(1);
    const cell3 = newRow.insertCell(2); 
    const cell4 = newRow.insertCell(3);
    const cell5 = newRow.insertCell(4);
    const cell6 = newRow.insertCell(5);

    cell1.innerHTML = countList;
    cell2.innerHTML = DropListSpeaker_Name;
    cell3.innerHTML = Subject;
    cell4.innerHTML = Duration;
    cell5.innerHTML = '<button onclick="deleteRow(this)">Delete</button>';

    document.getElementById("DropListSpeaker'sName").value = '';
    document.getElementById("subject").value = '';
    document.getElementById("duration").value = '';

    }
    }

    let countTabel = 0; // Initialize count to 0
    function addDataTableStriped() {
    const Subjeform_File_Multiplect = document.getElementById("formFileMultiple").value;
    
    if (Subjeform_File_Multiplect ) { // Check if both fields are filled
    countTabel++; // Increment count
    const tableBody = document.getElementById("ListOfAttachments");
    const newRow = tableBody.insertRow(); 
    const cell1 = newRow.insertCell(0); 
    const cell2 = newRow.insertCell(1);
    const cell3 = newRow.insertCell(2);

    cell1.innerHTML = countTabel;
    cell2.innerHTML = Subjeform_File_Multiplect ;
    cell3.innerHTML = '<button onclick="deleteRow(this)">Delete</button>';

    document.getElementById("formFileMultiple").value = '';


    }
    }

    var xValues = ["option1", "option2", "option3"];
    var yValues = [ 44, 24, 30];
    var barColors = ["red", "green","blue"];
    new Chart("myChart", {
    type: "bar",
    data: {
    labels: xValues,
    datasets: [{
        backgroundColor: barColors,
        data: yValues
    }]
    },
    options: {
    legend: {display: false},
    title: {
        display: true,
    }
    }
    });

    var voit = 2;
    const container = document.getElementById('input-cont');
    // Call addInput() function on button click
    function addInput(){
    voit++
    let input = document.createElement('input');
    input.placeholder = voit+' إجابة التصويت';
    container.appendChild(input);
    }

    function deleteRow(button) {
    var row = button.parentNode.parentNode;
    row.parentNode.removeChild(row);
    // Update the count numbers of remaining rows
    var rows = document.querySelectorAll('#tableBody tr');
    for (var i = 0; i < rows.length; i++) {
    rows[i].cells[0].innerHTML=i+1;}
    }
   
// <!-- End creatMeeting -->



// <!--    -->

    $(document).ready(function() {

            $('.i-checks').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green',
            });

        /* initialize the external events
         -----------------------------------------------------------------*/


        $('#external-events div.external-event').each(function() {

            $(this).data('event', {
                title: $.trim($(this).text()), 
                stick: true 
            });

            $(this).draggable({
                zIndex: 1111999,
                revert: true,      
                revertDuration: 0  
            });

        });


        /* initialize the calendar
         -----------------------------------------------------------------*/
        var date = new Date();
        var d = date.getDate();
        var m = date.getMonth();
        var y = date.getFullYear();

        $('#calendar').fullCalendar({
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            editable: true,
            droppable: true, 
            drop: function() {
                if ($('#drop-remove').is(':checked')) {
                    $(this).remove();
                }
            },
            events: [
                {
                    title: 'All Day Event',
                    start: new Date(y, m, 1)
                },
                {
                    title: 'Long Event',
                    start: new Date(y, m, d-5),
                    end: new Date(y, m, d-2),
                },
                {
                    id: 999,
                    title: 'Repeating Event',
                    start: new Date(y, m, d-3, 16, 0),
                    allDay: false,
                },
                {
                    id: 999,
                    title: 'Repeating Event',
                    start: new Date(y, m, d+4, 16, 0),
                    allDay: false
                },
                {
                    title: 'Meeting',
                    start: new Date(y, m, d, 10, 30),
                    allDay: false
                },
                {
                    title: 'Lunch',
                    start: new Date(y, m, d, 12, 0),
                    end: new Date(y, m, d, 14, 0),
                    allDay: false
                },
                {
                    title: 'Birthday Party',
                    start: new Date(y, m, d+1, 19, 0),
                    end: new Date(y, m, d+1, 22, 30),
                    allDay: false
                },
                {
                    title: 'Click for Google',
                    start: new Date(y, m, 28),
                    end: new Date(y, m, 29),
                    url: 'http://google.com/'
                }
            ],
        });


    });


session_start();
if (isset($_SESSION['fullname'])) {
    $fullname = $_SESSION['fullname'];
} else {
    $fullname = 'Guest';
}

if (isset($_SESSION['token'])) {
    $token = $_SESSION['token'];
} else {
    header('Location: login.php');
    exit;
}

if (isset($_SESSION['expiryDate'])) {
    $expiryDate = $_SESSION['expiryDate'];
} else {
    $expiryDate = 'none';
}

if (isset($_SESSION['refreshToken'])) {
    $refreshToken = $_SESSION['refreshToken'];
} else {
    $refreshToken = 'none';
}