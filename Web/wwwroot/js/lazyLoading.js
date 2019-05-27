let messagePage = 0,
	allMessageLoaded = false,
	receiverPage = 1,
	allReceiverLoaded = false,
	inCallback = false,
	messageReceiverId = $('.active_chat').attr('id');

function messageScrollHandler() {
	if (!allMessageLoaded && $(this).scrollTop() === 0 && !inCallback) {
		inCallback = true;
		let receiverId = $('.active_chat').attr('id');
		loadMessages(receiverId, ++messagePage, () => { $('.msg_history').animate({ scrollTop: 2 }, 100); });
	}
}

function loadMessages(receiverId, page, successCallback) {
	inCallback = true;
	if (messageReceiverId !== receiverId) {
		messageReceiverId = receiverId;
		allMessageLoaded = false;
	}
	messagePage = page;
	$.ajax({
		type: 'GET',
		url: 'api/Message/Get',
		data: `receiverId=${receiverId}&page=${page}`,
		success: function (data, textstatus) {
			if (data.length === 0) {
				allMessageLoaded = true;
			}
			for (var i = 0; i < data.length; i++) {
				$('.msg_history').prepend(getMessageHtml(data[i]));
			}

			inCallback = false;

			if (successCallback) {
				successCallback();
			}
		},
		error: function (XMLHttpRequest, textStatus, errorThrown) {
			alert(errorThrown);
		}
	});
}

function getMessageHtml(message) {
	if (message.sender) {
		return `<div class="incoming_msg">
					<div class="incoming_msg_img">${message.sender}</div>
					<div class="received_msg">
						<div class="received_withd_msg">
							<p>
								${message.text}
							</p>
							<span class="time_date">${message.created}</span>
						</div>
					</div>
				</div>`;
	}
	else {
		return `<div class="outgoing_msg">
					<div class="sent_msg">
						<p>
							${message.text}
						</p>
						<span class="time_date">${message.created}</span>
					</div>
				</div>`;
	}
}

function receiverScrollHandler() {
	if (!allReceiverLoaded && $(this).scrollTop() + $(this).innerHeight() + 10 >= $(this)[0].scrollHeight) {
		loadReceivers(receiverPage++);
	}
}

function loadReceivers(page) {
	$.ajax({
		type: 'GET',
		url: 'api/Receiver/Get',
		data: `page=${page}`,
		success: function (data, textstatus) {
			if (data.length === 0) {
				allReceiverLoaded = true;
			}
			for (var i = 0; i < data.length; i++) {
				$('.inbox_chat').append(getReceiverHtml(data[i]));
			}
		},
		error: function (XMLHttpRequest, textStatus, errorThrown) {
			alert(errorThrown);
		}
	});
}

function getReceiverHtml(receiver) {
	return `<div id="${receiver.id}" class="chat_list">
				<div class="chat_people">
					<div class="chat_img"> <img src="${receiver.iconUrl}"> </div>
						<div class="chat_ib">
						<h5>
							${receiver.name}
						</h5>
					</div>
				</div>
			</div>`;
}