
(function ($) {
    "use strict";


    /*==================================================================
    [ Focus Contact2 ]*/
    $('.input100').each(function(){
        $(this).on('blur', function(){
            if($(this).val().trim() != "") {
                $(this).addClass('has-val');
            }
            else {
                $(this).removeClass('has-val');
            }
        })    
    })
  
  
    /*==================================================================
    [ Validate ]*/
    var input = $('.validate-input .input100');

    $('.validate-form').on('submit',function(){
        var check = true;

        for(var i=0; i<input.length; i++) {
            if(validate(input[i]) == false){
                showValidate(input[i]);
                check=false;
            }
        }

        return check;
    });


    $('.validate-form .input100').each(function(){
        $(this).focus(function(){
           hideValidate(this);
        });
    });

    function validate (input) {
        if($(input).attr('type') == 'email' || $(input).attr('name') == 'email') {
            if($(input).val().trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/) == null) {
                return false;
            }
        }
        else {
            if($(input).val().trim() == ''){
                return false;
            }
        }
    }

    function showValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).addClass('alert-validate');
    }

    function hideValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).removeClass('alert-validate');
    }


    

})(jQuery);

/**
 * USer ResetPass
 */

function validateResetPass() {
    let newpassbox = document.getElementById("pwd")
    let newpasscfbox = document.getElementById("cnpwd")
    let messageBox = document.getElementById("message")

    let newpass = newpassbox.value;
    let newpasscf = newpasscfbox.value;

    if (newpass === "") {
        messageBox.innerHTML = "Please enter your new password";
        newpassbox.focus();
        return false;
    } else if (newpasscf === "") {
        messageBox.innerHTML = "Please enter your new confirm password";
        newpasscfbox.focus();
        return false;
    } else if (newpass.length < 6) {
        messageBox.innerHTML = "Your password must contain at least 6 characters";
        newpassbox.focus();
        return false;
    } else if (newpass != newpasscf) {
        messageBox.innerHTML = "Your passwords do not match";
        return false;
    }
    messageBox.innerHTML = "";
    return true;
}