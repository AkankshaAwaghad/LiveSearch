$(document).ready(function () {    
    $("#txtCustName").focus();

    $("#txtCustName").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Customer/SearchCustomer",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {

                        return { label: item.CustName, value: item.CustName, custId: item.CustId, mobile: item.Mobile, address: item.Address, email: item.Email };
                    }))
                }
            })
        },
        minLength: 2,
        select: function (event, ui) {
            $("#txtMobile").val(ui.item.mobile);
            $("#txtAddress").val(ui.item.address);
            $("#txtCustId").val(ui.item.custId);
            $("#txtEmail").val(ui.item.email);
        }

    });
})

var saveCustomer = function () {
      var custId = $("#CustId").val();
      var custname = $("#txtCustName").val();
      var address = $("#txtAddress").val();
      var mobile = $("#txtMobile").val();
      var email = $("#txtEmail").val();

      var model = {
          CustId: custId,
          CustName: custname,
          Address: address,
          Mobile: mobile,
          Email: email,
      };

    $.ajax({
        url: "/Customer/SaveCustomer",
        method: "Post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        datatype: "json",

        success: function (response) {
            alert("Successfull");

        }
    })
  }

