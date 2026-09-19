// LumeAuth page behaviour: show/hide password and the password strength meter.
// Plain JavaScript, no dependencies. Form validation itself is handled by
// jQuery Validate + unobtrusive (loaded in _AuthLayout).
(function () {
    'use strict';

    // ---- Show / hide password ----
    document.querySelectorAll('[data-toggle-password]').forEach(function (button) {
        var input = document.getElementById(button.getAttribute('data-toggle-password'));
        var icon  = button.querySelector('ion-icon');
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
