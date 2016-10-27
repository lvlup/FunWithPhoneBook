$(document).ready(function ()
{
    $("#exportButton").click(function ()
    {
        var url = $(this).data("url") + "?searchString=" + $("#searchBox").val();
        window.open(url, '_blank');
    });

    var timer;
    $(document).on('keyup', "#searchBox", function (event) {
        var $searchtextBox = $(this);
        clearTimeout(timer);
        timer = setTimeout(function ()
        {
            $("#divProcessing").show();
            $.get(
            {
                url: $searchtextBox.data("url"),
                data: { searchString: $searchtextBox.val() }
            }).done(function (data)
            {
                $('#PhoneContactsTable').html(data);
                $("#searchBox").val($searchtextBox.val()).focus();
            }).fail(function (jqXhr, textStatus)
            {
                alert("Something went wrong: " + textStatus + ".");
            }).always(function ()
            {
                $("#divProcessing").hide();
            });
    }, 400);

    });  
});