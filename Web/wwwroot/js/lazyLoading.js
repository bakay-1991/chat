
let page = 0,
	inCallback = false,
	_isAllLoaded = false;;

let scrollHandler = function () {
	if ($('.msg_history').scrollTop() <= 1) {
		console.log('(msg_history).scrollTop() = ' + $('.msg_history').scrollTop());
		loadProjectData("Home/GetUsers");
	}
}

function loadProjectData(loadMoreRowsUrl) {
	if (page > -1 && !inCallback) {
		inCallback = true;
		page++;

		$.ajax({
			type: 'GET',
			url: loadMoreRowsUrl,
			data: "page=" + page,
			success: function (data, textstatus) {
				if (data != '') {
					$('.msg_history').prepend('<div class="outgoing_msg"><div class="sent_msg"><p>Apollo University, Delhi, India Test</p><span class="time_date">11: 01 AM</span></div></div>');
				}
				else {
					page = -1;
				}

				inCallback = false;
				$('.msg_history').animate({ scrollTop: 2 }, 100);
			},
			error: function (XMLHttpRequest, textStatus, errorThrown) {
				alert(errorThrown);
			}
		});
	}
}