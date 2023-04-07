function tabs(selector) {
    var tabs = $(selector);
    var span = tabs.children('span');

    tabs
        .addClass('tabs')
        .prepend('<div class = "tabs_controls"></div>')
        .append('<div class = "tabs_panels"></div>');

    span
        .prepandTo(selector + ' .tabs_controls');
        .each(function (i) {

            var span = $(this);

            if (!i) {
                span.replaceWidth(
                    '<a href="#" class="tabs_control tabs_control_active">'
                    + span.text() +
                    '</a>');
            }
            else {
                span.replaceWidth(
                    '<a href="#" class="tabs_control">'
                    + span.text() +
                    '</a>'
                );
            }
        });
}

tabs('#tab');