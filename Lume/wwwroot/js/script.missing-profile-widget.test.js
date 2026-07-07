/**
 * Regression test documenting the current behaviour of
 * Lume/wwwroot/js/script.js when the notification widget exists in the DOM
 * but the profile dropdown does not.
 *
 * The notification click handler unconditionally references the
 * `profileDropdown` variable:
 *
 *   notificationBtn.addEventListener('click', function (e) {
 *       e.stopPropagation();
 *       notificationDropdown.classList.toggle('hidden');
 *       profileDropdown.classList.add('hidden');
 *   });
 *
 * If `[data-profile-dropdown]` is not present, `profileDropdown` is `null`,
 * so clicking the notification button raises a TypeError from inside the
 * click handler. This is a known limitation of the current implementation
 * (it is not guarded), and this test exists so any change to that behaviour
 * is intentional and visible in the test suite.
 *
 * Per the DOM spec (and as implemented by jsdom), an exception thrown
 * inside an event listener does NOT propagate synchronously out of
 * `element.click()` / `dispatchEvent()` - it is reported as an uncaught
 * error on `window` instead. These tests capture that error via the
 * `window` "error" event rather than wrapping `.click()` in `toThrow()`.
 *
 * This scenario lives in its own file so it gets a fresh jsdom
 * document/module registry, independent from script.test.js.
 */

const path = require('path');

test('clicking the notification button raises an uncaught error when the profile dropdown element is missing', () => {
  document.body.innerHTML = `
    <button type="button" data-notification-btn>Bell</button>
    <div data-notification-dropdown class="hidden">Notifications</div>
  `;

  require(path.join(__dirname, 'script.js'));
  document.dispatchEvent(new Event('DOMContentLoaded'));

  const notificationBtn = document.querySelector('[data-notification-btn]');
  const notificationDropdown = document.querySelector('[data-notification-dropdown]');

  const uncaughtErrors = [];
  window.addEventListener('error', (event) => uncaughtErrors.push(event.error));

  notificationBtn.click();

  expect(uncaughtErrors).toHaveLength(1);
  expect(uncaughtErrors[0]).toBeInstanceOf(TypeError);
  expect(uncaughtErrors[0].message).toMatch(/classList/);

  // The notification dropdown toggle (line before the faulty
  // profileDropdown reference) still runs before the error is raised.
  expect(notificationDropdown.classList.contains('hidden')).toBe(false);
});

test('the DOMContentLoaded handler itself does not throw when the profile widget is missing', () => {
  document.body.innerHTML = `
    <button type="button" data-notification-btn>Bell</button>
    <div data-notification-dropdown class="hidden">Notifications</div>
  `;

  expect(() => {
    require(path.join(__dirname, 'script.js'));
    document.dispatchEvent(new Event('DOMContentLoaded'));
  }).not.toThrow();
});

test('clicking outside the widgets does not throw even when the profile dropdown is missing', () => {
  document.body.innerHTML = `
    <button type="button" data-notification-btn>Bell</button>
    <div data-notification-dropdown class="hidden">Notifications</div>
  `;

  require(path.join(__dirname, 'script.js'));
  document.dispatchEvent(new Event('DOMContentLoaded'));

  expect(() => document.body.click()).not.toThrow();
});