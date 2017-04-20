HTML_TEXT = '<div class="item text"><h4>Text</h4><a>x</a><div><input class="lbl required" type="text" placeholder="label"></div></div>';
HTML_NUM = '<div class="item number"><h4>Number</h4><a>x</a><div><input class="lbl required" type="text" placeholder="label"></div></div>';
HTML_SPIN = '<div class="item spinner"><h4>Spinner</h4><a>x</a><input class="lbl required" type="text" placeholder="label"><div class="option"><div class="option-items"></div></div><span class="btn btn-primary add">Add Option</span></div>'
HTML_DATE = '<div class="item date"><h4>Date Picker</h4><a>x</a><div><input class="lbl required" type="text" placeholder="label"></div></div>';
HTML_REF = '<div class="item ref"><h4>Reference Field</h4><a>x</a><div><input class="lbl required" type="text" placeholder="label"></div></div>';

function rebuildPreview() {
    parseJsonForm();
    if (jsonForm) {
        buildPreview(jsonForm);
    }
}

function buildPreview(json) {
    var preview = $('.preview').empty();
    preview.append('<h3>' + json.title + '</h3>');
    preview.append('<h4>' + json.description + '</h4>');
    for (var i = 0; i < json.fobjects.length; i++) {
        var item = json.fobjects[i];
        var label = item.label.length ? ('<label>' + item.label + '</label>') : '<div class="nolabel">(no label)</div>';
        if (item.type_id == 10) {
            preview.append('<div>' + label + '<input type="text" /></div>');
        } else if (item.type_id == 20) {
            preview.append('<div>' + label + '</label><input type="number"/></div>');
        } else if (item.type_id == 30) {
            preview.append('<div>' + label + createSpinner(item.options) + '<div>');
        } else if (item.type_id == 40) {
            preview.append('<div>' + label + '<input type="text" class="datepicker" /></div>');
        } else if (item.type_id == 50) {
            preview.append('<div>' + label + '<select><option>' + item.table_name + ' values</option></select>')
        }
    };
}

function loadJsonForm(jf) {
    $('form.dynamic').attr('data-id', jf.id);
    $('.form-head input').val(jf.title);
    $('.form-head textarea').val(jf.description);
    for (var i = 0; i < jf.fobjects.length; i++) {
        switch (jf.fobjects[i].type_id) {
            case 10:
                var html = $(HTML_TEXT);
                html.find('.lbl').val(jf.fobjects[i].label);
                html.find('a').click(function () {
                    $(this).closest('.item').remove();
                    rebuildPreview();
                });
                $('.designer').append(html);
                break;
            case 20:
                var html = $(HTML_NUM);
                html.find('.lbl').val(jf.fobjects[i].label);
                html.find('a').click(function () {
                    $(this).closest('.item').remove();
                    rebuildPreview();
                });
                $('.designer').append(html);
                break;
            case 30:
                var html = $(HTML_SPIN);
                html.find('.lbl').val(jf.fobjects[i].label);
                html.find('span.add').click(spinnerAddOptionClick);
                for (var j = 0; j < jf.fobjects[i].options.length; j++) {
                    var tmp = createNewOption(jf.fobjects[i].options[j].text);
                    html.find('.option-items').append(tmp);
                }
                html.find('a').click(function () {
                    $(this).closest('.item').remove();
                    rebuildPreview();
                });
                $('.designer').append(html);
                break;
            case 40:
                var html = $(HTML_DATE);
                html.find('.lbl').val(jf.fobjects[i].label);
                html.find('a').click(function () {
                    $(this).closest('.item').remove();
                    rebuildPreview();
                });
                $('.designer').append(html);
                break;
            case 50:
                var html = $(HTML_REF);
                var bsBase = $('#bsBase').clone().removeAttr('id').show()[0].outerHTML;
                html.append(bsBase);
                html.find('select').val(jf.fobjects[i].table_name);
                html.find('.lbl').val(jf.fobjects[i].label);
                html.find('a').click(function () {
                    $(this).closest('.item').remove();
                    rebuildPreview();
                });
                $('.designer').append(html);
                break;
            default:
        }
    }
    rebuildPreview();
}

function parseJsonForm() {
    jsonForm = {};
    jsonForm.id = $('form.dynamic').attr('data-id');
    jsonForm.fobjects = [];
    jsonForm.title = $('.form-head input').val();
    jsonForm.description = $('.form-head textarea').val();
    $('.designer > div').each(function () {
        var div = $(this);
        var label = $('.lbl', div).val();
        var ref = $('select', div).val();
        if (div.hasClass('text')) {
            jsonForm.fobjects.push({ 'type_id': 10, 'label': label });
        } else if (div.hasClass('number')) {
            jsonForm.fobjects.push({ 'type_id': 20, 'label': label });
        } else if (div.hasClass('spinner')) {
            var list = $('.option-items input', div).filter(function (x) { return $(this).val().length; }).toArray().map(function (x) { return { text: $(x).val() } });
            jsonForm.fobjects.push({ 'type_id': 30, 'label': label, 'options': list });
        } else if (div.hasClass('date')) {
            jsonForm.fobjects.push({ 'type_id': 40, 'label': label });
        } else if (div.hasClass('ref')) {
            jsonForm.fobjects.push({ 'type_id': 50, 'label': label, 'table_name': ref });
        }
    });
}

function createSpinner(options) {
    var s = '<select>';
    for (var i = 0; i < options.length; i++) {
        s += '<option>' + options[i].text + '</option>';
    }
    return s + '</select>'
}

function saveForm(e) {
    e.preventDefault();
    if ($('form.dynamic').valid()) {
        parseJsonForm();
        if (formMode == 'edit') {
            $.post(location.protocol + '//' + location.host + '/dynamicform/edit', jsonForm, function () {
                window.location = location.protocol + '//' + location.host + '/dynamicform';
            });
        } else if (formMode == 'create') {
            $.post(location.protocol + '//' + location.host + '/dynamicform/save', jsonForm, function () {
                window.location = location.protocol + '//' + location.host + '/dynamicform';
            });
        }
    }
}

function spinnerAddOptionClick() {
    var scope = $(this).closest('.item.spinner');
    scope.find('.option-items').append(createNewOption());
}

function createNewOption(value) {
    value = value || '';
    var newOption = $('<div class="option-item"><a>x</a><input class="required" type="text" placeholder="option text" value="' + value + '" /></div>');
    newOption.find('input').keyup(rebuildPreview).attr('name', generateId());
    newOption.find('a').click(function () {
        $(this).closest('.option-item').remove();
        rebuildPreview();
    });
    return newOption;
}

function generateId() {
    return '_' + Math.random().toString(36).substr(2, 9);
}

$(function () {
    $('.dynamic input, .dynamic textarea').keyup(rebuildPreview);
    $(".simple").click(function () {
        var type = $(this).attr('data-type');
        var html = "";
        if (type == 'text') {
            html = $(HTML_TEXT);
            $('.designer').append(html);
        } else if (type == 'number') {
            html = $(HTML_NUM);
            $('.designer').append(html);
        } else if (type == 'spinner') {
            html = $(HTML_SPIN);
            $('.designer').append(html);
            $('span.add', html).click(spinnerAddOptionClick);
        } else if (type == 'date') {
            html = $(HTML_DATE);
            $('.designer').append(html);
        } else if (type == 'ref') {
            var bsBase = $('#bsBase').clone().removeAttr('id').show()[0].outerHTML;
            html = $(HTML_REF);
            html.append(bsBase);
            $('.designer').append(html);
        }

        rebuildPreview();
        $('input', html).keyup(rebuildPreview);
        $('select', html).change(rebuildPreview);
        $('a', html).click(function () {
            $(this).closest('.item').remove();
            rebuildPreview();
        });
        
        var i = 1;
        $('input, textarea').each(function () {
            $(this).attr('name', 'name' + i++);
        });

    });
});