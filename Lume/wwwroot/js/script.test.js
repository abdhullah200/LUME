/**
 * Tests for the notification/profile dropdown toggle behaviour introduced in
 * Lume/wwwroot/js/script.js.
 *
 * The script is a plain (non-module) browser script that wires up click
 * handlers on DOMContentLoaded. It is loaded once for this whole file so the
 * listeners are attached exactly once to a stable set of DOM nodes; each test
 * only resets the `class` of the dropdowns rather than re-creating the DOM,
 * which keeps the click listeners (added once) valid across tests.
 */

const path = require('path');

describe('script.js - notification and profile dropdown toggles', () => {
  let notificationBtn;
  let notificationDropdown;
  let profileBtn;
  let profileDropdown;

  beforeAll(() => {
    document.body.innerHTML = `
      <button type="button" data-notification-btn>Bell</button>
      <div data-notification-dropdown class="hidden">Notifications</div>
      <button type="button" data-profile-btn>Profile</button>
      <div data-profile-dropdown class="hidden">Profile Menu</div>
    `;

    // Load the script once, then manually fire DOMContentLoaded since jsdom's
    // own DOMContentLoaded has already fired by the time this file executes.
    require(path.join(__dirname, 'script.js'));
    document.dispatchEvent(new Event('DOMContentLoaded'));

    notificationBtn = document.querySelector('[data-notification-btn]');
    notificationDropdown = document.querySelector('[data-notification-dropdown]');
    profileBtn = document.querySelector('[data-profile-btn]');
    profileDropdown = document.querySelector('[data-profile-dropdown]');
  });

  beforeEach(() => {
    // Reset visibility state without recreating the elements, so the
    // listeners registered in beforeAll remain attached.
    notificationDropdown.className = 'hidden';
    profileDropdown.className = 'hidden';
  });

  test('clicking the notification button reveals the notification dropdown', () => {
    notificationBtn.click();
    expect(notificationDropdown.classList.contains('hidden')).toBe(false);
  });

  test('clicking the notification button twice hides the notification dropdown again (toggle)', () => {
    notificationBtn.click();
    notificationBtn.click();
    expect(notificationDropdown.classList.contains('hidden')).toBe(true);
  });

  test('opening the notification dropdown closes an already-open profile dropdown', () => {
    profileDropdown.classList.remove('hidden');
    notificationBtn.click();
    expect(profileDropdown.classList.contains('hidden')).toBe(true);
    expect(notificationDropdown.classList.contains('hidden')).toBe(false);
  });

  test('clicking the profile button reveals the profile dropdown', () => {
    profileBtn.click();
    expect(profileDropdown.classList.contains('hidden')).toBe(false);
  });

  test('clicking the profile button twice hides the profile dropdown again (toggle)', () => {
    profileBtn.click();
    profileBtn.click();
    expect(profileDropdown.classList.contains('hidden')).toBe(true);
  });

  test('opening the profile dropdown closes an already-open notification dropdown', () => {
    notificationDropdown.classList.remove('hidden');
    profileBtn.click();
    expect(notificationDropdown.classList.contains('hidden')).toBe(true);
    expect(profileDropdown.classList.contains('hidden')).toBe(false);
  });

  test('clicking a toggle button does not immediately close its own dropdown via the outside-click handler', () => {
    // This verifies e.stopPropagation() actually prevents the click from
    // bubbling up to the document-level "click outside" handler, which would
    // otherwise re-hide the dropdown that was just opened.
    notificationBtn.click();
    expect(notificationDropdown.classList.contains('hidden')).toBe(false);
  });

  test('clicking outside the widgets hides an open notification dropdown', () => {
    notificationDropdown.classList.remove('hidden');
    document.body.click();
    expect(notificationDropdown.classList.contains('hidden')).toBe(true);
  });

  test('clicking outside the widgets hides an open profile dropdown', () => {
    profileDropdown.classList.remove('hidden');
    document.body.click();
    expect(profileDropdown.classList.contains('hidden')).toBe(true);
  });

  test('clicking outside is a no-op when both dropdowns are already hidden', () => {
    document.body.click();
    expect(notificationDropdown.classList.contains('hidden')).toBe(true);
    expect(profileDropdown.classList.contains('hidden')).toBe(true);
  });

  test('clicking outside hides both dropdowns simultaneously if both are open', () => {
    notificationDropdown.classList.remove('hidden');
    profileDropdown.classList.remove('hidden');
    document.body.click();
    expect(notificationDropdown.classList.contains('hidden')).toBe(true);
    expect(profileDropdown.classList.contains('hidden')).toBe(true);
  });
});