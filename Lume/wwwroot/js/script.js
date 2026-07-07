document.addEventListener('DOMContentLoaded', function() {
    // Notification button and dropdown
    const notificationBtn = document.querySelector('[data-notification-btn]');
    const notificationDropdown = document.querySelector('[data-notification-dropdown]');

    if (notificationBtn && notificationDropdown) {
        notificationBtn.addEventListener('click', function(e) {
            e.stopPropagation();
            notificationDropdown.classList.toggle('hidden');
            profileDropdown.classList.add('hidden');
        });
    }

    // Profile button and dropdown
    const profileBtn = document.querySelector('[data-profile-btn]');
    const profileDropdown = document.querySelector('[data-profile-dropdown]');

    if (profileBtn && profileDropdown) {
        profileBtn.addEventListener('click', function(e) {
            e.stopPropagation();
            profileDropdown.classList.toggle('hidden');
            notificationDropdown.classList.add('hidden');
        });
    }

    // Close dropdowns when clicking outside
    document.addEventListener('click', function() {
        if (notificationDropdown) notificationDropdown.classList.add('hidden');
        if (profileDropdown) profileDropdown.classList.add('hidden');
    });
});