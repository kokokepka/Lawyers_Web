$(document).ready(function() {
  $('.nav-link-collapse').on('click', function() {
    $('.nav-link-collapse').not(this).removeClass('nav-link-show');
    $(this).toggleClass('nav-link-show');
  });
});

$(document).ready(function () {
    $('#showGame').click(function () {
        var url = $('#gameModal').data('url');

        $.get(url, function (data) {
            $('#gameContainer').html(data);

            $('#gameModal').modal('show');
        });
    });
});
