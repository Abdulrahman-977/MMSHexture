var MyApp = (function(){

    var _hub = null;
    //http ://mmshexture-001-site1.gtempurl.com/
    //https ://localhost:44338
    var _hubUrl = 'https://localhost:44338/signalr/hubs';
var meeting_id = '';
var user_id = '';
var user_id_data = '';
function init(uid, mid, useriddata){
    user_id = uid;
    meeting_id = mid;
    user_id_data = useriddata;
    $('#meetingname').text(meeting_id);
    $('#me').find("#nickname").text(user_id + '(Me)');
    document.title = user_id;

    SignalServerEventBinding();
    EventBinding();
}
function SignalServerEventBinding(){
    // Set up the SignalR connection
   
    $.connection.hub.logging = true;
    _hub = $.connection.webRtcHub;
    $.connection.hub.url = _hubUrl;

    var serverFn = function (data, to_connid) {
        _hub.server.exchangeSDP(data, to_connid);
    };

    _hub.client.reset = function () {
        
        location.reload();
    }
    _hub.client.addtask = function (task, askUser, datepicker1,username) {
        const TaskBody = document.getElementById("TaskBody");
        const newRow = TaskBody.insertRow(); // Insert a new row
        const cell1 = newRow.insertCell(0); // Insert count number cell
        const cell2 = newRow.insertCell(1); // Insert first name cell
        const cell3 = newRow.insertCell(2);
        const cell4 = newRow.insertCell(3);
        //const cell5 = newRow.insertCell(4);
        cell1.innerHTML = $("#TaskBody").find("tr").length ;
        cell2.innerHTML = task;
        cell3.innerHTML = username;
        cell4.innerHTML = datepicker1;
    }
    _hub.client.showvottingresult = function (vottingConnections) {
        $("#loaderdiv").hide();
        GetVoteVoteResult(vottingConnections, meeting_id)
    }
    _hub.client.adddecision = function (board, title, startDate, createdDate) {
        const TaskBody = document.getElementById("DecisionBody");
        const newRow = TaskBody.insertRow(); // Insert a new row
        const cell1 = newRow.insertCell(0); // Insert count number cell
        const cell2 = newRow.insertCell(1); // Insert first name cell
        const cell3 = newRow.insertCell(2);
        const cell4 = newRow.insertCell(3);
        const cell5 = newRow.insertCell(4);
        cell1.innerHTML = $("#DecisionBody").find("tr").length;
        cell2.innerHTML = title;
        cell3.innerHTML = board;
        cell4.innerHTML = startDate;
        cell5.innerHTML = createdDate;
    }
    _hub.client.deletedecision = function (rang) {
        if (rang) {
            $("#DecisionBody tr").slice((rang - 1), rang).remove();
            $.each($("#DecisionBody tr").find("td:first"), function (index, item) {
                $(this).html(index + 1)
            });
        }
        
    }
    _hub.client.deletetask = function (rang) {
        if (rang) {
            $("#TaskBody tr").slice((rang - 1), rang).remove();
            $.each($("#TaskBody tr").find("td:first"), function (index, item) {
                $(this).html(index + 1)
            });
        }

    }
    _hub.client.showspeakerstab = function (status) {
        if (status) {
            $("#speakerstab").show()
        } else {
            $("#speakerstab").hide()
        }
    }
    _hub.client.showvottingstab = function () {
        if (status) {
            $("#vottingtab").show()
        } else {
            $("#vottingtab").show()
        }
    }
    _hub.client.starttspeakermeeting = function (userid, time) {
        var speakerid = "speakerid" + userid;
        var now = new Date();
        now.setMinutes(now.getMinutes() + time); // timestamp
        now = new Date(now);
        consolelog(now)
        var countDownDate = now.getTime();

        // Update the count down every 1 second
        var x = setInterval(function () {

            // Get today's date and time
            var now = new Date().getTime();

            // Find the distance between now and the count down date
            var distance = countDownDate - now;

            // Time calculations for days, hours, minutes and seconds
            var days = Math.floor(distance / (1000 * 60 * 60 * 24));
            var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
            var seconds = Math.floor((distance % (1000 * 60)) / 1000);

            // Output the result in an element with id="demo"
            document.getElementById(speakerid).innerHTML = seconds +": " + minutes ;

            // If the count down is over, write some text 
            if (distance < 0) {
                clearInterval(x);
                document.getElementById(speakerid).innerHTML = "انتهت المداخلة";
            }
        }, 1000);
    }
    _hub.client.exchangeSDP = async function (data, from_connid) {
        //alert(from_connid);
        await WrtcHelper.ExecuteClientFn(data, from_connid);
    };

    _hub.client.informAboutNewConnection = function (other_user_id, connId, other_userslength, user_iddata) {
        var divid = "#divattendee" + user_iddata
        debugger
        if ($("#pressencelist").find(divid).length == 0) {
            AddNewUser(other_user_id, connId, other_userslength, user_iddata);
        }
    };

    _hub.client.informAboutConnectionEnd = function (connId,userid) {
        $('#' + connId).remove();
        RemovewUser(connId, userid)
    };

    _hub.client.showChatMessage = function (from, message, time) {
        var div = $("<div>").text(from + '(' + time + '):' + message);
        $('#messages').append(div);
    }
    
    $.connection.hub.start(function () {
        console.log('connected to signal server.');
    }).done(function () {

        //WrtcHelper.init(serverFn, $.connection.hub.id);

        if (user_id != "" && meeting_id != "") {
            _hub.server.AddTask = function (task, TaskName, askUser, datepicker1, datepicker2) {
                console.log(task)
            }
            _hub.server.connect(user_id, meeting_id,user_id_data).done(function (other_users) {
                $('#divUsers .other').remove();
                var divid = "#divattendee" + user_id_data
                if ($("#pressencelist").find(divid).length == 0) {
                    AddNewUser(user_id, $.connection.hub.id, 0, user_id_data);
                }
               
                if (other_users.length != 0) {

                    for (var i = 0; i < other_users.length; i++) {
                        var divid = "#divattendee" + other_users[i].user_id_data
                        if ($("#pressencelist").find(divid).length == 0) {
                            AddNewUser(other_users[i].user_id, other_users[i].connectionId, other_users.length, other_users[i].user_id_data);
                        }
                        //WrtcHelper.createNewConnection(other_users[i].connectionId);
                    }
                } 
                $(".toolbox").show();
                $('#messages').show();
                $('#divUsers').show();
            });
            _hub.server.getconnectusers( meeting_id).done(function (other_userslength) {
                UpdateUserCount(other_userslength)
            });
        }
    });
    $.connection.hub.disconnected(function () {
        $.connection.hub.start();
    });
    $(".js-switch_3").change(function () {
        var statuss = this.checked
            _hub.server.startspeakers(meeting_id, this.checked).done(function (status) {
                if (statuss) {
                    $("#speakerstab").show()
                } else {
                    $("#speakerstab").hide();
                }
            });
        
    });
    $(".js-switch").change(function () {
        var statuss = this.checked
        _hub.server.startvotting(meeting_id, this.checked).done(function (other_userslength) {
            if (statuss) {
                $("#vottingtab").show()
            } else {
                $("#vottingtab").hide()
            }
        });

    });
    $('#Addtaskbtn').on('click', function () {
        _hub.server.sendtask(meeting_id, $("#Task").val(), $("#TaskUser").val(), $("#datepicker2").val(), $("#TaskUser option:selected").text()).done(function (other_userslength) {
            const TaskBody = document.getElementById("TaskBody");
            const newRow = TaskBody.insertRow(); // Insert a new row
            const cell1 = newRow.insertCell(0); // Insert count number cell
            const cell2 = newRow.insertCell(1); // Insert first name cell
            const cell3 = newRow.insertCell(2);
            const cell4 = newRow.insertCell(3);
            const cell5 = newRow.insertCell(4);
            cell1.innerHTML = $("#TaskBody").find("tr").length ;
            cell2.innerHTML = $("#Task").val();
            cell3.innerHTML = $("#TaskUser option:selected").text();
            cell4.innerHTML = $("#datepicker2").val()
            cell5.innerHTML = "<a i  data-type='2' data-id='' onclick='ShowDeleteModal(this)' class='btn btn-info btn-danger taskdeletebtn'>حذف</a>";
           
            AddTaskServerRequest(meeting_id, $("#Task").val(), $("#TaskUser").val(), $("#datepicker2").val());
            
        });
    });
    $('#Adddecisionbtn').on('click', function () {
        _hub.server.senddecision(meeting_id, $("#Ldecision").val(), $("#Ldecision option:selected").text(), $("#decision").val(), $("#datepicker3").val(), $("#datepicker4").val()).done(function (other_userslength) {
            const TaskBody = document.getElementById("DecisionBody");
            const newRow = TaskBody.insertRow(); // Insert a new row
            const cell1 = newRow.insertCell(0); // Insert count number cell
            const cell2 = newRow.insertCell(1); // Insert first name cell
            const cell3 = newRow.insertCell(2);
            const cell4 = newRow.insertCell(3);
            const cell5 = newRow.insertCell(4);
            const cell6 = newRow.insertCell(5);
            cell1.innerHTML = $("#DecisionBody").find("tr").length  ;
            cell2.innerHTML = $("#decision").val();
            cell3.innerHTML = $("#Ldecision option:selected").text();
            cell4.innerHTML = $("#datepicker3").val();
            cell5.innerHTML = $("#datepicker4").val();
            cell6.innerHTML = "<a i  data-type='1' data-id='' onclick='ShowDeleteModal(this)' class='btn btn-info btn-danger decisiondeletebtn'>حذف</a>";
            AddDecisionServerRequest(meeting_id, $("#Ldecision").val(), $("#decision").val(), $("#datepicker3").val(), $("#datepicker4").val());

        });
    });
    $('#SendVotebtn').on('click', function () {
        var votearray = [];
        $("input:checkbox[name=flexCheckDefault]:checked").each(function () {
            votearray.push($(this).val());
        });
        _hub.server.sendvote(meeting_id, user_id_data, votearray).done(function (other_userslength) {
            $("#vottingdiv").hide();
            $("#loaderdiv").show();
            AddVote();
            //if (other_userslength) {
            //    $("#loaderdiv").hide();
            //    GetVoteVoteResult(vottingConnections)
            //}
            //AddDecisionServerRequest(meeting_id, $("#Ldecision").val(), $("#decision").val(), $("#datepicker3").val(), $("#datepicker4").val());

        });
    });
    $('#StartSpeaker').on('click', function () {
        _hub.server.startspeaker(meeting_id, $(this).data("id"),$(this).data("time")).done(function (other_userslength) {
            var now = new Date();
            now.setMinutes(now.getMinutes() + $(this).data("time")); // timestamp
            now = new Date(now);
            var countDownDate = now.getTime();

            // Update the count down every 1 second
            var x = setInterval(function () {

                // Get today's date and time
                var now = new Date().getTime();

                // Find the distance between now and the count down date
                var distance = countDownDate - now;

                // Time calculations for days, hours, minutes and seconds
                var days = Math.floor(distance / (1000 * 60 * 60 * 24));
                var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
                var seconds = Math.floor((distance % (1000 * 60)) / 1000);

                // Output the result in an element with id="demo"
                $('#StartSpeaker').parent().html(minutes + ": " + seconds + " ");

                // If the count down is over, write some text 
                if (distance < 0) {
                    clearInterval(x);
                    $('#StartSpeaker').parent().html( "انتهت المداخلة");
                }
            }, 1000);
        });
    });
    
    $('#deletebtn').on('click', function () {
        if ($("#dataurl").val() == "1") {
            _hub.server.deletedecisionserver(meeting_id, $("#dataid").val(), $("#rang").val()).done(function (rang) {
                if (rang) {
                    $('#deleteModal').modal('hide')
                    $("#DecisionBody tr").slice(($("#rang").val() - 1), $("#rang").val()).remove();
                    $.each($("#DecisionBody tr").find("td:first"), function (index, item) {
                        $(this).html(index + 1)
                    });
                }
               

            })
        } else if ($("#dataurl").val() == "2") {
            _hub.server.deletetaskserver(meeting_id, $("#dataid").val(), $("#rang").val()).done(function (rang) {
                if (rang) {
                    $('#deleteModal').modal('hide')
                    $("#TaskBody tr").slice(($("#rang").val() - 1), $("#rang").val()).remove();
                    $.each($("#TaskBody tr").find("td:first"), function (index, item) {
                        $(this).html(index + 1)
                    });
                }


            })
        }

    })
}
function EventBinding(){
    $('#btnResetMeeting').on('click', function () {
        _hub.server.reset();
    });
   
    $('#btnsend').on('click', function () {
        _hub.server.sendMessage($('#msgbox').val());
        $('#msgbox').val('');
    });

    $('#divUsers').on('dblclick', 'video', function () {
        this.requestFullscreen();
    });
}
function AddNewUser(other_user_id, connId,other_userslength,useriddata) {
    var width = 90 / (other_userslength + 1);
    var height = 95;
    if((other_userslength + 1) / 3 > 1 )
    {
        height = height / (other_userslength + 1);
    }
    $('.boxcam').attr("style","box-shadow: 0px 3px 5px -1px rgb(0 0 0 / 20%), 0px 6px 10px 0px rgb(0 0 0 / 14%), 0px 1px 18px 0px rgb(0 0 0 / 12%);border-radius: 4px;background-color: #303030;width:"+width +"%;height:"+height+"%;")
    $('#otherTemplate').attr("style","box-shadow: 0px 3px 5px -1px rgb(0 0 0 / 20%), 0px 6px 10px 0px rgb(0 0 0 / 14%), 0px 1px 18px 0px rgb(0 0 0 / 12%);border-radius: 4px;background-color: #303030;display:none;width:"+width +"%;height:"+height+"%;")
    var $newDiv = $('#otherTemplate').clone();
        $newDiv = $newDiv.attr('id', connId).addClass('other');
        $newDiv.find('h2').text(other_user_id);
        $newDiv.find('video').attr('id', 'v_' + connId);
        $newDiv.find('audio').attr('id', 'a_' + connId);
        $newDiv.show();
    $('#divUsers').append($newDiv);
    var id = "#divattendee" + useriddata;
    var iddiv = "divattendee" + useriddata;
    var attendeediv = $(id).html();
    $(id).hide();
    $("#pressencelist").append("<div id=" + iddiv+">" + attendeediv +"</div>")

}
    function RemovewUser( connId, useriddata) {
        
        var id = "#divattendee" + useriddata;
        var attendeediv = $(id).html();
        $(id).show();
        $("#pressencelist").find(id).remove();

    }
    function Disconect() {
        _hub.server.OnDisconnected(true);
    }
    var colors = ["red","green","blue","brown","yellow","orange"]
    function GetVoteVoteResult(_vottingConnections, meeting_id) {
        
        
        $.ajax({
            type: "Post",
            url: "../Meeting/GetVottingResult",
            data: { votearray: _vottingConnections, mid: meeting_id },
        }).done(function (result) {
            $("#loaderdiv").hide();
            console.log(result);
            for (var i = 0; i < result.Item2.length; i++) {
                var xValues = []
                var yValues = []
                var barColors = [];
                var filterednames = result.Item1.filter(function (obj) {
                    return (obj.PollId === result.Item2[i]);
                });
                $.each(filterednames, function (index, item) {
                    xValues.push(item.Polloptionvalue)
                    yValues.push(item.Count)
                    barColors.push(colors[index])
                });
                var chartid = "myChart" + result.Item2[i]
                new Chart(chartid, {
                    type: "bar",
                    data: {
                        labels: xValues,
                        datasets: [{
                            backgroundColor: barColors,
                            data: yValues
                        }]
                    },
                    options: {
                        legend: { display: false },
                        title: {
                            display: true,
                            text: filterednames[0].PollTitle
                        }
                    }
                });
                $(".chartdiv").show();
            }
            
        }).fail(function (data) {
            // Optionally alert the user of an error here...
        });
    }
    function AddVote() {
        var votearray = [];
        $("input:checkbox[name=flexCheckDefault]:checked").each(function () {
            votearray.push($(this).val());
        });
        $.ajax({
            type: "Post",
            url: "../Meeting/AddVote",
            data: { votearray: votearray,userid : user_id_data },
    }).done(function (result) {
    }).fail(function (data) {
        // Optionally alert the user of an error here...
    });
    }
function UpdateUserCount(other_userslength){
    $("#usercount").html("Participants: " + other_userslength.length)
    for (var i = 0; i < other_userslength.length; i++) {
        var divid = "#divattendee" + other_userslength[i].user_id_data;
        if ($("#pressencelist").find(divid).length != 0) {
            var attendeediv = $(divid).html();
            $(divid).hide();
            $("#pressencelist").append(attendeediv)
        }
    }
}
    function AddTaskServerRequest(mid, task, taskuser, date) {
        $.ajax({
            type: "Post",
            url: "../Task/Add",
            data: { meetingid: mid, Task: task, TaskUser: taskuser, datepicker : date},
        }).done(function (result) {
            $('#TaskBody tr:last').find(".taskdeletebtn").data("id", result);
        }).fail(function (data) {
            // Optionally alert the user of an error here...
        });
    }
    function AddDecisionServerRequest( meetingid,  boardId,  title,  startDate,  createdDat) {
        $.ajax({
            type: "Post",
            url: "../Decision/Add",
            data: { meetingid: meetingid, boardId: boardId, title: title, startDate: startDate, createdDate: createdDat },
        }).done(function (result) {
            
            $('#DecisionBody tr:last').find(".decisiondeletebtn").data("id", result);

        }).fail(function (data) {
            // Optionally alert the user of an error here...
        });
    }
   
return {

    _init: function(uid,mid,useriddata){
        init(uid, mid, useriddata);
        $(window).bind('beforeunload', function () {
            Disconect();
            return 'Are you sure you want to leave?';
        });
    }

};

}());
