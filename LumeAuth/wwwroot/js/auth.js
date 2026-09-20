// LumeAuth page behaviour: show/hide password and the password strength meter.
// Plain JavaScript, no dependencies. Form validation itself is handled by
// jQuery Validate + unobtrusive (loaded in _AuthLayout).
(function () {
    'use strict';

    // ---- Show / hide password ----
    document.querySelectorAll('[data-toggle-password]').forEach(function (button) {
        var input = document.getElementById(button.getAttribute('data-toggle-password'));
        var icon = button.querySelector('ion-icon');
        if (!input) { return; }

        button.addEventListener('click', function () {
            var show = input.type === 'password';
            input.type = show ? 'text' : 'password';

            button.setAttribute('aria-label', show ? 'Hide password' : 'Show password');
            button.setAttribute('aria-pressed', show ? 'true' : 'false');
            if (icon) { icon.setAttribute('name', show ? 'eye-off-outline' : 'eye-outline'); }
        });
    });

    // ---- Password strength meter (visual hint only, the server rules are the real check) ----
    var levelNames = ['', 'Weak', 'Fair', 'Good', 'Strong'];

    function scorePassword(value) {
        if (!value) { return 0; }

        var score = 0;
        if (value.length >= 8) { score++; }
        if (/[a-z]/.test(value) && /[A-Z]/.test(value)) { score++; }
        if (/\d/.test(value)) { score++; }
        if (/[^A-Za-z0-9]/.test(value) || value.length >= 14) { score++; }

        return Math.max(score, 1);
    }

    document.querySelectorAll('[data-strength-for]').forEach(function (meter) {
        var input = document.getElementById(meter.getAttribute('data-strength-for'));
        var label = meter.querySelector('[data-strength-label]');
        if (!input) { return; }

        input.addEventListener('input', function () {
            var level = scorePassword(input.value);

            meter.setAttribute('data-level', String(level));
            meter.classList.toggle('is-active', level > 0);
            if (label) { label.textContent = level > 0 ? 'Password strength: ' + levelNames[level] : ''; }
        });
    });
})();

// Birthday rule in the browser. Mirrors BirthDateAttribute on the server and reads its
// messages from the data-val-birthdate-* attributes, so the wording lives in one place (C#).
// The server stays the source of truth: this only gives instant feedback.
(function ($) {
    'use strict';

    if (!$ || !$.validator || !$.validator.unobtrusive) { return; }

    $.validator.addMethod('birthdate', function (value, element, params) {
        var form = $(element).closest('form');
        var year = parseInt(value, 10);
        var month = parseInt(form.find('[name="' + params.month + '"]').val(), 10);
        var day = parseInt(form.find('[name="' + params.day + '"]').val(), 10);
        var fail = function (message) { $(element).data('birthdateMessage', message); return false; };

        if (!year || !month || !day) { return fail(params.missing); }

        // Building the date and reading it back rejects dates that do not exist, like 31 February.
        var date = new Date(year, month - 1, day);
        if (date.getFullYear() !== year || date.getMonth() !== month - 1 || date.getDate() !== day) {
            return fail(params.invalid);
        }

        var today = new Date();
        if (date > today) { return fail(params.invalid); }

        var age = today.getFullYear() - year;
        if (today.getMonth() < month - 1 || (today.getMonth() === month - 1 && today.getDate() < day)) { age--; }
        if (age < params.minimumage) { return fail(params.tooyoung); }

        return true;
    }, function (params, element) {
        return $(element).data('birthdateMessage') || params.missing;
    });

    $.validator.unobtrusive.adapters.add('birthdate',
        ['month', 'day', 'minimumage', 'missing', 'invalid', 'tooyoung'],
        function (options) {
            options.rules['birthdate'] = {
                month: options.params.month,
                day: options.params.day,
                minimumage: parseInt(options.params.minimumage, 10),
                missing: options.params.missing,
                invalid: options.params.invalid,
                tooyoung: options.params.tooyoung
            };
        });

    // The rule sits on the year select. Re-check it when the month or day changes, otherwise an
    // old error stays on screen after the user fixes the other two selects.
    $(function () {
        $('[data-birthday]').each(function () {
            var group = $(this);

            group.on('change', 'select', function () {
                var validator = group.closest('form').data('validator');
                var yearSelect = group.find('select[data-val-birthdate]')[0];
                if (!validator || !yearSelect) { return; }

                var allChosen = group.find('select').toArray().every(function (s) { return s.value !== ''; });
                if (allChosen || $(yearSelect).hasClass('input-validation-error')) {
                    validator.element(yearSelect);
                }
            });
        });
    });
})(window.jQuery);