 var arr = [];
    var count;
    $(document).ready(function (id) {

        $('.minus').click(function () {
            var $input = $(this).parent().find('input');
            count = parseInt($input.val()) - 1;
            count = count < 1 ? 1 : count;
            $input.val(count);
            $input.change();
            return false;
        });

        $('.plus').click(function (id) {
            var $input = $(this).parent().find('input');
            $input.val(parseInt($input.val()) + 1);
            $input.change();
            return false;
        });
    });

    $(document).ready(function () {
        update();
        $(".quant").change(function () {
            //this: context of the input that was changed
            //console.log('calling /Cart/AddTocart; id:', $(this).attr('data-id'), ' quantity: ', $(this).val());
            $.get('/Cart/AddTocart', {
                id: $(this).attr('data-id'),
                returnUrl: '',
                quantity: $(this).val()
            });
            update();
        });

        function update() {
            var sum = 0.0;
            var quantity;
            $('#myTable > tbody  > tr').each(function () {
                var rp = parseFloat($('#rp').text().replace('-', ''))
                quantity = $(this).find('.quant').val();
                var price = parseFloat($(this).find('.price').attr('data-price').replace(',', '.'));
                var amount = (quantity * price);
                sum += amount;
                rewardsum =sum-rp
                $(this).find('.amount').text('₹' + amount);
            });

            const url = window.location.href;

            if (url.includes('?')) {
                $(".hideme").show();
                $('.mytotal').text('₹' + sum);
                $('.total').text('₹' + rewardsum)
            }
            else {
                $(".hideme").hide();
                 $('.total').text('₹' + sum);
            }

        }
    });


    function GetDelete(id) {

        document.getElementById("btnContinueDelete").href = "/Cart/DeleteProduct/" + id;
    }

    function GetOrder() {

        var ar = [];
        var dataBunch = [];

        var tab = $("#myTable > tbody > tr").each(function () {
            $(this).find('td');
        });

        for (var i = 0; i < tab.length; i++) {

           // dataBunch = [(tab[i].cells[0].childNodes[1].childNodes[0].currentSrc), (tab[i].children[0].children[1].value), (tab[i].cells[1].children[0].childNodes[0].innerText), (tab[i].cells[1].children[1].innerText), (tab[i].cells[2].children[0].children[1].value)];
           // "Image": tab[i].cells[0].childNodes[1].childNodes[0].currentSrc, "ProductName": tab[i].cells[1].children[0].childNodes[0].innerText, "Price": tab[i].cells[1].children[1].innerText,
            ar.push({ "Guid": tab[i].children[0].children[2].value, "Qty": tab[i].cells[2].children[0].children[1].value, "ProductId": tab[i].cells[0].children[0].value });
        }

        var makeString = JSON.stringify({ar});
        const url = window.location.href;  
        $.ajax({
            type: "POST",
            url: '@Url.Action("AddToCheck", "Cart")',
            dataType: 'JSON',
            data: makeString,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (url.includes('?')) {
                    window.location = "/Cart/CheckOut?orderid=" + $('.HidenGuid').val() +'&'+'reward'
                }
                else { window.location = "/Cart/CheckOut?orderid=" + $('.HidenGuid').val()}
            },
            error: function (xhr, status, error) {
                alert( window.location = "/Cart/CheckOut/" + $('.HidenGuid').val())
            }
        });

    };