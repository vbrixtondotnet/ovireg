$(document).ready(function () {
    var results = [];

    function compare(a, b) {
        if (a.FightNo < b.FightNo) {
            return -1;
        }
        if (a.FightNo > b.FightNo) {
            return 1;
        }
        return 0;
    }

    var sendResults = function () {
        var allResults = results.sort(compare);
        $("#btnHarvest").addClass('disabled');
        $.ajax({
            type: "POST",
            url: "https://onlinesabongresult.azurewebsites.net/api/harvest/fightresults",
            data: JSON.stringify(allResults),
            dataType: 'json',
            contentType: "application/json",
            success: function (data) {
                $("#btnHarvest").removeClass('disabled');
            }
        });
    }

    addHarvestButton = function () {
        var button = '<button type="button" class="btn btn-warning btn-sm btn-block" id="btnHarvest" style="margin-top:-12px;">HARVEST</button >';
        $('.col.p-0.text-center.pt-3.dark-right-border.dark-bg.mt-3').html(button);

        $("#btnHarvest").on('click', function () {
            harvest();
            sendResults();
        });
    }


    var harvest = function () {
        results = [];
        $("#box-history-automatic").find('.badge').each(function () {
            var result = $(this);
            var ringside = result.hasClass('badge-primary') ? 'WALA' : result.hasClass('badge-success') ? 'DRAW' : 'MERON';
            var fightNo = result.html();
            var res = { Id: 0, RingSide: ringside, FightNo: parseInt(fightNo), UserId: 1 }
            results.push(res);
        });
    }

    addHarvestButton();


});