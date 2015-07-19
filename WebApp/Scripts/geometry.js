(function ($) {
    $(document).ready(function () {
        var r1 = {};
        r1.id = 'r1';
        var r2 = {};
        r2.id = 'r2';
        initRectangles();
        $('.box').draggable({
            start: function (event, ui) {
                updateIntersect();
            },
            drag: function (event, ui) {
                var snapTolerance = $(this).draggable('option', 'snapTolerance');
                var topRemainder = ui.position.top % 20;
                var leftRemainder = ui.position.left % 20;

                if (topRemainder <= snapTolerance) {
                    ui.position.top = ui.position.top - topRemainder;
                }

                if (leftRemainder <= snapTolerance) {
                    ui.position.left = ui.position.left - leftRemainder;
                }

                if (this.id === 'bigguy') {
                    updateLoc(r1, ui.position);
                }
                else if (this.id == 'smallguy') {
                    updateLoc(r2, ui.position);
                }
            },
            stop: function (event, ui) {
                if (this.id === 'bigguy') {
                    updateLoc(r1, ui.position);
                }
                else if (this.id == 'smallguy') {
                    updateLoc(r2, ui.position);
                }
                checkRels();
            }
        });

        function updateLoc(r, loc) {
            r.Top = loc.top;
            r.Left = loc.left;
            updateDims(r);
        }

        function initRectangles() {
            r1.Top = 100;
            r1.Left = 320;
            r1.Width = 80;
            r1.Height = 60;
            r1.el = $('#bigguy');
            updateDims(r1);

            $('#r1w').change(function () {
                r1.Width = $(this).val();
                updateDims(r1);
            });
            $('#r1h').change(function () {
                r1.Height = $(this).val();
                updateDims(r1);
            });

            r2.Top = 80;
            r2.Left = 180;
            r2.Width = 20;
            r2.Height = 40;
            r2.el = $('#smallguy');
            updateDims(r2);

            $('#r2w').change(function () {
                r2.Width = $(this).val();
                updateDims(r2);
                checkRels();
            });
            $('#r2h').change(function () {
                r2.Height = $(this).val();
                updateDims(r2);
                checkRels();
            });

            $('#intersect').click(function () {
                updateIntersect();
            });
            checkRels();
        }

        function updateDims(r) {
            console.log("updateDims", r);
            r.el.css({
                'width': r.Width + 'px',
                'height': r.Height + 'px',
                'top': r.Top + 'px',
                'left': r.Left + 'px'
            });

            //bind values
            $('#' + r.id + 'w').val(r.Width);
            $('#' + r.id + 'h').val(r.Height);
            $('#' + r.id + 't').html(r.Top);
            $('#' + r.id + 'l').html(r.Left);
        }

        function checkRels() {
            var m = {};
            m.r1 = r1;
            m.r2 = r2;
            $.ajax({
                url: '/Home/Analyze',
                type: "POST",
                dataType: "json",
                data: JSON.stringify(m),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.Success == true) {
                        updateMessages(data.Header, data.Message);
                        updateIntersect(data.Diff);
                    }
                    else {
                        updateMessages(data.Header, data.Message);
                    }
                },
                error: function (jqXHR, exception) {
                    updateMessages("We've got issues. Code " + jqXHR.status, exception);
                }
            });
        }

        function updateMessages(status, msg) {
            $('#status').html(status);
            $('#msg').html(msg);
        }

        function updateIntersect(diff) {
            $('#intersect').width(0);
            $('#intersect').height(0);
            $("#intersect").addClass("borderless");

            if (diff) {
                if (diff.Width == 0) {
                    diff.Width = 1;
                }
                if (diff.Height == 0) {
                    diff.Height = 1;
                }
                $('#intersect').css({
                    'width': diff.Width + 'px',
                    'height': diff.Height + 'px',
                    'top': diff.Top + 'px',
                    'left': diff.Left + 'px',
                    'border': 'solid red 1px',
                    'background': '#FF4000',
                    'z-index': '25',
                    'position': 'absolute'
                });
            }
        }
    });
})(jQuery); 