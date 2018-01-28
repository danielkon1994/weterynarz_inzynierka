var AppNotify = AppNotify || {};

AppNotify.tempNotifyMessage = function (tempObj) {
    if (!!tempObj)
    { 
        try {        
            var notification = JSON.parse(tempObj);
            if (!!notification) {
                var messageType = null;
                switch (notification.MessageStatus) {
                    case 1:
                        messageType = 'error';
                        break;
                    case 2:
                        messageType = 'success'
                        break;
                    case 3:
                        messageType = 'warning'
                        break;
                    case 4:
                        messageType = 'info'
                        break;
                    case 5:
                        messageType = 'question'
                        break;
                }
                swal(notification.Text, notification.OptionalText, messageType);
            }
        }
        catch (ex) {
            console.log(ex);
        }
    }
};

AppNotify.notifyMessage = function (obj, cb) {
    try {
        if (!!obj) {
            var messageType = null;
            switch (obj.messageStatus)
            {
                case 1:
                    messageType = 'error';
                    break;
                case 2:
                    messageType = 'success'
                    break;
                case 3:
                    messageType = 'warning'
                    break;
                case 4:
                    messageType = 'info'
                    break;
                case 5:
                    messageType = 'question'
                    break;
            }
            swal(obj.text, obj.optionalText, messageType, cb());
        }
    }
    catch (ex) {
        console.log(ex);
    }
};

AppNotify.showMessage = function (text, message, type, cb) {
    try {
        switch (type) {
            case 1:
                messageType = 'error';
                break;
            case 2:
                messageType = 'success'
                break;
            case 3:
                messageType = 'warning'
                break;
            case 4:
                messageType = 'info'
                break;
            case 5:
                messageType = 'question'
                break;
        }
        swal(text, message, type, cb());
    }
    catch (ex) {
        console.log(ex);
    }
};

AppNotify.confirm = function (message, callback) {
    swal({
        title: 'Jesteś pewny?',
        text: message,
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Tak, usuń!',
        cancelButtonText: 'Nie, nic nie rób!',
        confirmButtonClass: 'btn btn-success',
        cancelButtonClass: 'btn btn-danger',
        buttonsStyling: false,
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            callback();
        }
    });
};